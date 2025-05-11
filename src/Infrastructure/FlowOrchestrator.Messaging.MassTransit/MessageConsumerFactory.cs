using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Messaging.MassTransit
{
    /// <summary>
    /// Factory for creating message consumers.
    /// </summary>
    public class MessageConsumerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MessageConsumerFactory> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageConsumerFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        public MessageConsumerFactory(IServiceProvider serviceProvider, ILogger<MessageConsumerFactory> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Configures consumers for a receive endpoint.
        /// </summary>
        /// <param name="configurator">The receive endpoint configurator.</param>
        /// <param name="consumerTypes">The types of consumers to configure.</param>
        public void ConfigureConsumers(IReceiveEndpointConfigurator configurator, IEnumerable<Type> consumerTypes)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            if (consumerTypes == null)
                throw new ArgumentNullException(nameof(consumerTypes));

            foreach (var consumerType in consumerTypes)
            {
                ConfigureConsumer(configurator, consumerType);
            }
        }

        /// <summary>
        /// Configures a consumer for a receive endpoint.
        /// </summary>
        /// <param name="configurator">The receive endpoint configurator.</param>
        /// <param name="consumerType">The type of consumer to configure.</param>
        public void ConfigureConsumer(IReceiveEndpointConfigurator configurator, Type consumerType)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            if (consumerType == null)
                throw new ArgumentNullException(nameof(consumerType));

            if (!typeof(IConsumer).IsAssignableFrom(consumerType))
                throw new ArgumentException($"Type {consumerType.Name} does not implement IConsumer", nameof(consumerType));

            try
            {
                _logger.LogDebug("Configuring consumer of type {ConsumerType}", consumerType.Name);

                // Use reflection to call the generic ConfigureConsumer method
                var method = typeof(DependencyInjectionReceiveEndpointExtensions)
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == "ConfigureConsumer" &&
                                        m.GetParameters().Length == 2 &&
                                        m.GetParameters()[0].ParameterType == typeof(IReceiveEndpointConfigurator) &&
                                        m.GetParameters()[1].ParameterType == typeof(IServiceProvider))
                    ?.MakeGenericMethod(consumerType);

                method?.Invoke(null, new object[] { configurator, _serviceProvider });

                _logger.LogDebug("Consumer of type {ConsumerType} configured successfully", consumerType.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error configuring consumer of type {ConsumerType}", consumerType.Name);
                throw;
            }
        }

        /// <summary>
        /// Creates a consumer of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of consumer to create.</typeparam>
        /// <returns>The created consumer.</returns>
        public T CreateConsumer<T>() where T : class, IConsumer
        {
            try
            {
                _logger.LogDebug("Creating consumer of type {ConsumerType}", typeof(T).Name);
                var consumer = _serviceProvider.GetRequiredService<T>();
                _logger.LogDebug("Consumer of type {ConsumerType} created successfully", typeof(T).Name);
                return consumer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating consumer of type {ConsumerType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Creates a consumer of the specified type.
        /// </summary>
        /// <param name="consumerType">The type of consumer to create.</param>
        /// <returns>The created consumer.</returns>
        public IConsumer CreateConsumer(Type consumerType)
        {
            if (consumerType == null)
                throw new ArgumentNullException(nameof(consumerType));

            if (!typeof(IConsumer).IsAssignableFrom(consumerType))
                throw new ArgumentException($"Type {consumerType.Name} does not implement IConsumer", nameof(consumerType));

            try
            {
                _logger.LogDebug("Creating consumer of type {ConsumerType}", consumerType.Name);
                var consumer = (IConsumer)_serviceProvider.GetRequiredService(consumerType);
                _logger.LogDebug("Consumer of type {ConsumerType} created successfully", consumerType.Name);
                return consumer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating consumer of type {ConsumerType}", consumerType.Name);
                throw;
            }
        }

        /// <summary>
        /// Registers a consumer type with the service collection.
        /// </summary>
        /// <typeparam name="T">The type of consumer to register.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection RegisterConsumer<T>(IServiceCollection services) where T : class, IConsumer
        {
            return services.AddScoped<T>();
        }

        /// <summary>
        /// Registers a consumer type with the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="consumerType">The type of consumer to register.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection RegisterConsumer(IServiceCollection services, Type consumerType)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (consumerType == null)
                throw new ArgumentNullException(nameof(consumerType));

            if (!typeof(IConsumer).IsAssignableFrom(consumerType))
                throw new ArgumentException($"Type {consumerType.Name} does not implement IConsumer", nameof(consumerType));

            return services.AddScoped(consumerType);
        }
    }
}
