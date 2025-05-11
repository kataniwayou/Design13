using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using FlowOrchestrator.ExporterBase;

namespace FlowOrchestrator.MessageQueueExporter;

/// <summary>
/// Exporter for exporting data to message queues.
/// </summary>
public class MessageQueueExporter : ExporterBase.ExporterBase
{
    private readonly ILogger<MessageQueueExporter> _logger;
    private readonly MessageQueueExporterOptions _options;
    private readonly IMessageQueueClient _messageQueueClient;

    /// <inheritdoc />
    public override string ExporterType => "MessageQueue";

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageQueueExporter"/> class.
    /// </summary>
    /// <param name="exporterId">The unique identifier for this exporter.</param>
    /// <param name="name">The name of this exporter.</param>
    /// <param name="description">The description of this exporter.</param>
    /// <param name="connectionManager">The connection manager for this exporter.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this exporter.</param>
    /// <param name="messageQueueClient">The message queue client.</param>
    public MessageQueueExporter(
        string exporterId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<MessageQueueExporter> logger,
        MessageQueueExporterOptions options,
        IMessageQueueClient messageQueueClient)
        : base(exporterId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _messageQueueClient = messageQueueClient ?? throw new ArgumentNullException(nameof(messageQueueClient));
    }

    /// <inheritdoc />
    public override async Task<ExporterBase.ExportResult> ExportAsync(ExporterBase.ExportContext exportContext, CancellationToken cancellationToken = default)
    {
        if (exportContext == null) throw new ArgumentNullException(nameof(exportContext));

        _logger.LogInformation("Exporting data to message queue for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot export data for exporter {ExporterId} in status {Status}");
        }

        Status = ExporterStatus.Exporting;

        try
        {
            var queueName = GetQueueName(exportContext);
            var exchange = GetExchange(exportContext);
            var routingKey = GetRoutingKey(exportContext, queueName);
            var data = exportContext.Data;

            if (data == null)
            {
                _logger.LogWarning("No data to export to message queue {QueueName}", queueName);

                var emptyResult = ExporterBase.ExportResult.Success(
                    exportContext.ExportId,
                    0, // Records exported
                    0  // Total records
                );

                Status = ExporterStatus.Open;
                return emptyResult;
            }

            // Convert data to messages
            var messages = ConvertToMessages(data, exportContext);

            _logger.LogInformation("Exporting {MessageCount} messages to queue {QueueName}", messages.Count, queueName);

            // Publish messages
            foreach (var message in messages)
            {
                await _messageQueueClient.PublishMessageAsync(
                    exchange,
                    routingKey,
                    message,
                    _options.UsePersistentMessages,
                    cancellationToken);
            }

            var result = ExporterBase.ExportResult.Success(
                exportContext.ExportId,
                messages.Count, // Records exported
                messages.Count  // Total records
            );

            Status = ExporterStatus.Open;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting data to message queue for exporter {ExporterId}", ExporterId);
            Status = ExporterStatus.Error;
            return ExporterBase.ExportResult.Failure(exportContext.ExportId, ex.Message);
        }
    }

    /// <inheritdoc />
    public override async Task<Domain.Entities.DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting schema for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot get schema for exporter {ExporterId} in status {Status}");
        }

        // For message queue exporter, we'll create a simple schema
        var schema = new Domain.Entities.DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<Domain.Entities.DataTable>
            {
                new Domain.Entities.DataTable
                {
                    Name = "MessageData",
                    Description = "Data to export to the message queue",
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
    public override ExporterBase.ExporterCapabilities GetCapabilities()
    {
        return new ExporterBase.ExporterCapabilities
        {
            SupportsStreaming = true,
            SupportsBatching = true,
            SupportsFiltering = false,
            SupportsSorting = false,
            SupportsPagination = false,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalExport = false,
            SupportsParallelExport = false,
            SupportsResumeExport = false,
            SupportsAuthentication = true,
            SupportsEncryption = true,
            SupportsCompression = true,
            SupportsTransactions = _options.UseTransactions,
            SupportsBulkOperations = false,
            SupportsUpserts = false,
            SupportsDeletes = false,
            MaxBatchSize = _options.MaxBatchSize,
            MaxParallelExports = 1,
            SupportedDataFormats = new List<string> { "json", "xml", "text", "binary" },
            SupportedAuthenticationMethods = new List<string> { "none", "basic", "certificate", "token" },
            SupportedEncryptionMethods = new List<string> { "none", "ssl", "tls" },
            SupportedCompressionMethods = new List<string> { "none", "gzip" }
        };
    }

    private string GetQueueName(ExporterBase.ExportContext exportContext)
    {
        // Check if the queue name is specified in the export context
        if (exportContext.Parameters.TryGetValue("QueueName", out var queueNameObj) && queueNameObj is string queueName)
        {
            return queueName;
        }

        // Otherwise, use the default queue name from options
        return _options.DefaultQueueName;
    }

    private string GetExchange(ExporterBase.ExportContext exportContext)
    {
        // Check if the exchange is specified in the export context
        if (exportContext.Parameters.TryGetValue("Exchange", out var exchangeObj) && exchangeObj is string exchange)
        {
            return exchange;
        }

        // Otherwise, use the default exchange from options
        return _options.DefaultExchange;
    }

    private string GetRoutingKey(ExporterBase.ExportContext exportContext, string queueName)
    {
        // Check if the routing key is specified in the export context
        if (exportContext.Parameters.TryGetValue("RoutingKey", out var routingKeyObj) && routingKeyObj is string routingKey)
        {
            return routingKey;
        }

        // Otherwise, use the queue name as the routing key
        return queueName;
    }

    private List<MessageQueueMessage> ConvertToMessages(object data, ExporterBase.ExportContext exportContext)
    {
        var messages = new List<MessageQueueMessage>();

        // If the data is already a collection of messages, use it
        if (data is IEnumerable<MessageQueueMessage> messageCollection)
        {
            messages.AddRange(messageCollection);
            return messages;
        }

        // If the data is a single message, wrap it in a collection
        if (data is MessageQueueMessage singleMessage)
        {
            messages.Add(singleMessage);
            return messages;
        }

        // If the data is a collection, create a message for each item
        if (data is IEnumerable<object> collection)
        {
            foreach (var item in collection)
            {
                var message = CreateMessage(item, exportContext);
                messages.Add(message);
            }

            return messages;
        }

        // Otherwise, create a single message for the data
        var singleDataMessage = CreateMessage(data, exportContext);
        messages.Add(singleDataMessage);

        return messages;
    }

    private MessageQueueMessage CreateMessage(object data, ExporterBase.ExportContext exportContext)
    {
        var message = new MessageQueueMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow
        };

        // Set the content
        if (data is string stringData)
        {
            message.Content = stringData;
        }
        else
        {
            message.Content = JsonSerializer.Serialize(data);
        }

        // Set the properties
        if (exportContext.Parameters.TryGetValue("Properties", out var propertiesObj) && propertiesObj is Dictionary<string, object> properties)
        {
            foreach (var property in properties)
            {
                message.Properties[property.Key] = property.Value;
            }
        }

        // Set the headers
        if (exportContext.Parameters.TryGetValue("Headers", out var headersObj) && headersObj is Dictionary<string, object> headers)
        {
            foreach (var header in headers)
            {
                message.Headers[header.Key] = header.Value;
            }
        }

        return message;
    }
}
