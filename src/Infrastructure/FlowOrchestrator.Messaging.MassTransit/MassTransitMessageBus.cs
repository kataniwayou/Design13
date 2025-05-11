using System;
using System.Threading;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Configuration;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Messaging.MassTransit
{
    /// <summary>
    /// MassTransit implementation of the message bus for the FlowOrchestrator system.
    /// </summary>
    public class MassTransitMessageBus : IDisposable
    {
        private readonly IBusControl _busControl;
        private readonly IBus _bus;
        private readonly ILogger<MassTransitMessageBus> _logger;
        private readonly ConfigurationParameters _configuration;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassTransitMessageBus"/> class.
        /// </summary>
        /// <param name="configuration">The configuration parameters for the message bus.</param>
        /// <param name="logger">The logger.</param>
        public MassTransitMessageBus(ConfigurationParameters configuration, ILogger<MassTransitMessageBus> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _busControl = ConfigureBus();
            _busControl.Start();
            _bus = _busControl;
        }

        /// <summary>
        /// Gets the MassTransit bus instance.
        /// </summary>
        public IBus Bus => _bus;

        /// <summary>
        /// Publishes a message to the bus.
        /// </summary>
        /// <typeparam name="T">The type of the message.</typeparam>
        /// <param name="message">The message to publish.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            try
            {
                _logger.LogDebug("Publishing message of type {MessageType}", typeof(T).Name);
                await _bus.Publish(message, cancellationToken);
                _logger.LogDebug("Message of type {MessageType} published successfully", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message of type {MessageType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Sends a message to a specific endpoint.
        /// </summary>
        /// <typeparam name="T">The type of the message.</typeparam>
        /// <param name="message">The message to send.</param>
        /// <param name="endpointAddress">The address of the endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendAsync<T>(T message, Uri endpointAddress, CancellationToken cancellationToken = default) where T : class
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress));

            try
            {
                _logger.LogDebug("Sending message of type {MessageType} to endpoint {EndpointAddress}", typeof(T).Name, endpointAddress);
                var endpoint = await _bus.GetSendEndpoint(endpointAddress);
                await endpoint.Send(message, cancellationToken);
                _logger.LogDebug("Message of type {MessageType} sent successfully to endpoint {EndpointAddress}", typeof(T).Name, endpointAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message of type {MessageType} to endpoint {EndpointAddress}", typeof(T).Name, endpointAddress);
                throw;
            }
        }

        /// <summary>
        /// Configures the MassTransit bus.
        /// </summary>
        /// <returns>The configured bus control.</returns>
        private IBusControl ConfigureBus()
        {
            return global::MassTransit.Bus.Factory.CreateUsingInMemory(cfg =>
            {
                // Configure in-memory transport
                var queueLimit = _configuration.GetParameter<int>("InMemory:QueueLimit");
                if (queueLimit == 0) queueLimit = 1000;

                var timeout = _configuration.GetParameter<int>("InMemory:Timeout");
                if (timeout == 0) timeout = 30000;

                // Configure concurrency limit
                cfg.ConcurrentMessageLimit = 10;

                // Configure message retry
                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

                // Configure retry policy
                var retryCount = _configuration.GetParameter<int>("InMemory:RetryCount");
                if (retryCount == 0) retryCount = 3;
                var retryIntervalSeconds = _configuration.GetParameter<int>("InMemory:RetryIntervalSeconds");
                if (retryIntervalSeconds == 0) retryIntervalSeconds = 5;

                cfg.UseRetry(r => r.Interval(
                    retryCount,
                    TimeSpan.FromSeconds(retryIntervalSeconds)
                ));

                // Configure message serialization
                cfg.UseJsonSerializer();

                // Configure circuit breaker
                cfg.UseCircuitBreaker(cb =>
                {
                    var trackingPeriodMinutes = _configuration.GetParameter<int>("RabbitMQ:CircuitBreaker:TrackingPeriodMinutes");
                    if (trackingPeriodMinutes == 0) trackingPeriodMinutes = 1;

                    var tripThreshold = _configuration.GetParameter<int>("RabbitMQ:CircuitBreaker:TripThreshold");
                    if (tripThreshold == 0) tripThreshold = 15;

                    var activeThreshold = _configuration.GetParameter<int>("RabbitMQ:CircuitBreaker:ActiveThreshold");
                    if (activeThreshold == 0) activeThreshold = 10;

                    var resetIntervalMinutes = _configuration.GetParameter<int>("RabbitMQ:CircuitBreaker:ResetIntervalMinutes");
                    if (resetIntervalMinutes == 0) resetIntervalMinutes = 5;

                    cb.TrackingPeriod = TimeSpan.FromMinutes(trackingPeriodMinutes);
                    cb.TripThreshold = tripThreshold;
                    cb.ActiveThreshold = activeThreshold;
                    cb.ResetInterval = TimeSpan.FromMinutes(resetIntervalMinutes);
                });
            });
        }

        /// <summary>
        /// Disposes the MassTransit message bus.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the MassTransit message bus.
        /// </summary>
        /// <param name="disposing">Whether to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _busControl?.Stop();
                }

                _disposed = true;
            }
        }
    }
}
