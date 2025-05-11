namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents an optimization recommendation.
/// </summary>
public class OptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation ID.
    /// </summary>
    public string RecommendationId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the recommendation name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the recommendation description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the recommendation type.
    /// </summary>
    public OptimizationRecommendationType Type { get; set; } = OptimizationRecommendationType.Other;
    
    /// <summary>
    /// Gets or sets the recommendation priority.
    /// </summary>
    public OptimizationPriority Priority { get; set; } = OptimizationPriority.Medium;
    
    /// <summary>
    /// Gets or sets the expected impact percentage.
    /// </summary>
    public double ExpectedImpactPercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the implementation difficulty.
    /// </summary>
    public ImplementationDifficulty ImplementationDifficulty { get; set; } = ImplementationDifficulty.Medium;
    
    /// <summary>
    /// Gets or sets the implementation steps.
    /// </summary>
    public List<string> ImplementationSteps { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the recommendation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
