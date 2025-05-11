namespace FlowOrchestrator.FileImporter;

/// <summary>
/// Options for the file importer.
/// </summary>
public class FileImporterOptions
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
    /// Gets or sets a value indicating whether to delete the file after importing.
    /// </summary>
    public bool DeleteFileAfterImport { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to move the file after importing.
    /// </summary>
    public bool MoveFileAfterImport { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the directory to move the file to after importing.
    /// </summary>
    public string? MoveToDirectory { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to backup the file before importing.
    /// </summary>
    public bool BackupFileBeforeImport { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the directory to backup the file to before importing.
    /// </summary>
    public string? BackupDirectory { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to watch for file changes.
    /// </summary>
    public bool WatchForFileChanges { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the file watch pattern.
    /// </summary>
    public string FileWatchPattern { get; set; } = "*.*";
    
    /// <summary>
    /// Gets or sets a value indicating whether to include subdirectories when watching for file changes.
    /// </summary>
    public bool IncludeSubdirectories { get; set; } = false;
}
