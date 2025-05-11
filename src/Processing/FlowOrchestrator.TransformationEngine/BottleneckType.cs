namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the type of a bottleneck.
/// </summary>
public enum BottleneckType
{
    /// <summary>
    /// CPU bottleneck.
    /// </summary>
    Cpu,
    
    /// <summary>
    /// Memory bottleneck.
    /// </summary>
    Memory,
    
    /// <summary>
    /// I/O bottleneck.
    /// </summary>
    IO,
    
    /// <summary>
    /// Network bottleneck.
    /// </summary>
    Network,
    
    /// <summary>
    /// Algorithm bottleneck.
    /// </summary>
    Algorithm,
    
    /// <summary>
    /// Data structure bottleneck.
    /// </summary>
    DataStructure,
    
    /// <summary>
    /// Concurrency bottleneck.
    /// </summary>
    Concurrency,
    
    /// <summary>
    /// Resource contention bottleneck.
    /// </summary>
    ResourceContention,
    
    /// <summary>
    /// External dependency bottleneck.
    /// </summary>
    ExternalDependency,
    
    /// <summary>
    /// Other bottleneck.
    /// </summary>
    Other
}
