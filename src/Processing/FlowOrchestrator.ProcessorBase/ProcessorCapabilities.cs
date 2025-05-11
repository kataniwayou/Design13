namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the capabilities of a processor.
/// </summary>
public class ProcessorCapabilities
{
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports validation.
    /// </summary>
    public bool SupportsValidation { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports transformation.
    /// </summary>
    public bool SupportsTransformation { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports mapping.
    /// </summary>
    public bool SupportsMapping { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports enrichment.
    /// </summary>
    public bool SupportsEnrichment { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports caching.
    /// </summary>
    public bool SupportsCache { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports parallel processing.
    /// </summary>
    public bool SupportsParallelProcessing { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports error recovery.
    /// </summary>
    public bool SupportsErrorRecovery { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports schema discovery.
    /// </summary>
    public bool SupportsSchemaDiscovery { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports streaming.
    /// </summary>
    public bool SupportsStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the processor supports batching.
    /// </summary>
    public bool SupportsBatching { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum batch size supported by the processor.
    /// </summary>
    public int MaxBatchSize { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum parallel processing tasks supported by the processor.
    /// </summary>
    public int MaxParallelProcessingTasks { get; set; }
    
    /// <summary>
    /// Gets or sets the supported input data types.
    /// </summary>
    public List<string> SupportedInputDataTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported output data types.
    /// </summary>
    public List<string> SupportedOutputDataTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported validation rule types.
    /// </summary>
    public List<string> SupportedValidationRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported transformation rule types.
    /// </summary>
    public List<string> SupportedTransformationRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported mapping rule types.
    /// </summary>
    public List<string> SupportedMappingRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported enrichment rule types.
    /// </summary>
    public List<string> SupportedEnrichmentRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported error recovery strategies.
    /// </summary>
    public List<string> SupportedErrorRecoveryStrategies { get; set; } = new List<string>();
}
