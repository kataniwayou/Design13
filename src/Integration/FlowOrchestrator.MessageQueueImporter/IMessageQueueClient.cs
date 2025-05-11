namespace FlowOrchestrator.MessageQueueImporter;

/// <summary>
/// Interface for message queue clients.
/// </summary>
public interface IMessageQueueClient
{
    /// <summary>
    /// Receives messages from the specified queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="batchSize">The maximum number of messages to receive.</param>
    /// <param name="timeout">The timeout for the operation.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the received messages.</returns>
    Task<List<MessageQueueMessage>> ReceiveMessagesAsync(string queueName, int batchSize, TimeSpan timeout, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Acknowledges a message.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="messageId">The ID of the message to acknowledge.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AcknowledgeMessageAsync(string queueName, string messageId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Requeues a message.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="messageId">The ID of the message to requeue.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RequeueMessageAsync(string queueName, string messageId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a message to the dead letter queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="messageId">The ID of the message to send to the dead letter queue.</param>
    /// <param name="reason">The reason for sending the message to the dead letter queue.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeadLetterMessageAsync(string queueName, string messageId, string reason, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the queue statistics.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the queue statistics.</returns>
    Task<MessageQueueStatistics> GetQueueStatisticsAsync(string queueName, CancellationToken cancellationToken = default);
    
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
    /// Deletes a queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="ifUnused">A value indicating whether the queue should only be deleted if it is unused.</param>
    /// <param name="ifEmpty">A value indicating whether the queue should only be deleted if it is empty.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteQueueAsync(string queueName, bool ifUnused, bool ifEmpty, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Purges a queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PurgeQueueAsync(string queueName, CancellationToken cancellationToken = default);
}
