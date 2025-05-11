namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for a compensating action to execute on failure.
/// </summary>
public interface ICompensatingAction : IEntity
{
    /// <summary>
    /// Gets the action type.
    /// </summary>
    string ActionType { get; }
    
    /// <summary>
    /// Gets the action configuration.
    /// </summary>
    string Configuration { get; }
    
    /// <summary>
    /// Gets the step ID that this action compensates for.
    /// </summary>
    string StepId { get; }
    
    /// <summary>
    /// Gets the branch path that this action compensates for.
    /// </summary>
    string BranchPath { get; }
    
    /// <summary>
    /// Gets the execution order of this action.
    /// </summary>
    int ExecutionOrder { get; }
}
