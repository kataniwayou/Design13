using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.ExporterBase;
using FlowOrchestrator.FileExporter;
using FlowOrchestrator.FileImporter;
using FlowOrchestrator.ImporterBase;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace FlowOrchestrator.Integration.Tests;

public class FileImporterExporterIntegrationTests
{
    private readonly Mock<ILogger<FileImporter>> _importerLoggerMock;
    private readonly Mock<ILogger<FileExporter>> _exporterLoggerMock;
    private readonly Mock<ILogger<FileConnectionManager>> _connectionManagerLoggerMock;
    private readonly string _testDirectory;
    private readonly string _importFilePath;
    private readonly string _exportFilePath;

    public FileImporterExporterIntegrationTests()
    {
        _importerLoggerMock = new Mock<ILogger<FileImporter>>();
        _exporterLoggerMock = new Mock<ILogger<FileExporter>>();
        _connectionManagerLoggerMock = new Mock<ILogger<FileConnectionManager>>();
        
        _testDirectory = Path.Combine(Path.GetTempPath(), "FileImporterExporterTests");
        Directory.CreateDirectory(_testDirectory);
        
        _importFilePath = Path.Combine(_testDirectory, "import.json");
        _exportFilePath = Path.Combine(_testDirectory, "export.json");
    }

    [Fact]
    public async Task ImportAndExport_WithJsonFile_SuccessfullyTransfersData()
    {
        // Arrange
        var testData = new { Name = "Test", Value = 123 };
        var json = JsonSerializer.Serialize(testData);
        await File.WriteAllTextAsync(_importFilePath, json);

        // Create importer
        var importerOptions = new FileImporterOptions
        {
            BaseDirectory = _testDirectory,
            CreateDirectoryIfNotExists = true,
            DefaultEncoding = "utf-8",
            DefaultFileExtension = ".json"
        };

        var importerConnectionManager = new FileConnectionManager(
            _connectionManagerLoggerMock.Object,
            importerOptions);

        var importer = new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for integration tests",
            importerConnectionManager,
            _importerLoggerMock.Object,
            importerOptions);

        // Create exporter
        var exporterOptions = new FileExporterOptions
        {
            BaseDirectory = _testDirectory,
            CreateDirectoryIfNotExists = true,
            DefaultEncoding = "utf-8",
            DefaultFileExtension = ".json",
            OverwriteExistingFiles = true
        };

        var exporterConnectionManager = new FileConnectionManager(
            _connectionManagerLoggerMock.Object,
            exporterOptions);

        var exporter = new FileExporter(
            "test-exporter",
            "Test Exporter",
            "Test exporter for integration tests",
            exporterConnectionManager,
            _exporterLoggerMock.Object,
            exporterOptions);

        // Open connections
        await importerConnectionManager.OpenAsync();
        await exporterConnectionManager.OpenAsync();

        // Set the status to Open for both importer and exporter
        var importerField = typeof(ImporterBase.ImporterBase).GetField("_status", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        importerField?.SetValue(importer, ImporterStatus.Open);

        var exporterField = typeof(ExporterBase.ExporterBase).GetField("_status", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        exporterField?.SetValue(exporter, ExporterStatus.Open);

        // Create import context
        var importContext = new ImportContext
        {
            ImportId = "test-import",
            Parameters = new Dictionary<string, object>
            {
                { "FilePath", _importFilePath }
            }
        };

        // Act
        // Import data
        var importResult = await importer.ImportAsync(importContext);

        // Create export context with the imported data
        var exportContext = new ExportContext
        {
            ExportId = "test-export",
            Data = importResult.Data,
            Parameters = new Dictionary<string, object>
            {
                { "FilePath", _exportFilePath }
            }
        };

        // Export data
        var exportResult = await exporter.ExportAsync(exportContext);

        // Assert
        Assert.NotNull(importResult);
        Assert.True(importResult.IsSuccessful);
        Assert.Equal("test-import", importResult.ImportId);
        Assert.Equal(1, importResult.RecordsImported);
        Assert.Equal(1, importResult.TotalRecords);
        Assert.NotNull(importResult.Data);

        Assert.NotNull(exportResult);
        Assert.True(exportResult.IsSuccessful);
        Assert.Equal("test-export", exportResult.ExportId);
        Assert.Equal(1, exportResult.RecordsExported);
        Assert.Equal(1, exportResult.TotalRecords);

        // Verify the exported file
        Assert.True(File.Exists(_exportFilePath));
        var exportedJson = await File.ReadAllTextAsync(_exportFilePath);
        var exportedData = JsonSerializer.Deserialize<JsonElement>(exportedJson);
        
        Assert.Equal("Test", exportedData.GetProperty("Name").GetString());
        Assert.Equal(123, exportedData.GetProperty("Value").GetInt32());

        // Clean up
        await importerConnectionManager.CloseAsync();
        await exporterConnectionManager.CloseAsync();
    }
}
