using System;
using System.Diagnostics;
using FlowOrchestrator.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FlowOrchestrator.Telemetry.OpenTelemetry
{
    /// <summary>
    /// OpenTelemetry provider implementation for the FlowOrchestrator system.
    /// </summary>
    public class OpenTelemetryProvider
    {
        private readonly ConfigurationParameters _configuration;
        private readonly ILogger<OpenTelemetryProvider> _logger;
        private readonly TracerProvider _tracerProvider;
        private readonly ActivitySource _activitySource;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTelemetryProvider"/> class.
        /// </summary>
        /// <param name="configuration">The configuration parameters.</param>
        /// <param name="logger">The logger.</param>
        public OpenTelemetryProvider(ConfigurationParameters configuration, ILogger<OpenTelemetryProvider> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            var serviceName = _configuration.GetParameter<string>("ServiceName") ?? "FlowOrchestrator";
            var serviceVersion = _configuration.GetParameter<string>("ServiceVersion") ?? "1.0.0";
            
            _activitySource = new ActivitySource(serviceName, serviceVersion);
            
            _tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                .AddSource(serviceName)
                .AddConsoleExporter()
                .Build();
            
            _logger.LogInformation("OpenTelemetry provider initialized for service {ServiceName} version {ServiceVersion}", 
                serviceName, serviceVersion);
        }

        /// <summary>
        /// Gets the activity source.
        /// </summary>
        public ActivitySource ActivitySource => _activitySource;

        /// <summary>
        /// Configures OpenTelemetry services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection ConfigureServices(IServiceCollection services, ConfigurationParameters configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            
            var serviceName = configuration.GetParameter<string>("ServiceName") ?? "FlowOrchestrator";
            var serviceVersion = configuration.GetParameter<string>("ServiceVersion") ?? "1.0.0";
            var otlpEndpoint = configuration.GetParameter<string>("OpenTelemetry:OtlpEndpoint");
            
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault()
                            .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                        .AddSource(serviceName)
                        .AddHttpClientInstrumentation()
                        .AddAspNetCoreInstrumentation();
                    
                    if (!string.IsNullOrEmpty(otlpEndpoint))
                    {
                        builder.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint));
                    }
                    else
                    {
                        builder.AddConsoleExporter();
                    }
                });
            
            services.AddSingleton<OpenTelemetryProvider>();
            services.AddSingleton<MetricsCollector>();
            services.AddSingleton<TracingProvider>();
            services.AddSingleton<LoggingProvider>();
            services.AddSingleton<HealthCheckProvider>();
            
            return services;
        }

        /// <summary>
        /// Creates a new activity.
        /// </summary>
        /// <param name="name">The name of the activity.</param>
        /// <returns>The created activity.</returns>
        public Activity? StartActivity(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Activity name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Starting activity {ActivityName}", name);
                
                var activity = _activitySource.StartActivity(name);
                
                if (activity != null)
                {
                    _logger.LogDebug("Activity {ActivityName} started with ID {ActivityId}", name, activity.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to start activity {ActivityName}", name);
                }
                
                return activity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting activity {ActivityName}", name);
                throw;
            }
        }

        /// <summary>
        /// Creates a new activity with the specified kind.
        /// </summary>
        /// <param name="name">The name of the activity.</param>
        /// <param name="kind">The kind of activity.</param>
        /// <returns>The created activity.</returns>
        public Activity? StartActivity(string name, ActivityKind kind)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Activity name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Starting activity {ActivityName} with kind {ActivityKind}", name, kind);
                
                var activity = _activitySource.StartActivity(name, kind);
                
                if (activity != null)
                {
                    _logger.LogDebug("Activity {ActivityName} started with ID {ActivityId} and kind {ActivityKind}", 
                        name, activity.Id, kind);
                }
                else
                {
                    _logger.LogWarning("Failed to start activity {ActivityName} with kind {ActivityKind}", name, kind);
                }
                
                return activity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting activity {ActivityName} with kind {ActivityKind}", name, kind);
                throw;
            }
        }

        /// <summary>
        /// Disposes the OpenTelemetry provider.
        /// </summary>
        public void Dispose()
        {
            _tracerProvider?.Dispose();
        }
    }
}
