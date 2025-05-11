using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using FlowOrchestrator.ExporterBase;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.FileExporter;

/// <summary>
/// Exporter for exporting data to files.
/// </summary>
public class FileExporter : ExporterBase.ExporterBase
{
    private readonly ILogger<FileExporter> _logger;
    private readonly FileExporterOptions _options;

    /// <inheritdoc />
    public override string ExporterType => "File";

    /// <summary>
    /// Initializes a new instance of the <see cref="FileExporter"/> class.
    /// </summary>
    /// <param name="exporterId">The unique identifier for this exporter.</param>
    /// <param name="name">The name of this exporter.</param>
    /// <param name="description">The description of this exporter.</param>
    /// <param name="connectionManager">The connection manager for this exporter.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this exporter.</param>
    public FileExporter(
        string exporterId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<FileExporter> logger,
        FileExporterOptions options)
        : base(exporterId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public override async Task<ExporterBase.ExportResult> ExportAsync(ExportContext exportContext, CancellationToken cancellationToken = default)
    {
        if (exportContext == null) throw new ArgumentNullException(nameof(exportContext));

        _logger.LogInformation("Exporting data to file for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot export data for exporter {ExporterId} in status {Status}");
        }

        Status = ExporterStatus.Exporting;

        try
        {
            var filePath = GetFilePath(exportContext);
            var fileInfo = new FileInfo(filePath);
            var fileExtension = fileInfo.Extension.ToLowerInvariant();

            // Create the directory if it doesn't exist
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Backup the file if it exists and backup is enabled
            if (File.Exists(filePath) && _options.BackupFileBeforeExport && !string.IsNullOrEmpty(_options.BackupDirectory))
            {
                var backupFilePath = Path.Combine(_options.BackupDirectory, $"{Path.GetFileName(filePath)}.{DateTime.Now:yyyyMMddHHmmss}.bak");
                File.Copy(filePath, backupFilePath, true);
            }

            // Export the data based on the file extension
            switch (fileExtension)
            {
                case ".json":
                    await ExportJsonAsync(filePath, exportContext, cancellationToken);
                    break;
                case ".csv":
                    await ExportCsvAsync(filePath, exportContext, cancellationToken);
                    break;
                case ".xml":
                    await ExportXmlAsync(filePath, exportContext, cancellationToken);
                    break;
                default:
                    await ExportTextAsync(filePath, exportContext, cancellationToken);
                    break;
            }

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
            _logger.LogError(ex, "Error exporting data to file for exporter {ExporterId}", ExporterId);
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

        // For file exporter, we'll create a simple schema based on the file type
        var schema = new DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<Domain.Entities.DataTable>
            {
                new Domain.Entities.DataTable
                {
                    Name = "FileData",
                    Description = "Data to export to the file",
                    Columns = new List<Domain.Entities.DataColumn>
                    {
                        new Domain.Entities.DataColumn
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
    public override ExporterCapabilities GetCapabilities()
    {
        return new ExporterCapabilities
        {
            SupportsStreaming = true,
            SupportsBatching = false,
            SupportsFiltering = false,
            SupportsSorting = false,
            SupportsPagination = false,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalExport = false,
            SupportsParallelExport = false,
            SupportsResumeExport = false,
            SupportsAuthentication = false,
            SupportsEncryption = true,
            SupportsCompression = true,
            SupportsTransactions = false,
            SupportsBulkOperations = false,
            SupportsUpserts = false,
            SupportsDeletes = false,
            MaxBatchSize = 1,
            MaxParallelExports = 1,
            SupportedDataFormats = new List<string> { "json", "csv", "xml", "text" },
            SupportedEncryptionMethods = new List<string> { "none" },
            SupportedCompressionMethods = new List<string> { "none", "gzip", "zip" }
        };
    }

    private string GetFilePath(ExportContext exportContext)
    {
        // Check if the file path is specified in the export context
        if (exportContext.Parameters.TryGetValue("FilePath", out var filePathObj) && filePathObj is string filePath)
        {
            return filePath;
        }

        // Otherwise, use the base directory from options and the file name from the export context
        if (exportContext.Parameters.TryGetValue("FileName", out var fileNameObj) && fileNameObj is string fileName)
        {
            return Path.Combine(_options.BaseDirectory, fileName);
        }

        // If no file name is specified, use the export ID as the file name
        return Path.Combine(_options.BaseDirectory, $"{exportContext.ExportId}.dat");
    }

    private async Task ExportJsonAsync(string filePath, ExportContext exportContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exporting JSON data to file {FilePath}", filePath);

        if (exportContext.Data == null)
        {
            await File.WriteAllTextAsync(filePath, "{}", cancellationToken);
            return;
        }

        var json = JsonSerializer.Serialize(exportContext.Data, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(filePath, json, cancellationToken);
    }

    private async Task ExportCsvAsync(string filePath, ExportContext exportContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exporting CSV data to file {FilePath}", filePath);

        if (exportContext.Data == null)
        {
            await File.WriteAllTextAsync(filePath, string.Empty, cancellationToken);
            return;
        }

        if (exportContext.Data is IEnumerable<IDictionary<string, object>> dictionaries)
        {
            var headers = dictionaries.SelectMany(d => d.Keys).Distinct().ToList();
            var lines = new List<string> { string.Join(",", headers) };

            foreach (var dictionary in dictionaries)
            {
                var values = headers.Select(h => dictionary.TryGetValue(h, out var value) ? value?.ToString() ?? string.Empty : string.Empty);
                lines.Add(string.Join(",", values));
            }

            await File.WriteAllLinesAsync(filePath, lines, cancellationToken);
        }
        else
        {
            await File.WriteAllTextAsync(filePath, exportContext.Data.ToString() ?? string.Empty, cancellationToken);
        }
    }

    private async Task ExportXmlAsync(string filePath, ExportContext exportContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exporting XML data to file {FilePath}", filePath);

        if (exportContext.Data == null)
        {
            await File.WriteAllTextAsync(filePath, "<root></root>", cancellationToken);
            return;
        }

        // For simplicity, we'll just convert the data to a string
        await File.WriteAllTextAsync(filePath, exportContext.Data.ToString() ?? string.Empty, cancellationToken);
    }

    private async Task ExportTextAsync(string filePath, ExportContext exportContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exporting text data to file {FilePath}", filePath);

        if (exportContext.Data == null)
        {
            await File.WriteAllTextAsync(filePath, string.Empty, cancellationToken);
            return;
        }

        await File.WriteAllTextAsync(filePath, exportContext.Data.ToString() ?? string.Empty, cancellationToken);
    }
}
