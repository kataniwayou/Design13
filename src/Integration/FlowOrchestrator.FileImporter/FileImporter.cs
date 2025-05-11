using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using FlowOrchestrator.ImporterBase;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.FileImporter;

/// <summary>
/// Importer for importing data from files.
/// </summary>
public class FileImporter : ImporterBase.ImporterBase
{
    private readonly ILogger<FileImporter> _logger;
    private readonly FileImporterOptions _options;

    /// <inheritdoc />
    public override string ImporterType => "File";

    /// <summary>
    /// Initializes a new instance of the <see cref="FileImporter"/> class.
    /// </summary>
    /// <param name="importerId">The unique identifier for this importer.</param>
    /// <param name="name">The name of this importer.</param>
    /// <param name="description">The description of this importer.</param>
    /// <param name="connectionManager">The connection manager for this importer.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this importer.</param>
    public FileImporter(
        string importerId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<FileImporter> logger,
        FileImporterOptions options)
        : base(importerId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public override async Task<ImporterBase.ImportResult> ImportAsync(ImportContext importContext, CancellationToken cancellationToken = default)
    {
        if (importContext == null) throw new ArgumentNullException(nameof(importContext));

        _logger.LogInformation("Importing data from file for importer {ImporterId}", ImporterId);

        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot import data for importer {ImporterId} in status {Status}");
        }

        Status = ImporterStatus.Importing;

        try
        {
            var filePath = GetFilePath(importContext);

            if (!File.Exists(filePath))
            {
                return ImporterBase.ImportResult.Failure(importContext.ImportId, $"File not found: {filePath}");
            }

            var fileInfo = new FileInfo(filePath);
            var fileExtension = fileInfo.Extension.ToLowerInvariant();

            object? data;

            switch (fileExtension)
            {
                case ".json":
                    data = await ImportJsonAsync(filePath, importContext, cancellationToken);
                    break;
                case ".csv":
                    data = await ImportCsvAsync(filePath, importContext, cancellationToken);
                    break;
                case ".xml":
                    data = await ImportXmlAsync(filePath, importContext, cancellationToken);
                    break;
                default:
                    data = await ImportTextAsync(filePath, importContext, cancellationToken);
                    break;
            }

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
            _logger.LogError(ex, "Error importing data from file for importer {ImporterId}", ImporterId);
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

        // For file importer, we'll create a simple schema based on the file type
        var schema = new DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<DataTable>
            {
                new DataTable
                {
                    Name = "FileData",
                    Description = "Data from the file",
                    Columns = new List<DataColumn>
                    {
                        new DataColumn
                        {
                            Name = "Content",
                            Description = "File content",
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
            SupportsStreaming = true,
            SupportsBatching = false,
            SupportsFiltering = false,
            SupportsSorting = false,
            SupportsPagination = false,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalImport = false,
            SupportsParallelImport = false,
            SupportsResumeImport = false,
            SupportsAuthentication = false,
            SupportsEncryption = true,
            SupportsCompression = true,
            MaxBatchSize = 1,
            MaxParallelImports = 1,
            SupportedDataFormats = new List<string> { "json", "csv", "xml", "text" },
            SupportedEncryptionMethods = new List<string> { "none" },
            SupportedCompressionMethods = new List<string> { "none", "gzip", "zip" }
        };
    }

    private string GetFilePath(ImportContext importContext)
    {
        // Check if the file path is specified in the import context
        if (importContext.Parameters.TryGetValue("FilePath", out var filePathObj) && filePathObj is string filePath)
        {
            return filePath;
        }

        // Otherwise, use the base directory from options and the file name from the import context
        if (importContext.Parameters.TryGetValue("FileName", out var fileNameObj) && fileNameObj is string fileName)
        {
            return Path.Combine(_options.BaseDirectory, fileName);
        }

        // If no file name is specified, use the import ID as the file name
        return Path.Combine(_options.BaseDirectory, $"{importContext.ImportId}.dat");
    }

    private async Task<object?> ImportJsonAsync(string filePath, ImportContext importContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Importing JSON data from file {FilePath}", filePath);

        var json = await File.ReadAllTextAsync(filePath, cancellationToken);
        return JsonSerializer.Deserialize<object>(json);
    }

    private async Task<object?> ImportCsvAsync(string filePath, ImportContext importContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Importing CSV data from file {FilePath}", filePath);

        var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);

        if (lines.Length == 0)
        {
            return new List<Dictionary<string, string>>();
        }

        var headers = lines[0].Split(',');
        var result = new List<Dictionary<string, string>>();

        for (var i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            var row = new Dictionary<string, string>();

            for (var j = 0; j < headers.Length && j < values.Length; j++)
            {
                row[headers[j]] = values[j];
            }

            result.Add(row);
        }

        return result;
    }

    private async Task<object?> ImportXmlAsync(string filePath, ImportContext importContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Importing XML data from file {FilePath}", filePath);

        var xml = await File.ReadAllTextAsync(filePath, cancellationToken);
        return xml;
    }

    private async Task<object?> ImportTextAsync(string filePath, ImportContext importContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Importing text data from file {FilePath}", filePath);

        var text = await File.ReadAllTextAsync(filePath, cancellationToken);
        return text;
    }
}
