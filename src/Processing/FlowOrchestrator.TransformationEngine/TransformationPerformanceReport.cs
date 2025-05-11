namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a performance report for a transformation rule.
/// </summary>
public class TransformationPerformanceReport
{
    /// <summary>
    /// Gets or sets a value indicating whether the performance profiling was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the performance profiling failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation rule that was profiled.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the test data that was used for profiling.
    /// </summary>
    public DataPackage? TestData { get; set; }
    
    /// <summary>
    /// Gets or sets the performance profile.
    /// </summary>
    public PerformanceProfile? PerformanceProfile { get; set; }
    
    /// <summary>
    /// Gets or sets the optimization plan.
    /// </summary>
    public OptimizationPlan? OptimizationPlan { get; set; }
    
    /// <summary>
    /// Gets or sets the average duration of the transformation in milliseconds.
    /// </summary>
    public double AverageDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum duration of the transformation in milliseconds.
    /// </summary>
    public long MinDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum duration of the transformation in milliseconds.
    /// </summary>
    public long MaxDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the number of iterations that were run.
    /// </summary>
    public int IterationsRun { get; set; }
    
    /// <summary>
    /// Gets or sets the memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the CPU usage percentage.
    /// </summary>
    public double CpuUsagePercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the bottlenecks that were identified.
    /// </summary>
    public List<string> Bottlenecks { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the optimization recommendations.
    /// </summary>
    public List<string> OptimizationRecommendations { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the performance profiling.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful performance report.
    /// </summary>
    /// <param name="rule">The transformation rule that was profiled.</param>
    /// <param name="testData">The test data that was used for profiling.</param>
    /// <param name="performanceProfile">The performance profile.</param>
    /// <param name="optimizationPlan">The optimization plan.</param>
    /// <param name="averageDurationMs">The average duration of the transformation in milliseconds.</param>
    /// <param name="minDurationMs">The minimum duration of the transformation in milliseconds.</param>
    /// <param name="maxDurationMs">The maximum duration of the transformation in milliseconds.</param>
    /// <param name="iterationsRun">The number of iterations that were run.</param>
    /// <returns>A successful performance report.</returns>
    public static TransformationPerformanceReport Success(
        TransformationRule rule,
        DataPackage testData,
        PerformanceProfile performanceProfile,
        OptimizationPlan optimizationPlan,
        double averageDurationMs,
        long minDurationMs,
        long maxDurationMs,
        int iterationsRun)
    {
        return new TransformationPerformanceReport
        {
            IsSuccessful = true,
            Rule = rule,
            TestData = testData,
            PerformanceProfile = performanceProfile,
            OptimizationPlan = optimizationPlan,
            AverageDurationMs = averageDurationMs,
            MinDurationMs = minDurationMs,
            MaxDurationMs = maxDurationMs,
            IterationsRun = iterationsRun,
            MemoryUsageBytes = performanceProfile.MemoryUsageBytes,
            CpuUsagePercentage = performanceProfile.CpuUsagePercentage,
            Bottlenecks = performanceProfile.Bottlenecks,
            OptimizationRecommendations = optimizationPlan.Recommendations
        };
    }
    
    /// <summary>
    /// Creates a failed performance report.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="rule">The transformation rule that was profiled.</param>
    /// <param name="testData">The test data that was used for profiling.</param>
    /// <returns>A failed performance report.</returns>
    public static TransformationPerformanceReport Failure(string errorMessage, TransformationRule rule, DataPackage testData)
    {
        return new TransformationPerformanceReport
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Rule = rule,
            TestData = testData
        };
    }
}
