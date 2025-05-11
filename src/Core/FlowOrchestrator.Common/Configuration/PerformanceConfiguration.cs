namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for performance.
/// </summary>
public class PerformanceConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether performance optimization is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use parallel processing.
    /// </summary>
    public bool UseParallelProcessing { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the number of parallel processing tasks.
    /// </summary>
    public int ParallelProcessingTasks { get; set; } = Environment.ProcessorCount;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use batch processing.
    /// </summary>
    public bool UseBatchProcessing { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the batch size.
    /// </summary>
    public int BatchSize { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use streaming.
    /// </summary>
    public bool UseStreaming { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the streaming buffer size.
    /// </summary>
    public int StreamingBufferSize { get; set; } = 8192;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use memory pooling.
    /// </summary>
    public bool UseMemoryPooling { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the memory pool size.
    /// </summary>
    public int MemoryPoolSize { get; set; } = 1024 * 1024 * 10; // 10 MB
    
    /// <summary>
    /// Gets or sets a value indicating whether to use compression.
    /// </summary>
    public bool UseCompression { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the compression level.
    /// </summary>
    public int CompressionLevel { get; set; } = 5;
    
    /// <summary>
    /// Gets or sets the additional parameters for performance.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
