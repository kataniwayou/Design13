namespace FlowOrchestrator.ExporterBase;

/// <summary>
/// Represents the capabilities of an exporter.
/// </summary>
public class ExporterCapabilities
{
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports streaming.
    /// </summary>
    public bool SupportsStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports batching.
    /// </summary>
    public bool SupportsBatching { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports filtering.
    /// </summary>
    public bool SupportsFiltering { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports sorting.
    /// </summary>
    public bool SupportsSorting { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports pagination.
    /// </summary>
    public bool SupportsPagination { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports schema discovery.
    /// </summary>
    public bool SupportsSchemaDiscovery { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports incremental exports.
    /// </summary>
    public bool SupportsIncrementalExport { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports parallel exports.
    /// </summary>
    public bool SupportsParallelExport { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports resuming exports.
    /// </summary>
    public bool SupportsResumeExport { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports authentication.
    /// </summary>
    public bool SupportsAuthentication { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports encryption.
    /// </summary>
    public bool SupportsEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports compression.
    /// </summary>
    public bool SupportsCompression { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports transactions.
    /// </summary>
    public bool SupportsTransactions { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports bulk operations.
    /// </summary>
    public bool SupportsBulkOperations { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports upserts.
    /// </summary>
    public bool SupportsUpserts { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the exporter supports deletes.
    /// </summary>
    public bool SupportsDeletes { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum batch size supported by the exporter.
    /// </summary>
    public int MaxBatchSize { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum parallel exports supported by the exporter.
    /// </summary>
    public int MaxParallelExports { get; set; }
    
    /// <summary>
    /// Gets or sets the supported data formats.
    /// </summary>
    public List<string> SupportedDataFormats { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported authentication methods.
    /// </summary>
    public List<string> SupportedAuthenticationMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported encryption methods.
    /// </summary>
    public List<string> SupportedEncryptionMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported compression methods.
    /// </summary>
    public List<string> SupportedCompressionMethods { get; set; } = new List<string>();
}
