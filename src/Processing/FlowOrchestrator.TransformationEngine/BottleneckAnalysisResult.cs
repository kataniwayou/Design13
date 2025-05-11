namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a bottleneck analysis operation.
/// </summary>
public class BottleneckAnalysisResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the analysis was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the analysis failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the rule that was analyzed.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the data sample that was used for analysis.
    /// </summary>
    public DataSample? DataSample { get; set; }
    
    /// <summary>
    /// Gets or sets the bottlenecks that were identified.
    /// </summary>
    public List<Bottleneck> Bottlenecks { get; set; } = new List<Bottleneck>();
    
    /// <summary>
    /// Gets or sets the optimization recommendations.
    /// </summary>
    public List<OptimizationRecommendation> OptimizationRecommendations { get; set; } = new List<OptimizationRecommendation>();
    
    /// <summary>
    /// Gets or sets the performance profile.
    /// </summary>
    public PerformanceProfile? PerformanceProfile { get; set; }
    
    /// <summary>
    /// Gets or sets the analysis timestamp.
    /// </summary>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the analysis.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful analysis result.
    /// </summary>
    /// <param name="rule">The rule that was analyzed.</param>
    /// <param name="dataSample">The data sample that was used for analysis.</param>
    /// <param name="bottlenecks">The bottlenecks that were identified.</param>
    /// <param name="optimizationRecommendations">The optimization recommendations.</param>
    /// <param name="performanceProfile">The performance profile.</param>
    /// <returns>A successful analysis result.</returns>
    public static BottleneckAnalysisResult Success(
        TransformationRule rule,
        DataSample dataSample,
        List<Bottleneck> bottlenecks,
        List<OptimizationRecommendation> optimizationRecommendations,
        PerformanceProfile performanceProfile)
    {
        return new BottleneckAnalysisResult
        {
            IsSuccessful = true,
            Rule = rule,
            DataSample = dataSample,
            Bottlenecks = bottlenecks,
            OptimizationRecommendations = optimizationRecommendations,
            PerformanceProfile = performanceProfile
        };
    }
    
    /// <summary>
    /// Creates a failed analysis result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="rule">The rule that was analyzed.</param>
    /// <param name="dataSample">The data sample that was used for analysis.</param>
    /// <returns>A failed analysis result.</returns>
    public static BottleneckAnalysisResult Failure(string errorMessage, TransformationRule rule, DataSample dataSample)
    {
        return new BottleneckAnalysisResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Rule = rule,
            DataSample = dataSample
        };
    }
}
