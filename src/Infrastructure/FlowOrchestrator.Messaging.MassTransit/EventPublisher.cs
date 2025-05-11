using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Messaging.MassTransit
{
    /// <summary>
    /// Publisher for events in the FlowOrchestrator system.
    /// </summary>
    public class EventPublisher
    {
        private readonly MassTransitMessageBus _messageBus;
        private readonly ILogger<EventPublisher> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventPublisher"/> class.
        /// </summary>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="logger">The logger.</param>
        public EventPublisher(MassTransitMessageBus messageBus, ILogger<EventPublisher> logger)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Publishes an event to all subscribers.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="event">The event to publish.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PublishEventAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));
            
            try
            {
                _logger.LogDebug("Publishing event of type {EventType}", typeof(T).Name);
                
                // Publish the event to all subscribers
                await _messageBus.PublishAsync(@event, cancellationToken);
                
                _logger.LogDebug("Event of type {EventType} published successfully", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing event of type {EventType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Publishes multiple events to all subscribers.
        /// </summary>
        /// <typeparam name="T">The type of the events.</typeparam>
        /// <param name="events">The events to publish.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PublishEventsAsync<T>(T[] events, CancellationToken cancellationToken = default) where T : class
        {
            if (events == null)
                throw new ArgumentNullException(nameof(events));
            
            if (events.Length == 0)
                return;
            
            try
            {
                _logger.LogDebug("Publishing {EventCount} events of type {EventType}", events.Length, typeof(T).Name);
                
                // Publish each event to all subscribers
                foreach (var @event in events)
                {
                    if (@event == null)
                        continue;
                    
                    await _messageBus.PublishAsync(@event, cancellationToken);
                }
                
                _logger.LogDebug("{EventCount} events of type {EventType} published successfully", events.Length, typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing events of type {EventType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Creates an event of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="configure">The action to configure the event.</param>
        /// <returns>The created event.</returns>
        public T CreateEvent<T>(Action<T> configure) where T : class, new()
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));
            
            try
            {
                _logger.LogDebug("Creating event of type {EventType}", typeof(T).Name);
                
                // Create the event
                var @event = new T();
                
                // Configure the event
                configure(@event);
                
                _logger.LogDebug("Event of type {EventType} created successfully", typeof(T).Name);
                
                return @event;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating event of type {EventType}", typeof(T).Name);
                throw;
            }
        }
    }
}
