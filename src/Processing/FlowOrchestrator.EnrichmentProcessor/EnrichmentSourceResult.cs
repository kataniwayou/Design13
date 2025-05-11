namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Represents the result of an enrichment operation.
/// </summary>
public class EnrichmentSourceResult
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
    /// Gets or sets the enriched data from the enrichment.
    /// </summary>
    public object? EnrichedData { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment rule that was applied.
    /// </summary>
    public EnrichmentRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment source that was used.
    /// </summary>
    public string? SourceName { get; set; }
    
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
    /// <param name="enrichedData">The enriched data from the enrichment.</param>
    /// <param name="rule">The enrichment rule that was applied.</param>
    /// <param name="sourceName">The enrichment source that was used.</param>
    /// <returns>A successful enrichment result.</returns>
    public static EnrichmentSourceResult Success(object? originalData, object? enrichedData, EnrichmentRule rule, string sourceName)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-100); // Simulate a 100ms enrichment
        var endTime = DateTime.UtcNow;
        
        return new EnrichmentSourceResult
        {
            IsSuccessful = true,
            OriginalData = originalData,
            EnrichedData = enrichedData,
            Rule = rule,
            SourceName = sourceName,
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
    /// <param name="rule">The enrichment rule that was applied.</param>
    /// <param name="sourceName">The enrichment source that was used.</param>
    /// <returns>A failed enrichment result.</returns>
    public static EnrichmentSourceResult Failure(string errorMessage, object? originalData, EnrichmentRule rule, string sourceName)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-100); // Simulate a 100ms enrichment
        var endTime = DateTime.UtcNow;
        
        return new EnrichmentSourceResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            OriginalData = originalData,
            Rule = rule,
            SourceName = sourceName,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
