namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the result of an enrichment operation.
/// </summary>
public class EnrichmentResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the enrichment failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the original data that was enriched.
    /// </summary>
    public object? OriginalData { get; set; }
    
    /// <summary>
    /// Gets or sets the original data type that was enriched.
    /// </summary>
    public string? OriginalDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the enriched data from the enrichment.
    /// </summary>
    public object? EnrichedData { get; set; }
    
    /// <summary>
    /// Gets or sets the enriched data type from the enrichment.
    /// </summary>
    public string? EnrichedDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment rule that was applied.
    /// </summary>
    public string? EnrichmentRule { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment rule type that was applied.
    /// </summary>
    public string? EnrichmentRuleType { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment sources that were used.
    /// </summary>
    public List<string> EnrichmentSources { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the start time of the enrichment operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the enrichment operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the enrichment operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the enrichment operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the enrichment operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful enrichment result.
    /// </summary>
    /// <param name="originalData">The original data that was enriched.</param>
    /// <param name="originalDataType">The original data type that was enriched.</param>
    /// <param name="enrichedData">The enriched data from the enrichment.</param>
    /// <param name="enrichedDataType">The enriched data type from the enrichment.</param>
    /// <param name="enrichmentRule">The enrichment rule that was applied.</param>
    /// <param name="enrichmentRuleType">The enrichment rule type that was applied.</param>
    /// <param name="enrichmentSources">The enrichment sources that were used.</param>
    /// <returns>A successful enrichment result.</returns>
    public static EnrichmentResult Success(object? originalData, string? originalDataType, object? enrichedData, string? enrichedDataType, string? enrichmentRule = null, string? enrichmentRuleType = null, List<string>? enrichmentSources = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-250); // Simulate a 250ms enrichment
        var endTime = DateTime.UtcNow;
        
        return new EnrichmentResult
        {
            IsSuccessful = true,
            OriginalData = originalData,
            OriginalDataType = originalDataType,
            EnrichedData = enrichedData,
            EnrichedDataType = enrichedDataType,
            EnrichmentRule = enrichmentRule,
            EnrichmentRuleType = enrichmentRuleType,
            EnrichmentSources = enrichmentSources ?? new List<string>(),
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
    
    /// <summary>
    /// Creates a failed enrichment result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="originalData">The original data that was enriched.</param>
    /// <param name="originalDataType">The original data type that was enriched.</param>
    /// <param name="enrichmentRule">The enrichment rule that was applied.</param>
    /// <param name="enrichmentRuleType">The enrichment rule type that was applied.</param>
    /// <param name="enrichmentSources">The enrichment sources that were used.</param>
    /// <returns>A failed enrichment result.</returns>
    public static EnrichmentResult Failure(string errorMessage, object? originalData, string? originalDataType, string? enrichmentRule = null, string? enrichmentRuleType = null, List<string>? enrichmentSources = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-250); // Simulate a 250ms enrichment
        var endTime = DateTime.UtcNow;
        
        return new EnrichmentResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            OriginalData = originalData,
            OriginalDataType = originalDataType,
            EnrichmentRule = enrichmentRule,
            EnrichmentRuleType = enrichmentRuleType,
            EnrichmentSources = enrichmentSources ?? new List<string>(),
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
