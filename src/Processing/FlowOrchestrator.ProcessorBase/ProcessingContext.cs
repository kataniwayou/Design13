namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the context for a processing operation.
/// </summary>
public class ProcessingContext
{
    /// <summary>
    /// Gets or sets the unique identifier for this processing operation.
    /// </summary>
    public string ProcessingId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the input data for this processing operation.
    /// </summary>
    public object? InputData { get; set; }
    
    /// <summary>
    /// Gets or sets the input data type for this processing operation.
    /// </summary>
    public string? InputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the expected output data type for this processing operation.
    /// </summary>
    public string? OutputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the timeout for this processing operation in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 3600;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the input data before processing.
    /// </summary>
    public bool ValidateInput { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the output data after processing.
    /// </summary>
    public bool ValidateOutput { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the validation rules for the input data.
    /// </summary>
    public string? InputValidationRules { get; set; }
    
    /// <summary>
    /// Gets or sets the validation rules for the output data.
    /// </summary>
    public string? OutputValidationRules { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation rules for this processing operation.
    /// </summary>
    public string? TransformationRules { get; set; }
    
    /// <summary>
    /// Gets or sets the mapping rules for this processing operation.
    /// </summary>
    public string? MappingRules { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment rules for this processing operation.
    /// </summary>
    public string? EnrichmentRules { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use caching for this processing operation.
    /// </summary>
    public bool UseCache { get; set; }
    
    /// <summary>
    /// Gets or sets the cache key for this processing operation.
    /// </summary>
    public string? CacheKey { get; set; }
    
    /// <summary>
    /// Gets or sets the cache expiration in seconds.
    /// </summary>
    public int CacheExpirationSeconds { get; set; } = 3600;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use parallel processing for this operation.
    /// </summary>
    public bool UseParallelProcessing { get; set; }
    
    /// <summary>
    /// Gets or sets the number of parallel processing tasks to use.
    /// </summary>
    public int ParallelProcessingTasks { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use error recovery for this processing operation.
    /// </summary>
    public bool UseErrorRecovery { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the error recovery strategy for this processing operation.
    /// </summary>
    public string? ErrorRecoveryStrategy { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts for error recovery.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the additional parameters for this processing operation.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
