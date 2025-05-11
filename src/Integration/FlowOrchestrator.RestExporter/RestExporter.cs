using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using FlowOrchestrator.ExporterBase;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.RestExporter;

/// <summary>
/// Exporter for exporting data to REST APIs.
/// </summary>
public class RestExporter : ExporterBase.ExporterBase
{
    private readonly ILogger<RestExporter> _logger;
    private readonly RestExporterOptions _options;
    private readonly HttpClient _httpClient;

    /// <inheritdoc />
    public override string ExporterType => "REST";

    /// <summary>
    /// Initializes a new instance of the <see cref="RestExporter"/> class.
    /// </summary>
    /// <param name="exporterId">The unique identifier for this exporter.</param>
    /// <param name="name">The name of this exporter.</param>
    /// <param name="description">The description of this exporter.</param>
    /// <param name="connectionManager">The connection manager for this exporter.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this exporter.</param>
    /// <param name="httpClient">The HTTP client.</param>
    public RestExporter(
        string exporterId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<RestExporter> logger,
        RestExporterOptions options,
        HttpClient httpClient)
        : base(exporterId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <inheritdoc />
    public override async Task<ExporterBase.ExportResult> ExportAsync(ExportContext exportContext, CancellationToken cancellationToken = default)
    {
        if (exportContext == null) throw new ArgumentNullException(nameof(exportContext));

        _logger.LogInformation("Exporting data to REST API for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot export data for exporter {ExporterId} in status {Status}");
        }

        Status = ExporterStatus.Exporting;

        try
        {
            var url = GetUrl(exportContext);
            var method = GetMethod(exportContext);
            var headers = GetHeaders(exportContext);
            var body = GetBody(exportContext);

            using var request = new HttpRequestMessage(method, url);

            // Add headers
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            // Add body if applicable
            if (method != HttpMethod.Get && method != HttpMethod.Head && body != null)
            {
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Content = content;
            }

            // Send request
            using var response = await _httpClient.SendAsync(request, cancellationToken);

            // Ensure success
            response.EnsureSuccessStatusCode();

            var result = ExporterBase.ExportResult.Success(
                exportContext.ExportId,
                1, // Records exported
                1  // Total records
            );

            Status = ExporterStatus.Open;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting data to REST API for exporter {ExporterId}", ExporterId);
            Status = ExporterStatus.Error;
            return ExporterBase.ExportResult.Failure(exportContext.ExportId, ex.Message);
        }
    }

    /// <inheritdoc />
    public override async Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting schema for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot get schema for exporter {ExporterId} in status {Status}");
        }

        // For REST exporter, we'll create a simple schema
        var schema = new DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<Domain.Entities.DataTable>
            {
                new Domain.Entities.DataTable
                {
                    Name = "RestData",
                    Description = "Data to export to the REST API",
                    Columns = new List<Domain.Entities.DataColumn>
                    {
                        new Domain.Entities.DataColumn
                        {
                            Name = "Content",
                            Description = "REST API request content",
                            DataType = "string",
                            IsNullable = false
                        }
                    }
                }
            }
        };

        await Task.CompletedTask;
        return schema;
    }

    /// <inheritdoc />
    public override ExporterCapabilities GetCapabilities()
    {
        return new ExporterCapabilities
        {
            SupportsStreaming = false,
            SupportsBatching = false,
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
            SupportsTransactions = false,
            SupportsBulkOperations = false,
            SupportsUpserts = false,
            SupportsDeletes = false,
            MaxBatchSize = 1,
            MaxParallelExports = 1,
            SupportedDataFormats = new List<string> { "json" },
            SupportedAuthenticationMethods = new List<string> { "none", "basic", "bearer", "api-key" },
            SupportedEncryptionMethods = new List<string> { "none", "ssl", "tls" },
            SupportedCompressionMethods = new List<string> { "none", "gzip" }
        };
    }

    private string GetUrl(ExportContext exportContext)
    {
        // Check if the URL is specified in the export context
        if (exportContext.Parameters.TryGetValue("Url", out var urlObj) && urlObj is string url)
        {
            return url;
        }

        // Otherwise, use the base URL from options and the endpoint from the export context
        if (exportContext.Parameters.TryGetValue("Endpoint", out var endpointObj) && endpointObj is string endpoint)
        {
            return $"{_options.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
        }

        // If no endpoint is specified, use the base URL
        return _options.BaseUrl;
    }

    private HttpMethod GetMethod(ExportContext exportContext)
    {
        // Check if the method is specified in the export context
        if (exportContext.Parameters.TryGetValue("Method", out var methodObj) && methodObj is string method)
        {
            return new HttpMethod(method);
        }

        // Otherwise, use the default method from options
        return new HttpMethod(_options.DefaultMethod);
    }

    private Dictionary<string, string> GetHeaders(ExportContext exportContext)
    {
        var headers = new Dictionary<string, string>();

        // Add default headers from options
        foreach (var header in _options.DefaultHeaders)
        {
            headers[header.Key] = header.Value;
        }

        // Add headers from export context
        if (exportContext.Parameters.TryGetValue("Headers", out var headersObj) && headersObj is Dictionary<string, string> exportHeaders)
        {
            foreach (var header in exportHeaders)
            {
                headers[header.Key] = header.Value;
            }
        }

        return headers;
    }

    private string? GetBody(ExportContext exportContext)
    {
        // Use the data from the export context if available
        if (exportContext.Data != null)
        {
            if (exportContext.Data is string dataStr)
            {
                return dataStr;
            }

            // If the data is not a string, serialize it to JSON
            return JsonSerializer.Serialize(exportContext.Data);
        }

        // Check if the body is specified in the export context
        if (exportContext.Parameters.TryGetValue("Body", out var bodyObj))
        {
            if (bodyObj is string bodyStr)
            {
                return bodyStr;
            }

            // If the body is not a string, serialize it to JSON
            return JsonSerializer.Serialize(bodyObj);
        }

        return null;
    }
}
