namespace FlowOrchestrator.MessageQueueExporter;

/// <summary>
/// Interface for message queue clients.
/// </summary>
public interface IMessageQueueClient
{
    /// <summary>
    /// Publishes a message to the specified exchange with the specified routing key.
    /// </summary>
    /// <param name="exchange">The exchange to publish to.</param>
    /// <param name="routingKey">The routing key to use.</param>
    /// <param name="message">The message to publish.</param>
    /// <param name="persistent">A value indicating whether the message should be persistent.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PublishMessageAsync(string exchange, string routingKey, MessageQueueMessage message, bool persistent, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="durable">A value indicating whether the queue should be durable.</param>
    /// <param name="exclusive">A value indicating whether the queue should be exclusive.</param>
    /// <param name="autoDelete">A value indicating whether the queue should be auto-deleted.</param>
    /// <param name="arguments">The queue arguments.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateQueueAsync(string queueName, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object>? arguments = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates an exchange.
    /// </summary>
    /// <param name="exchange">The name of the exchange.</param>
    /// <param name="type">The type of the exchange.</param>
    /// <param name="durable">A value indicating whether the exchange should be durable.</param>
    /// <param name="autoDelete">A value indicating whether the exchange should be auto-deleted.</param>
    /// <param name="arguments">The exchange arguments.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateExchangeAsync(string exchange, string type, bool durable, bool autoDelete, IDictionary<string, object>? arguments = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Binds a queue to an exchange.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="exchange">The name of the exchange.</param>
    /// <param name="routingKey">The routing key to use.</param>
    /// <param name="arguments">The binding arguments.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task BindQueueAsync(string queueName, string exchange, string routingKey, IDictionary<string, object>? arguments = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the queue statistics.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the queue statistics.</returns>
    Task<MessageQueueStatistics> GetQueueStatisticsAsync(string queueName, CancellationToken cancellationToken = default);
}
