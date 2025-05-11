namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a performance bottleneck.
/// </summary>
public class Bottleneck
{
    /// <summary>
    /// Gets or sets the bottleneck ID.
    /// </summary>
    public string BottleneckId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the bottleneck name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the bottleneck description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the bottleneck type.
    /// </summary>
    public BottleneckType Type { get; set; } = BottleneckType.Other;
    
    /// <summary>
    /// Gets or sets the bottleneck severity.
    /// </summary>
    public BottleneckSeverity Severity { get; set; } = BottleneckSeverity.Medium;
    
    /// <summary>
    /// Gets or sets the bottleneck location.
    /// </summary>
    public string Location { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the bottleneck impact percentage.
    /// </summary>
    public double ImpactPercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the optimization recommendations for this bottleneck.
    /// </summary>
    public List<OptimizationRecommendation> OptimizationRecommendations { get; set; } = new List<OptimizationRecommendation>();
    
    /// <summary>
    /// Gets or sets the additional information about the bottleneck.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
