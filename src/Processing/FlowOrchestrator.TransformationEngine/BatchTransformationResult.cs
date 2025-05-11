using FlowOrchestrator.ProcessorBase;

namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a batch transformation operation.
/// </summary>
public class BatchTransformationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the batch transformation was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the batch transformation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the individual transformation results.
    /// </summary>
    public List<TransformationResult> Results { get; set; } = new List<TransformationResult>();
    
    /// <summary>
    /// Gets or sets the number of successful transformations.
    /// </summary>
    public int SuccessCount { get; set; }
    
    /// <summary>
    /// Gets or sets the number of failed transformations.
    /// </summary>
    public int FailureCount { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of transformations.
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation rule that was applied.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the batch transformation operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the batch transformation operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the batch transformation operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the batch transformation operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the batch transformation operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful batch transformation result.
    /// </summary>
    /// <param name="results">The individual transformation results.</param>
    /// <param name="rule">The transformation rule that was applied.</param>
    /// <param name="startTime">The start time of the batch transformation operation.</param>
    /// <param name="endTime">The end time of the batch transformation operation.</param>
    /// <param name="durationMs">The duration of the batch transformation operation in milliseconds.</param>
    /// <returns>A successful batch transformation result.</returns>
    public static BatchTransformationResult Success(List<TransformationResult> results, TransformationRule rule, DateTime startTime, DateTime endTime, long durationMs)
    {
        var successCount = results.Count(r => r.IsSuccessful);
        var failureCount = results.Count - successCount;
        
        return new BatchTransformationResult
        {
            IsSuccessful = failureCount == 0,
            Results = results,
            SuccessCount = successCount,
            FailureCount = failureCount,
            TotalCount = results.Count,
            Rule = rule,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = durationMs
        };
    }
    
    /// <summary>
    /// Creates a failed batch transformation result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="inputs">The input data packages.</param>
    /// <param name="rule">The transformation rule that was applied.</param>
    /// <param name="results">The individual transformation results that were completed before the failure.</param>
    /// <param name="startTime">The start time of the batch transformation operation.</param>
    /// <param name="endTime">The end time of the batch transformation operation.</param>
    /// <param name="durationMs">The duration of the batch transformation operation in milliseconds.</param>
    /// <returns>A failed batch transformation result.</returns>
    public static BatchTransformationResult Failure(string errorMessage, List<DataPackage> inputs, TransformationRule rule, List<TransformationResult>? results = null, DateTime? startTime = null, DateTime? endTime = null, long? durationMs = null)
    {
        var actualStartTime = startTime ?? DateTime.UtcNow.AddSeconds(-1);
        var actualEndTime = endTime ?? DateTime.UtcNow;
        var actualDurationMs = durationMs ?? (long)(actualEndTime - actualStartTime).TotalMilliseconds;
        var actualResults = results ?? new List<TransformationResult>();
        
        return new BatchTransformationResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Results = actualResults,
            SuccessCount = actualResults.Count(r => r.IsSuccessful),
            FailureCount = actualResults.Count(r => !r.IsSuccessful) + (inputs.Count - actualResults.Count),
            TotalCount = inputs.Count,
            Rule = rule,
            StartTime = actualStartTime,
            EndTime = actualEndTime,
            DurationMs = actualDurationMs
        };
    }
}
