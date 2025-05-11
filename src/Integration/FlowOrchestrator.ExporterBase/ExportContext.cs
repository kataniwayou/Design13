namespace FlowOrchestrator.ExporterBase;

/// <summary>
/// Represents the context for an export operation.
/// </summary>
public class ExportContext
{
    /// <summary>
    /// Gets or sets the unique identifier for this export operation.
    /// </summary>
    public string ExportId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the batch size for this export operation.
    /// </summary>
    public int BatchSize { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets the timeout for this export operation in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 3600;
    
    /// <summary>
    /// Gets or sets the filter for this export operation.
    /// </summary>
    public string? Filter { get; set; }
    
    /// <summary>
    /// Gets or sets the sort for this export operation.
    /// </summary>
    public string? Sort { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use streaming for this export operation.
    /// </summary>
    public bool UseStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use incremental export for this export operation.
    /// </summary>
    public bool UseIncrementalExport { get; set; }
    
    /// <summary>
    /// Gets or sets the last export timestamp for incremental exports.
    /// </summary>
    public DateTime? LastExportTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use parallel export for this export operation.
    /// </summary>
    public bool UseParallelExport { get; set; }
    
    /// <summary>
    /// Gets or sets the number of parallel exports to use.
    /// </summary>
    public int ParallelExports { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets a value indicating whether to resume a previous export operation.
    /// </summary>
    public bool ResumeExport { get; set; }
    
    /// <summary>
    /// Gets or sets the resume token for resuming a previous export operation.
    /// </summary>
    public string? ResumeToken { get; set; }
    
    /// <summary>
    /// Gets or sets the data format for this export operation.
    /// </summary>
    public string? DataFormat { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use compression for this export operation.
    /// </summary>
    public bool UseCompression { get; set; }
    
    /// <summary>
    /// Gets or sets the compression method to use.
    /// </summary>
    public string? CompressionMethod { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use encryption for this export operation.
    /// </summary>
    public bool UseEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption method to use.
    /// </summary>
    public string? EncryptionMethod { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use transactions for this export operation.
    /// </summary>
    public bool UseTransactions { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use bulk operations for this export operation.
    /// </summary>
    public bool UseBulkOperations { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use upserts for this export operation.
    /// </summary>
    public bool UseUpserts { get; set; }
    
    /// <summary>
    /// Gets or sets the data to export.
    /// </summary>
    public object? Data { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this export operation.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
