namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a benchmark operation.
/// </summary>
public class BenchmarkResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the benchmark was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the benchmark failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the rule that was benchmarked.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the data sample that was used for benchmarking.
    /// </summary>
    public DataSample? DataSample { get; set; }
    
    /// <summary>
    /// Gets or sets the number of iterations that were run.
    /// </summary>
    public int Iterations { get; set; }
    
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
    /// Gets or sets the standard deviation of the duration in milliseconds.
    /// </summary>
    public double StandardDeviationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the CPU usage percentage.
    /// </summary>
    public double CpuUsagePercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the throughput in operations per second.
    /// </summary>
    public double ThroughputOpsPerSecond { get; set; }
    
    /// <summary>
    /// Gets or sets the benchmark timestamp.
    /// </summary>
    public DateTime BenchmarkTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the benchmark.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful benchmark result.
    /// </summary>
    /// <param name="rule">The rule that was benchmarked.</param>
    /// <param name="dataSample">The data sample that was used for benchmarking.</param>
    /// <param name="iterations">The number of iterations that were run.</param>
    /// <param name="averageDurationMs">The average duration of the transformation in milliseconds.</param>
    /// <param name="minDurationMs">The minimum duration of the transformation in milliseconds.</param>
    /// <param name="maxDurationMs">The maximum duration of the transformation in milliseconds.</param>
    /// <param name="standardDeviationMs">The standard deviation of the duration in milliseconds.</param>
    /// <param name="memoryUsageBytes">The memory usage in bytes.</param>
    /// <param name="cpuUsagePercentage">The CPU usage percentage.</param>
    /// <returns>A successful benchmark result.</returns>
    public static BenchmarkResult Success(
        TransformationRule rule,
        DataSample dataSample,
        int iterations,
        double averageDurationMs,
        long minDurationMs,
        long maxDurationMs,
        double standardDeviationMs,
        long memoryUsageBytes,
        double cpuUsagePercentage)
    {
        return new BenchmarkResult
        {
            IsSuccessful = true,
            Rule = rule,
            DataSample = dataSample,
            Iterations = iterations,
            AverageDurationMs = averageDurationMs,
            MinDurationMs = minDurationMs,
            MaxDurationMs = maxDurationMs,
            StandardDeviationMs = standardDeviationMs,
            MemoryUsageBytes = memoryUsageBytes,
            CpuUsagePercentage = cpuUsagePercentage,
            ThroughputOpsPerSecond = 1000.0 / averageDurationMs
        };
    }
    
    /// <summary>
    /// Creates a failed benchmark result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="rule">The rule that was benchmarked.</param>
    /// <param name="dataSample">The data sample that was used for benchmarking.</param>
    /// <param name="iterations">The number of iterations that were run.</param>
    /// <returns>A failed benchmark result.</returns>
    public static BenchmarkResult Failure(string errorMessage, TransformationRule rule, DataSample dataSample, int iterations)
    {
        return new BenchmarkResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Rule = rule,
            DataSample = dataSample,
            Iterations = iterations
        };
    }
}
