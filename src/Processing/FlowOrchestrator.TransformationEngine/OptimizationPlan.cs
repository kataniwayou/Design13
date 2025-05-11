namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents an optimization plan for a transformation rule.
/// </summary>
public class OptimizationPlan
{
    /// <summary>
    /// Gets or sets the unique identifier for this optimization plan.
    /// </summary>
    public string PlanId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the transformation rule that this plan is for.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the performance profile that this plan is based on.
    /// </summary>
    public PerformanceProfile? PerformanceProfile { get; set; }
    
    /// <summary>
    /// Gets or sets the optimization techniques to apply.
    /// </summary>
    public List<string> OptimizationTechniques { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the expected optimization factor (performance improvement).
    /// </summary>
    public double ExpectedOptimizationFactor { get; set; }
    
    /// <summary>
    /// Gets or sets the optimization recommendations.
    /// </summary>
    public List<string> Recommendations { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the optimization priority.
    /// </summary>
    public OptimizationPriority Priority { get; set; } = OptimizationPriority.Medium;
    
    /// <summary>
    /// Gets or sets the timestamp when this plan was created.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the optimization plan.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
