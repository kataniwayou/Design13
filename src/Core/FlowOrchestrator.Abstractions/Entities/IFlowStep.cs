namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for a step in a flow.
/// </summary>
public interface IFlowStep : IEntity
{
    /// <summary>
    /// Gets the type of the step (IMPORT, PROCESS, EXPORT).
    /// </summary>
    StepType StepType { get; }
    
    /// <summary>
    /// Gets the position of the step within its branch.
    /// </summary>
    int StepId { get; }
    
    /// <summary>
    /// Gets the branch path to which this step belongs.
    /// </summary>
    string BranchPath { get; }
    
    /// <summary>
    /// Gets the component ID that implements this step.
    /// </summary>
    string ComponentId { get; }
    
    /// <summary>
    /// Gets the configuration for this step.
    /// </summary>
    string Configuration { get; }
}
