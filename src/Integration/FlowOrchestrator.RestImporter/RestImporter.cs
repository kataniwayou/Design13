using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using FlowOrchestrator.ImporterBase;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.RestImporter;

/// <summary>
/// Importer for importing data from REST APIs.
/// </summary>
public class RestImporter : ImporterBase.ImporterBase
{
    private readonly ILogger<RestImporter> _logger;
    private readonly RestImporterOptions _options;
    private readonly HttpClient _httpClient;

    /// <inheritdoc />
    public override string ImporterType => "REST";

    /// <summary>
    /// Initializes a new instance of the <see cref="RestImporter"/> class.
    /// </summary>
    /// <param name="importerId">The unique identifier for this importer.</param>
    /// <param name="name">The name of this importer.</param>
    /// <param name="description">The description of this importer.</param>
    /// <param name="connectionManager">The connection manager for this importer.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this importer.</param>
    /// <param name="httpClient">The HTTP client.</param>
    public RestImporter(
        string importerId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<RestImporter> logger,
        RestImporterOptions options,
        HttpClient httpClient)
        : base(importerId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <inheritdoc />
    public override async Task<ImporterBase.ImportResult> ImportAsync(ImportContext importContext, CancellationToken cancellationToken = default)
    {
        if (importContext == null) throw new ArgumentNullException(nameof(importContext));

        _logger.LogInformation("Importing data from REST API for importer {ImporterId}", ImporterId);

        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot import data for importer {ImporterId} in status {Status}");
        }

        Status = ImporterStatus.Importing;

        try
        {
            var url = GetUrl(importContext);
            var method = GetMethod(importContext);
            var headers = GetHeaders(importContext);
            var body = GetBody(importContext);

            using var request = new HttpRequestMessage(method, url);

            // Add headers
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            // Add body if applicable
            if (method != HttpMethod.Get && method != HttpMethod.Head && !string.IsNullOrEmpty(body))
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            // Send request
            using var response = await _httpClient.SendAsync(request, cancellationToken);

            // Ensure success
            response.EnsureSuccessStatusCode();

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            // Parse response
            var data = JsonSerializer.Deserialize<object>(responseContent);

            var result = ImporterBase.ImportResult.Success(
                importContext.ImportId,
                1, // Records imported
                1, // Total records
                data);

            Status = ImporterStatus.Open;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing data from REST API for importer {ImporterId}", ImporterId);
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

        // For REST importer, we'll create a simple schema
        var schema = new DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<DataTable>
            {
                new DataTable
                {
                    Name = "RestData",
                    Description = "Data from the REST API",
                    Columns = new List<DataColumn>
                    {
                        new DataColumn
                        {
                            Name = "Content",
                            Description = "REST API response content",
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
    public override ImporterCapabilities GetCapabilities()
    {
        return new ImporterCapabilities
        {
            SupportsStreaming = false,
            SupportsBatching = false,
            SupportsFiltering = true,
            SupportsSorting = true,
            SupportsPagination = true,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalImport = true,
            SupportsParallelImport = false,
            SupportsResumeImport = false,
            SupportsAuthentication = true,
            SupportsEncryption = true,
            SupportsCompression = true,
            MaxBatchSize = 1,
            MaxParallelImports = 1,
            SupportedDataFormats = new List<string> { "json" },
            SupportedAuthenticationMethods = new List<string> { "none", "basic", "bearer", "api-key" },
            SupportedEncryptionMethods = new List<string> { "none", "ssl", "tls" },
            SupportedCompressionMethods = new List<string> { "none", "gzip" }
        };
    }

    private string GetUrl(ImportContext importContext)
    {
        // Check if the URL is specified in the import context
        if (importContext.Parameters.TryGetValue("Url", out var urlObj) && urlObj is string url)
        {
            return url;
        }

        // Otherwise, use the base URL from options and the endpoint from the import context
        if (importContext.Parameters.TryGetValue("Endpoint", out var endpointObj) && endpointObj is string endpoint)
        {
            return $"{_options.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
        }

        // If no endpoint is specified, use the base URL
        return _options.BaseUrl;
    }

    private HttpMethod GetMethod(ImportContext importContext)
    {
        // Check if the method is specified in the import context
        if (importContext.Parameters.TryGetValue("Method", out var methodObj) && methodObj is string method)
        {
            return new HttpMethod(method);
        }

        // Otherwise, use the default method from options
        return new HttpMethod(_options.DefaultMethod);
    }

    private Dictionary<string, string> GetHeaders(ImportContext importContext)
    {
        var headers = new Dictionary<string, string>();

        // Add default headers from options
        foreach (var header in _options.DefaultHeaders)
        {
            headers[header.Key] = header.Value;
        }

        // Add headers from import context
        if (importContext.Parameters.TryGetValue("Headers", out var headersObj) && headersObj is Dictionary<string, string> importHeaders)
        {
            foreach (var header in importHeaders)
            {
                headers[header.Key] = header.Value;
            }
        }

        return headers;
    }

    private string? GetBody(ImportContext importContext)
    {
        // Check if the body is specified in the import context
        if (importContext.Parameters.TryGetValue("Body", out var bodyObj))
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
