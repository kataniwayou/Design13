namespace FlowOrchestrator.FileExporter;

/// <summary>
/// Options for the file exporter.
/// </summary>
public class FileExporterOptions
{
    /// <summary>
    /// Gets or sets the base directory for file operations.
    /// </summary>
    public string BaseDirectory { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether to create the directory if it doesn't exist.
    /// </summary>
    public bool CreateDirectoryIfNotExists { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use relative paths.
    /// </summary>
    public bool UseRelativePaths { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the default encoding to use for text files.
    /// </summary>
    public string DefaultEncoding { get; set; } = "utf-8";
    
    /// <summary>
    /// Gets or sets the default file extension to use when none is specified.
    /// </summary>
    public string DefaultFileExtension { get; set; } = ".dat";
    
    /// <summary>
    /// Gets or sets a value indicating whether to overwrite existing files.
    /// </summary>
    public bool OverwriteExistingFiles { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to append to existing files.
    /// </summary>
    public bool AppendToExistingFiles { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to backup the file before exporting.
    /// </summary>
    public bool BackupFileBeforeExport { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the directory to backup the file to before exporting.
    /// </summary>
    public string? BackupDirectory { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to create a new file with a timestamp if the file already exists.
    /// </summary>
    public bool CreateNewFileWithTimestamp { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the timestamp format to use when creating a new file with a timestamp.
    /// </summary>
    public string TimestampFormat { get; set; } = "yyyyMMddHHmmss";
}
