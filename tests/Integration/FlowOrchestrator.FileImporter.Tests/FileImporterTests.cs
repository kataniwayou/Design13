using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.ImporterBase;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace FlowOrchestrator.FileImporter.Tests;

public class FileImporterTests
{
    private readonly Mock<ILogger<FileImporter>> _loggerMock;
    private readonly Mock<IConnectionManager> _connectionManagerMock;
    private readonly FileImporterOptions _options;
    private readonly string _testDirectory;
    private readonly string _testFilePath;

    public FileImporterTests()
    {
        _loggerMock = new Mock<ILogger<FileImporter>>();
        _connectionManagerMock = new Mock<IConnectionManager>();
        
        _testDirectory = Path.Combine(Path.GetTempPath(), "FileImporterTests");
        Directory.CreateDirectory(_testDirectory);
        
        _testFilePath = Path.Combine(_testDirectory, "test.json");
        
        _options = new FileImporterOptions
        {
            BaseDirectory = _testDirectory,
            CreateDirectoryIfNotExists = true,
            DefaultEncoding = "utf-8",
            DefaultFileExtension = ".json"
        };
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var importer = new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options);

        // Assert
        Assert.NotNull(importer);
        Assert.Equal("test-importer", importer.ImporterId);
        Assert.Equal("Test Importer", importer.Name);
        Assert.Equal("Test importer for unit tests", importer.Description);
        Assert.Equal("File", importer.ImporterType);
        Assert.Equal(ImporterStatus.Created, importer.Status);
    }

    [Fact]
    public void Constructor_WithNullParameters_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new FileImporter(
            null!,
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options));

        Assert.Throws<ArgumentNullException>(() => new FileImporter(
            "test-importer",
            null!,
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options));

        Assert.Throws<ArgumentNullException>(() => new FileImporter(
            "test-importer",
            "Test Importer",
            null!,
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options));

        Assert.Throws<ArgumentNullException>(() => new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            null!,
            _loggerMock.Object,
            _options));

        Assert.Throws<ArgumentNullException>(() => new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            null!,
            _options));

        Assert.Throws<ArgumentNullException>(() => new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            null!));
    }

    [Fact]
    public async Task ImportAsync_WithJsonFile_ReturnsSuccessResult()
    {
        // Arrange
        var testData = new { Name = "Test", Value = 123 };
        var json = JsonSerializer.Serialize(testData);
        await File.WriteAllTextAsync(_testFilePath, json);

        var importer = new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options);

        // Set the status to Open
        var field = typeof(ImporterBase.ImporterBase).GetField("_status", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field?.SetValue(importer, ImporterStatus.Open);

        var importContext = new ImportContext
        {
            ImportId = "test-import",
            Parameters = new Dictionary<string, object>
            {
                { "FilePath", _testFilePath }
            }
        };

        // Act
        var result = await importer.ImportAsync(importContext);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccessful);
        Assert.Equal("test-import", result.ImportId);
        Assert.Equal(1, result.RecordsImported);
        Assert.Equal(1, result.TotalRecords);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void GetCapabilities_ReturnsExpectedCapabilities()
    {
        // Arrange
        var importer = new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options);

        // Act
        var capabilities = importer.GetCapabilities();

        // Assert
        Assert.NotNull(capabilities);
        Assert.True(capabilities.SupportsStreaming);
        Assert.False(capabilities.SupportsBatching);
        Assert.False(capabilities.SupportsFiltering);
        Assert.False(capabilities.SupportsSorting);
        Assert.False(capabilities.SupportsPagination);
        Assert.True(capabilities.SupportsSchemaDiscovery);
        Assert.False(capabilities.SupportsIncrementalImport);
        Assert.False(capabilities.SupportsParallelImport);
        Assert.False(capabilities.SupportsResumeImport);
        Assert.False(capabilities.SupportsAuthentication);
        Assert.True(capabilities.SupportsEncryption);
        Assert.True(capabilities.SupportsCompression);
        Assert.Equal(1, capabilities.MaxBatchSize);
        Assert.Equal(1, capabilities.MaxParallelImports);
        Assert.Contains("json", capabilities.SupportedDataFormats);
        Assert.Contains("csv", capabilities.SupportedDataFormats);
        Assert.Contains("xml", capabilities.SupportedDataFormats);
        Assert.Contains("text", capabilities.SupportedDataFormats);
    }

    [Fact]
    public async Task GetSchemaAsync_ReturnsExpectedSchema()
    {
        // Arrange
        var importer = new FileImporter(
            "test-importer",
            "Test Importer",
            "Test importer for unit tests",
            _connectionManagerMock.Object,
            _loggerMock.Object,
            _options);

        // Set the status to Open
        var field = typeof(ImporterBase.ImporterBase).GetField("_status", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field?.SetValue(importer, ImporterStatus.Open);

        // Act
        var schema = await importer.GetSchemaAsync();

        // Assert
        Assert.NotNull(schema);
        Assert.Equal($"{importer.Name} Schema", schema.Name);
        Assert.Equal($"Schema for {importer.Description}", schema.Description);
        Assert.Equal("1.0.0", schema.Version);
        Assert.Single(schema.Tables);
        
        var table = schema.Tables[0];
        Assert.Equal("FileData", table.Name);
        Assert.Equal("Data from the file", table.Description);
        Assert.Single(table.Columns);
        
        var column = table.Columns[0];
        Assert.Equal("Content", column.Name);
        Assert.Equal("File content", column.Description);
        Assert.Equal("string", column.DataType);
        Assert.False(column.IsNullable);
    }
}
