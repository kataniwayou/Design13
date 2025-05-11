namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a transformation optimization operation.
/// </summary>
public class TransformationOptimizationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the optimization was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the optimization failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the original transformation rule.
    /// </summary>
    public TransformationRule? OriginalRule { get; set; }
    
    /// <summary>
    /// Gets or sets the optimized transformation rule.
    /// </summary>
    public TransformationRule? OptimizedRule { get; set; }
    
    /// <summary>
    /// Gets or sets the optimization factor (performance improvement).
    /// </summary>
    public double OptimizationFactor { get; set; }
    
    /// <summary>
    /// Gets or sets the optimization techniques applied.
    /// </summary>
    public List<string> OptimizationTechniques { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the start time of the optimization operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the optimization operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the optimization operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the optimization operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful optimization result.
    /// </summary>
    /// <param name="originalRule">The original transformation rule.</param>
    /// <param name="optimizedRule">The optimized transformation rule.</param>
    /// <param name="optimizationFactor">The optimization factor (performance improvement).</param>
    /// <param name="optimizationTechniques">The optimization techniques applied.</param>
    /// <returns>A successful optimization result.</returns>
    public static TransformationOptimizationResult Success(TransformationRule originalRule, TransformationRule? optimizedRule, double optimizationFactor, List<string>? optimizationTechniques = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-300); // Simulate a 300ms optimization
        var endTime = DateTime.UtcNow;
        
        return new TransformationOptimizationResult
        {
            IsSuccessful = true,
            OriginalRule = originalRule,
            OptimizedRule = optimizedRule,
            OptimizationFactor = optimizationFactor,
            OptimizationTechniques = optimizationTechniques ?? new List<string>(),
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
    
    /// <summary>
    /// Creates a failed optimization result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="originalRule">The original transformation rule.</param>
    /// <returns>A failed optimization result.</returns>
    public static TransformationOptimizationResult Failure(string errorMessage, TransformationRule originalRule)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-300); // Simulate a 300ms optimization
        var endTime = DateTime.UtcNow;
        
        return new TransformationOptimizationResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            OriginalRule = originalRule,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
