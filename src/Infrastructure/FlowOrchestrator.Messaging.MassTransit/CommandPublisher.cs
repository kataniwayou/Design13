using System;
using System.Threading;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Configuration;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Messaging.MassTransit
{
    /// <summary>
    /// Publisher for commands in the FlowOrchestrator system.
    /// </summary>
    public class CommandPublisher
    {
        private readonly MassTransitMessageBus _messageBus;
        private readonly ILogger<CommandPublisher> _logger;
        private readonly ConfigurationParameters _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandPublisher"/> class.
        /// </summary>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="configuration">The configuration parameters.</param>
        /// <param name="logger">The logger.</param>
        public CommandPublisher(MassTransitMessageBus messageBus, ConfigurationParameters configuration, ILogger<CommandPublisher> logger)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Sends a command to a specific service.
        /// </summary>
        /// <typeparam name="T">The type of the command.</typeparam>
        /// <param name="command">The command to send.</param>
        /// <param name="serviceName">The name of the service to send the command to.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendCommandAsync<T>(T command, string serviceName, CancellationToken cancellationToken = default) where T : class
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentException("Service name cannot be null or empty.", nameof(serviceName));

            try
            {
                _logger.LogDebug("Sending command of type {CommandType} to service {ServiceName}", typeof(T).Name, serviceName);

                // Get the endpoint address for the service
                var endpointAddress = GetServiceEndpointAddress(serviceName);

                // Send the command to the service
                await _messageBus.SendAsync(command, endpointAddress, cancellationToken);

                _logger.LogDebug("Command of type {CommandType} sent successfully to service {ServiceName}", typeof(T).Name, serviceName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending command of type {CommandType} to service {ServiceName}", typeof(T).Name, serviceName);
                throw;
            }
        }

        /// <summary>
        /// Sends a command to a specific endpoint.
        /// </summary>
        /// <typeparam name="T">The type of the command.</typeparam>
        /// <param name="command">The command to send.</param>
        /// <param name="endpointAddress">The address of the endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendCommandAsync<T>(T command, Uri endpointAddress, CancellationToken cancellationToken = default) where T : class
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress));

            try
            {
                _logger.LogDebug("Sending command of type {CommandType} to endpoint {EndpointAddress}", typeof(T).Name, endpointAddress);

                // Send the command to the endpoint
                await _messageBus.SendAsync(command, endpointAddress, cancellationToken);

                _logger.LogDebug("Command of type {CommandType} sent successfully to endpoint {EndpointAddress}", typeof(T).Name, endpointAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending command of type {CommandType} to endpoint {EndpointAddress}", typeof(T).Name, endpointAddress);
                throw;
            }
        }

        /// <summary>
        /// Gets the endpoint address for a service.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>The endpoint address.</returns>
        private Uri GetServiceEndpointAddress(string serviceName)
        {
            // Get the RabbitMQ host and port from configuration
            var host = _configuration.GetParameter<string>("RabbitMQ:Host") ?? "localhost";
            var port = _configuration.GetParameter<int>("RabbitMQ:Port");
            if (port == 0) port = 5672;
            var virtualHost = _configuration.GetParameter<string>("RabbitMQ:VirtualHost") ?? "/";

            // Get the queue name for the service
            var queueName = _configuration.GetParameter<string>($"Services:{serviceName}:QueueName") ?? serviceName;

            // Create the endpoint address
            return new Uri($"rabbitmq://{host}:{port}/{virtualHost}/{queueName}");
        }
    }
}
