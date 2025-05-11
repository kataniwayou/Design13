namespace FlowOrchestrator.ImporterBase;

/// <summary>
/// Represents the context for an import operation.
/// </summary>
public class ImportContext
{
    /// <summary>
    /// Gets or sets the unique identifier for this import operation.
    /// </summary>
    public string ImportId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the batch size for this import operation.
    /// </summary>
    public int BatchSize { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets the maximum number of records to import.
    /// </summary>
    public int? MaxRecords { get; set; }
    
    /// <summary>
    /// Gets or sets the timeout for this import operation in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 3600;
    
    /// <summary>
    /// Gets or sets the filter for this import operation.
    /// </summary>
    public string? Filter { get; set; }
    
    /// <summary>
    /// Gets or sets the sort for this import operation.
    /// </summary>
    public string? Sort { get; set; }
    
    /// <summary>
    /// Gets or sets the page number for this import operation.
    /// </summary>
    public int? PageNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the page size for this import operation.
    /// </summary>
    public int? PageSize { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use streaming for this import operation.
    /// </summary>
    public bool UseStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use incremental import for this import operation.
    /// </summary>
    public bool UseIncrementalImport { get; set; }
    
    /// <summary>
    /// Gets or sets the last import timestamp for incremental imports.
    /// </summary>
    public DateTime? LastImportTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use parallel import for this import operation.
    /// </summary>
    public bool UseParallelImport { get; set; }
    
    /// <summary>
    /// Gets or sets the number of parallel imports to use.
    /// </summary>
    public int ParallelImports { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets a value indicating whether to resume a previous import operation.
    /// </summary>
    public bool ResumeImport { get; set; }
    
    /// <summary>
    /// Gets or sets the resume token for resuming a previous import operation.
    /// </summary>
    public string? ResumeToken { get; set; }
    
    /// <summary>
    /// Gets or sets the data format for this import operation.
    /// </summary>
    public string? DataFormat { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use compression for this import operation.
    /// </summary>
    public bool UseCompression { get; set; }
    
    /// <summary>
    /// Gets or sets the compression method to use.
    /// </summary>
    public string? CompressionMethod { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use encryption for this import operation.
    /// </summary>
    public bool UseEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption method to use.
    /// </summary>
    public string? EncryptionMethod { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this import operation.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
