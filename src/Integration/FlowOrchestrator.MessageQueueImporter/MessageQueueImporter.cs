using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using FlowOrchestrator.ImporterBase;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.MessageQueueImporter;

/// <summary>
/// Importer for importing data from message queues.
/// </summary>
public class MessageQueueImporter : ImporterBase.ImporterBase
{
    private readonly ILogger<MessageQueueImporter> _logger;
    private readonly MessageQueueImporterOptions _options;
    private readonly IMessageQueueClient _messageQueueClient;

    /// <inheritdoc />
    public override string ImporterType => "MessageQueue";

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageQueueImporter"/> class.
    /// </summary>
    /// <param name="importerId">The unique identifier for this importer.</param>
    /// <param name="name">The name of this importer.</param>
    /// <param name="description">The description of this importer.</param>
    /// <param name="connectionManager">The connection manager for this importer.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this importer.</param>
    /// <param name="messageQueueClient">The message queue client.</param>
    public MessageQueueImporter(
        string importerId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<MessageQueueImporter> logger,
        MessageQueueImporterOptions options,
        IMessageQueueClient messageQueueClient)
        : base(importerId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _messageQueueClient = messageQueueClient ?? throw new ArgumentNullException(nameof(messageQueueClient));
    }

    /// <inheritdoc />
    public override async Task<ImporterBase.ImportResult> ImportAsync(ImportContext importContext, CancellationToken cancellationToken = default)
    {
        if (importContext == null) throw new ArgumentNullException(nameof(importContext));

        _logger.LogInformation("Importing data from message queue for importer {ImporterId}", ImporterId);

        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot import data for importer {ImporterId} in status {Status}");
        }

        Status = ImporterStatus.Importing;

        try
        {
            var queueName = GetQueueName(importContext);
            var batchSize = GetBatchSize(importContext);
            var timeout = GetTimeout(importContext);

            var messages = await _messageQueueClient.ReceiveMessagesAsync(queueName, batchSize, timeout, cancellationToken);

            if (messages.Count == 0)
            {
                _logger.LogInformation("No messages received from queue {QueueName}", queueName);

                var emptyResult = ImporterBase.ImportResult.Success(
                    importContext.ImportId,
                    0, // Records imported
                    0, // Total records
                    new List<object>());

                Status = ImporterStatus.Open;
                return emptyResult;
            }

            _logger.LogInformation("Received {MessageCount} messages from queue {QueueName}", messages.Count, queueName);

            var data = new List<object>();

            foreach (var message in messages)
            {
                try
                {
                    var messageData = DeserializeMessage(message);
                    data.Add(messageData);

                    // Acknowledge the message if required
                    if (_options.AcknowledgeMessages)
                    {
                        await _messageQueueClient.AcknowledgeMessageAsync(queueName, message.MessageId, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message {MessageId} from queue {QueueName}", message.MessageId, queueName);

                    // Handle the error based on options
                    if (_options.DeadLetterOnError)
                    {
                        await _messageQueueClient.DeadLetterMessageAsync(queueName, message.MessageId, ex.Message, cancellationToken);
                    }
                    else if (_options.RequeueOnError)
                    {
                        await _messageQueueClient.RequeueMessageAsync(queueName, message.MessageId, cancellationToken);
                    }
                }
            }

            var result = ImporterBase.ImportResult.Success(
                importContext.ImportId,
                data.Count, // Records imported
                data.Count, // Total records
                data);

            Status = ImporterStatus.Open;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing data from message queue for importer {ImporterId}", ImporterId);
            Status = ImporterStatus.Error;
            return ImporterBase.ImportResult.Failure(importContext.ImportId, ex.Message);
        }
    }

    /// <inheritdoc />
    public override async Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting schema for importer {ImporterId}", ImporterId);

        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot get schema for importer {ImporterId} in status {Status}");
        }

        // For message queue importer, we'll create a simple schema
        var schema = new DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<Domain.Entities.DataTable>
            {
                new Domain.Entities.DataTable
                {
                    Name = "MessageData",
                    Description = "Data from the message queue",
                    Columns = new List<Domain.Entities.DataColumn>
                    {
                        new Domain.Entities.DataColumn
                        {
                            Name = "MessageId",
                            Description = "Message ID",
                            DataType = "string",
                            IsNullable = false
                        },
                        new Domain.Entities.DataColumn
                        {
                            Name = "Content",
                            Description = "Message content",
                            DataType = "string",
                            IsNullable = false
                        },
                        new Domain.Entities.DataColumn
                        {
                            Name = "Timestamp",
                            Description = "Message timestamp",
                            DataType = "datetime",
                            IsNullable = false
                        },
                        new Domain.Entities.DataColumn
                        {
                            Name = "Properties",
                            Description = "Message properties",
                            DataType = "object",
                            IsNullable = true
                        }
                    }
                }
            }
        };

        await Task.CompletedTask;
        return schema;
    }

    /// <inheritdoc />
    public override ImporterCapabilities GetCapabilities()
    {
        return new ImporterCapabilities
        {
            SupportsStreaming = true,
            SupportsBatching = true,
            SupportsFiltering = false,
            SupportsSorting = false,
            SupportsPagination = false,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalImport = false,
            SupportsParallelImport = false,
            SupportsResumeImport = false,
            SupportsAuthentication = true,
            SupportsEncryption = true,
            SupportsCompression = true,
            MaxBatchSize = _options.MaxBatchSize,
            MaxParallelImports = 1,
            SupportedDataFormats = new List<string> { "json", "xml", "text", "binary" },
            SupportedAuthenticationMethods = new List<string> { "none", "basic", "certificate", "token" },
            SupportedEncryptionMethods = new List<string> { "none", "ssl", "tls" },
            SupportedCompressionMethods = new List<string> { "none", "gzip" }
        };
    }

    private string GetQueueName(ImportContext importContext)
    {
        // Check if the queue name is specified in the import context
        if (importContext.Parameters.TryGetValue("QueueName", out var queueNameObj) && queueNameObj is string queueName)
        {
            return queueName;
        }

        // Otherwise, use the default queue name from options
        return _options.DefaultQueueName;
    }

    private int GetBatchSize(ImportContext importContext)
    {
        // Check if the batch size is specified in the import context
        if (importContext.Parameters.TryGetValue("BatchSize", out var batchSizeObj) && batchSizeObj is int batchSize)
        {
            return Math.Min(batchSize, _options.MaxBatchSize);
        }

        // Otherwise, use the batch size from the import context or the default batch size from options
        return Math.Min(importContext.BatchSize, _options.MaxBatchSize);
    }

    private TimeSpan GetTimeout(ImportContext importContext)
    {
        // Check if the timeout is specified in the import context
        if (importContext.Parameters.TryGetValue("TimeoutSeconds", out var timeoutObj) && timeoutObj is int timeoutSeconds)
        {
            return TimeSpan.FromSeconds(timeoutSeconds);
        }

        // Otherwise, use the timeout from the import context or the default timeout from options
        return TimeSpan.FromSeconds(importContext.TimeoutSeconds);
    }

    private object DeserializeMessage(MessageQueueMessage message)
    {
        if (message.Content == null)
        {
            return new { };
        }

        try
        {
            // Try to deserialize as JSON
            return JsonSerializer.Deserialize<object>(message.Content) ?? new { };
        }
        catch
        {
            // If JSON deserialization fails, return the content as is
            return message.Content;
        }
    }
}
