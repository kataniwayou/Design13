namespace FlowOrchestrator.ImporterBase;

/// <summary>
/// Represents the capabilities of an importer.
/// </summary>
public class ImporterCapabilities
{
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports streaming.
    /// </summary>
    public bool SupportsStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports batching.
    /// </summary>
    public bool SupportsBatching { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports filtering.
    /// </summary>
    public bool SupportsFiltering { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports sorting.
    /// </summary>
    public bool SupportsSorting { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports pagination.
    /// </summary>
    public bool SupportsPagination { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports schema discovery.
    /// </summary>
    public bool SupportsSchemaDiscovery { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports incremental imports.
    /// </summary>
    public bool SupportsIncrementalImport { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports parallel imports.
    /// </summary>
    public bool SupportsParallelImport { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports resuming imports.
    /// </summary>
    public bool SupportsResumeImport { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports authentication.
    /// </summary>
    public bool SupportsAuthentication { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports encryption.
    /// </summary>
    public bool SupportsEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the importer supports compression.
    /// </summary>
    public bool SupportsCompression { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum batch size supported by the importer.
    /// </summary>
    public int MaxBatchSize { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum parallel imports supported by the importer.
    /// </summary>
    public int MaxParallelImports { get; set; }
    
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
