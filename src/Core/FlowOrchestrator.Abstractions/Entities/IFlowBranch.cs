using System.Collections.Generic;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for a branch in a flow.
/// </summary>
public interface IFlowBranch : IEntity
{
    /// <summary>
    /// Gets the path of the branch (e.g., main, branchA, branchB).
    /// </summary>
    string BranchPath { get; }
    
    /// <summary>
    /// Gets the parent branch path (null for the main branch).
    /// </summary>
    string? ParentBranchPath { get; }
    
    /// <summary>
    /// Gets the step ID in the parent branch where this branch splits off.
    /// </summary>
    int? ParentStepId { get; }
    
    /// <summary>
    /// Gets the list of steps in this branch.
    /// </summary>
    IReadOnlyList<IFlowStep> Steps { get; }
    
    /// <summary>
    /// Gets the branch priority for resource allocation.
    /// </summary>
    int Priority { get; }
    
    /// <summary>
    /// Gets the merge configuration for this branch.
    /// </summary>
    IMergeConfig MergeConfig { get; }
}
