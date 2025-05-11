using FlowOrchestrator.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.FileExporter;

/// <summary>
/// Connection manager for file operations.
/// </summary>
public class FileConnectionManager : IConnectionManager
{
    private readonly ILogger<FileConnectionManager> _logger;
    private readonly FileExporterOptions _options;
    private bool _isOpen;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="FileConnectionManager"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public FileConnectionManager(ILogger<FileConnectionManager> logger, FileExporterOptions options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    /// <inheritdoc />
    public async Task OpenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Opening file connection");
        
        if (_isOpen)
        {
            _logger.LogWarning("File connection is already open");
            return;
        }
        
        // Ensure the base directory exists
        if (!string.IsNullOrEmpty(_options.BaseDirectory) && _options.CreateDirectoryIfNotExists)
        {
            Directory.CreateDirectory(_options.BaseDirectory);
        }
        
        // Ensure the backup directory exists if specified
        if (_options.BackupFileBeforeExport && !string.IsNullOrEmpty(_options.BackupDirectory) && _options.CreateDirectoryIfNotExists)
        {
            Directory.CreateDirectory(_options.BackupDirectory);
        }
        
        _isOpen = true;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing file connection");
        
        if (!_isOpen)
        {
            _logger.LogWarning("File connection is already closed");
            return;
        }
        
        _isOpen = false;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public async Task TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Testing file connection");
        
        // Test if the base directory exists or can be created
        if (!string.IsNullOrEmpty(_options.BaseDirectory))
        {
            if (Directory.Exists(_options.BaseDirectory))
            {
                // Test if we can write to the directory
                var testFilePath = Path.Combine(_options.BaseDirectory, $"test_{Guid.NewGuid()}.tmp");
                
                try
                {
                    await File.WriteAllTextAsync(testFilePath, "Test", cancellationToken);
                    File.Delete(testFilePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error writing to base directory {BaseDirectory}", _options.BaseDirectory);
                    throw new InvalidOperationException($"Cannot write to base directory {_options.BaseDirectory}", ex);
                }
            }
            else if (_options.CreateDirectoryIfNotExists)
            {
                try
                {
                    Directory.CreateDirectory(_options.BaseDirectory);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating base directory {BaseDirectory}", _options.BaseDirectory);
                    throw new InvalidOperationException($"Cannot create base directory {_options.BaseDirectory}", ex);
                }
            }
            else
            {
                _logger.LogError("Base directory {BaseDirectory} does not exist", _options.BaseDirectory);
                throw new InvalidOperationException($"Base directory {_options.BaseDirectory} does not exist");
            }
        }
        
        // Test if the backup directory exists or can be created
        if (_options.BackupFileBeforeExport && !string.IsNullOrEmpty(_options.BackupDirectory))
        {
            if (Directory.Exists(_options.BackupDirectory))
            {
                // Test if we can write to the directory
                var testFilePath = Path.Combine(_options.BackupDirectory, $"test_{Guid.NewGuid()}.tmp");
                
                try
                {
                    await File.WriteAllTextAsync(testFilePath, "Test", cancellationToken);
                    File.Delete(testFilePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error writing to backup directory {BackupDirectory}", _options.BackupDirectory);
                    throw new InvalidOperationException($"Cannot write to backup directory {_options.BackupDirectory}", ex);
                }
            }
            else if (_options.CreateDirectoryIfNotExists)
            {
                try
                {
                    Directory.CreateDirectory(_options.BackupDirectory);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating backup directory {BackupDirectory}", _options.BackupDirectory);
                    throw new InvalidOperationException($"Cannot create backup directory {_options.BackupDirectory}", ex);
                }
            }
            else
            {
                _logger.LogError("Backup directory {BackupDirectory} does not exist", _options.BackupDirectory);
                throw new InvalidOperationException($"Backup directory {_options.BackupDirectory} does not exist");
            }
        }
        
        await Task.CompletedTask;
    }
}
