namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the type of an optimization recommendation.
/// </summary>
public enum OptimizationRecommendationType
{
    /// <summary>
    /// Algorithm optimization.
    /// </summary>
    Algorithm,
    
    /// <summary>
    /// Data structure optimization.
    /// </summary>
    DataStructure,
    
    /// <summary>
    /// Memory usage optimization.
    /// </summary>
    MemoryUsage,
    
    /// <summary>
    /// CPU usage optimization.
    /// </summary>
    CpuUsage,
    
    /// <summary>
    /// I/O optimization.
    /// </summary>
    IO,
    
    /// <summary>
    /// Network optimization.
    /// </summary>
    Network,
    
    /// <summary>
    /// Concurrency optimization.
    /// </summary>
    Concurrency,
    
    /// <summary>
    /// Caching optimization.
    /// </summary>
    Caching,
    
    /// <summary>
    /// Batching optimization.
    /// </summary>
    Batching,
    
    /// <summary>
    /// Parallelization optimization.
    /// </summary>
    Parallelization,
    
    /// <summary>
    /// Resource management optimization.
    /// </summary>
    ResourceManagement,
    
    /// <summary>
    /// External dependency optimization.
    /// </summary>
    ExternalDependency,
    
    /// <summary>
    /// Configuration optimization.
    /// </summary>
    Configuration,
    
    /// <summary>
    /// Other optimization.
    /// </summary>
    Other
}
