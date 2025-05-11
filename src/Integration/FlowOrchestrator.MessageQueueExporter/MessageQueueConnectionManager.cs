using FlowOrchestrator.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.MessageQueueExporter;

/// <summary>
/// Connection manager for message queue operations.
/// </summary>
public class MessageQueueConnectionManager : IConnectionManager
{
    private readonly ILogger<MessageQueueConnectionManager> _logger;
    private readonly MessageQueueExporterOptions _options;
    private readonly IMessageQueueClient _messageQueueClient;
    private bool _isOpen;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageQueueConnectionManager"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    /// <param name="messageQueueClient">The message queue client.</param>
    public MessageQueueConnectionManager(
        ILogger<MessageQueueConnectionManager> logger,
        MessageQueueExporterOptions options,
        IMessageQueueClient messageQueueClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _messageQueueClient = messageQueueClient ?? throw new ArgumentNullException(nameof(messageQueueClient));
    }
    
    /// <inheritdoc />
    public async Task OpenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Opening message queue connection");
        
        if (_isOpen)
        {
            _logger.LogWarning("Message queue connection is already open");
            return;
        }
        
        // Create the queue if it doesn't exist and declaration is enabled
        if (_options.DeclareQueuesBeforePublishing && !string.IsNullOrEmpty(_options.DefaultQueueName))
        {
            await _messageQueueClient.CreateQueueAsync(
                _options.DefaultQueueName,
                _options.UseDurableQueues,
                _options.UseExclusiveQueues,
                _options.UseAutoDeleteQueues,
                null,
                cancellationToken);
        }
        
        // Create the exchange if it doesn't exist and declaration is enabled
        if (_options.DeclareExchangesBeforePublishing && !string.IsNullOrEmpty(_options.DefaultExchange))
        {
            await _messageQueueClient.CreateExchangeAsync(
                _options.DefaultExchange,
                "direct", // Default exchange type
                _options.UseDurableQueues,
                _options.UseAutoDeleteQueues,
                null,
                cancellationToken);
        }
        
        // Bind the queue to the exchange if binding is enabled
        if (_options.BindQueuesBeforePublishing && !string.IsNullOrEmpty(_options.DefaultQueueName) && !string.IsNullOrEmpty(_options.DefaultExchange))
        {
            await _messageQueueClient.BindQueueAsync(
                _options.DefaultQueueName,
                _options.DefaultExchange,
                _options.DefaultQueueName, // Use queue name as routing key
                null,
                cancellationToken);
        }
        
        _isOpen = true;
    }
    
    /// <inheritdoc />
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing message queue connection");
        
        if (!_isOpen)
        {
            _logger.LogWarning("Message queue connection is already closed");
            return;
        }
        
        _isOpen = false;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public async Task TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Testing message queue connection");
        
        try
        {
            // Try to get queue statistics
            if (!string.IsNullOrEmpty(_options.DefaultQueueName))
            {
                await _messageQueueClient.GetQueueStatisticsAsync(_options.DefaultQueueName, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error testing message queue connection");
            throw new InvalidOperationException("Cannot connect to message queue", ex);
        }
    }
}
