namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Defines the interface for transformation performance optimization.
/// </summary>
public interface ITransformationPerformanceOptimizer
{
    /// <summary>
    /// Optimizes a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to optimize.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the optimization result.</returns>
    Task<RuleOptimizationResult> OptimizeRuleAsync(TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Analyzes the performance of a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to analyze.</param>
    /// <param name="dataSample">The data sample to use for analysis.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the performance profile.</returns>
    Task<PerformanceProfile> AnalyzeTransformationPerformanceAsync(TransformationRule rule, DataSample dataSample, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates an optimization plan for a performance profile.
    /// </summary>
    /// <param name="performanceProfile">The performance profile to create a plan for.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the optimization plan.</returns>
    Task<OptimizationPlan> CreateOptimizationPlanAsync(PerformanceProfile performanceProfile, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Benchmarks a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to benchmark.</param>
    /// <param name="dataSample">The data sample to use for benchmarking.</param>
    /// <param name="iterations">The number of iterations to run.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the benchmark result.</returns>
    Task<BenchmarkResult> BenchmarkRuleAsync(TransformationRule rule, DataSample dataSample, int iterations, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Compares the performance of two transformation rules.
    /// </summary>
    /// <param name="rule1">The first rule to compare.</param>
    /// <param name="rule2">The second rule to compare.</param>
    /// <param name="dataSample">The data sample to use for comparison.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the performance comparison result.</returns>
    Task<PerformanceComparisonResult> CompareRulePerformanceAsync(TransformationRule rule1, TransformationRule rule2, DataSample dataSample, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Identifies bottlenecks in a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to analyze.</param>
    /// <param name="dataSample">The data sample to use for analysis.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the bottleneck analysis result.</returns>
    Task<BottleneckAnalysisResult> IdentifyBottlenecksAsync(TransformationRule rule, DataSample dataSample, CancellationToken cancellationToken = default);
}
