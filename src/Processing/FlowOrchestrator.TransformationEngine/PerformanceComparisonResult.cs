namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a performance comparison operation.
/// </summary>
public class PerformanceComparisonResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the comparison was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the comparison failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the first rule that was compared.
    /// </summary>
    public TransformationRule? Rule1 { get; set; }
    
    /// <summary>
    /// Gets or sets the second rule that was compared.
    /// </summary>
    public TransformationRule? Rule2 { get; set; }
    
    /// <summary>
    /// Gets or sets the data sample that was used for comparison.
    /// </summary>
    public DataSample? DataSample { get; set; }
    
    /// <summary>
    /// Gets or sets the benchmark result for the first rule.
    /// </summary>
    public BenchmarkResult? BenchmarkResult1 { get; set; }
    
    /// <summary>
    /// Gets or sets the benchmark result for the second rule.
    /// </summary>
    public BenchmarkResult? BenchmarkResult2 { get; set; }
    
    /// <summary>
    /// Gets or sets the performance difference factor (Rule1 / Rule2).
    /// </summary>
    public double PerformanceDifferenceFactor { get; set; }
    
    /// <summary>
    /// Gets or sets the memory usage difference factor (Rule1 / Rule2).
    /// </summary>
    public double MemoryUsageDifferenceFactor { get; set; }
    
    /// <summary>
    /// Gets or sets the CPU usage difference factor (Rule1 / Rule2).
    /// </summary>
    public double CpuUsageDifferenceFactor { get; set; }
    
    /// <summary>
    /// Gets or sets the throughput difference factor (Rule1 / Rule2).
    /// </summary>
    public double ThroughputDifferenceFactor { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the first rule is faster.
    /// </summary>
    public bool IsRule1Faster { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the first rule uses less memory.
    /// </summary>
    public bool IsRule1MemoryEfficient { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the first rule uses less CPU.
    /// </summary>
    public bool IsRule1CpuEfficient { get; set; }
    
    /// <summary>
    /// Gets or sets the comparison timestamp.
    /// </summary>
    public DateTime ComparisonTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the comparison.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful comparison result.
    /// </summary>
    /// <param name="rule1">The first rule that was compared.</param>
    /// <param name="rule2">The second rule that was compared.</param>
    /// <param name="dataSample">The data sample that was used for comparison.</param>
    /// <param name="benchmarkResult1">The benchmark result for the first rule.</param>
    /// <param name="benchmarkResult2">The benchmark result for the second rule.</param>
    /// <returns>A successful comparison result.</returns>
    public static PerformanceComparisonResult Success(
        TransformationRule rule1,
        TransformationRule rule2,
        DataSample dataSample,
        BenchmarkResult benchmarkResult1,
        BenchmarkResult benchmarkResult2)
    {
        var performanceDifferenceFactor = benchmarkResult2.AverageDurationMs / benchmarkResult1.AverageDurationMs;
        var memoryUsageDifferenceFactor = (double)benchmarkResult2.MemoryUsageBytes / benchmarkResult1.MemoryUsageBytes;
        var cpuUsageDifferenceFactor = benchmarkResult2.CpuUsagePercentage / benchmarkResult1.CpuUsagePercentage;
        var throughputDifferenceFactor = benchmarkResult1.ThroughputOpsPerSecond / benchmarkResult2.ThroughputOpsPerSecond;
        
        return new PerformanceComparisonResult
        {
            IsSuccessful = true,
            Rule1 = rule1,
            Rule2 = rule2,
            DataSample = dataSample,
            BenchmarkResult1 = benchmarkResult1,
            BenchmarkResult2 = benchmarkResult2,
            PerformanceDifferenceFactor = performanceDifferenceFactor,
            MemoryUsageDifferenceFactor = memoryUsageDifferenceFactor,
            CpuUsageDifferenceFactor = cpuUsageDifferenceFactor,
            ThroughputDifferenceFactor = throughputDifferenceFactor,
            IsRule1Faster = performanceDifferenceFactor > 1.0,
            IsRule1MemoryEfficient = memoryUsageDifferenceFactor > 1.0,
            IsRule1CpuEfficient = cpuUsageDifferenceFactor > 1.0
        };
    }
    
    /// <summary>
    /// Creates a failed comparison result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="rule1">The first rule that was compared.</param>
    /// <param name="rule2">The second rule that was compared.</param>
    /// <param name="dataSample">The data sample that was used for comparison.</param>
    /// <returns>A failed comparison result.</returns>
    public static PerformanceComparisonResult Failure(string errorMessage, TransformationRule rule1, TransformationRule rule2, DataSample dataSample)
    {
        return new PerformanceComparisonResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Rule1 = rule1,
            Rule2 = rule2,
            DataSample = dataSample
        };
    }
}
