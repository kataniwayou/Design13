namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Represents the type of a flow step.
/// </summary>
public enum StepType
{
    /// <summary>
    /// Import step that retrieves data from an external source.
    /// </summary>
    Import,
    
    /// <summary>
    /// Process step that transforms or validates data.
    /// </summary>
    Process,
    
    /// <summary>
    /// Export step that sends data to an external destination.
    /// </summary>
    Export
}
