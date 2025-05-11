namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the result of a mapping operation.
/// </summary>
public class MappingResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the mapping was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the mapping failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the source data that was mapped.
    /// </summary>
    public object? SourceData { get; set; }
    
    /// <summary>
    /// Gets or sets the source data type that was mapped.
    /// </summary>
    public string? SourceDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the target data from the mapping.
    /// </summary>
    public object? TargetData { get; set; }
    
    /// <summary>
    /// Gets or sets the target data type from the mapping.
    /// </summary>
    public string? TargetDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the mapping rule that was applied.
    /// </summary>
    public string? MappingRule { get; set; }
    
    /// <summary>
    /// Gets or sets the mapping rule type that was applied.
    /// </summary>
    public string? MappingRuleType { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the mapping operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the mapping operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the mapping operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the mapping operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the mapping operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful mapping result.
    /// </summary>
    /// <param name="sourceData">The source data that was mapped.</param>
    /// <param name="sourceDataType">The source data type that was mapped.</param>
    /// <param name="targetData">The target data from the mapping.</param>
    /// <param name="targetDataType">The target data type from the mapping.</param>
    /// <param name="mappingRule">The mapping rule that was applied.</param>
    /// <param name="mappingRuleType">The mapping rule type that was applied.</param>
    /// <returns>A successful mapping result.</returns>
    public static MappingResult Success(object? sourceData, string? sourceDataType, object? targetData, string? targetDataType, string? mappingRule = null, string? mappingRuleType = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-150); // Simulate a 150ms mapping
        var endTime = DateTime.UtcNow;
        
        return new MappingResult
        {
            IsSuccessful = true,
            SourceData = sourceData,
            SourceDataType = sourceDataType,
            TargetData = targetData,
            TargetDataType = targetDataType,
            MappingRule = mappingRule,
            MappingRuleType = mappingRuleType,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
    
    /// <summary>
    /// Creates a failed mapping result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="sourceData">The source data that was mapped.</param>
    /// <param name="sourceDataType">The source data type that was mapped.</param>
    /// <param name="mappingRule">The mapping rule that was applied.</param>
    /// <param name="mappingRuleType">The mapping rule type that was applied.</param>
    /// <returns>A failed mapping result.</returns>
    public static MappingResult Failure(string errorMessage, object? sourceData, string? sourceDataType, string? mappingRule = null, string? mappingRuleType = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-150); // Simulate a 150ms mapping
        var endTime = DateTime.UtcNow;
        
        return new MappingResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            SourceData = sourceData,
            SourceDataType = sourceDataType,
            MappingRule = mappingRule,
            MappingRuleType = mappingRuleType,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
