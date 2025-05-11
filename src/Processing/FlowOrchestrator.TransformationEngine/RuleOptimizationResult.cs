namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a rule optimization operation.
/// </summary>
public class RuleOptimizationResult
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
    /// Gets or sets the original rule.
    /// </summary>
    public TransformationRule? OriginalRule { get; set; }
    
    /// <summary>
    /// Gets or sets the optimized rule.
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
    /// Gets or sets the timestamp when the optimization was performed.
    /// </summary>
    public DateTime OptimizationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the optimization.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful optimization result.
    /// </summary>
    /// <param name="originalRule">The original rule.</param>
    /// <param name="optimizedRule">The optimized rule.</param>
    /// <param name="optimizationFactor">The optimization factor (performance improvement).</param>
    /// <param name="optimizationTechniques">The optimization techniques applied.</param>
    /// <returns>A successful optimization result.</returns>
    public static RuleOptimizationResult Success(TransformationRule originalRule, TransformationRule optimizedRule, double optimizationFactor, List<string>? optimizationTechniques = null)
    {
        return new RuleOptimizationResult
        {
            IsSuccessful = true,
            OriginalRule = originalRule,
            OptimizedRule = optimizedRule,
            OptimizationFactor = optimizationFactor,
            OptimizationTechniques = optimizationTechniques ?? new List<string>()
        };
    }
    
    /// <summary>
    /// Creates a failed optimization result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="originalRule">The original rule.</param>
    /// <returns>A failed optimization result.</returns>
    public static RuleOptimizationResult Failure(string errorMessage, TransformationRule originalRule)
    {
        return new RuleOptimizationResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            OriginalRule = originalRule
        };
    }
}
