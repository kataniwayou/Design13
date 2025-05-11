using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IFlowBranch interface.
/// </summary>
public class FlowBranch : BaseEntity, IFlowBranch
{
    /// <summary>
    /// Gets or sets the path of the branch (e.g., main, branchA, branchB).
    /// </summary>
    public string BranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the parent branch path (null for the main branch).
    /// </summary>
    public string? ParentBranchPath { get; set; }
    
    /// <summary>
    /// Gets or sets the step ID in the parent branch where this branch splits off.
    /// </summary>
    public int? ParentStepId { get; set; }
    
    /// <summary>
    /// Gets or sets the list of steps in this branch.
    /// </summary>
    public IReadOnlyList<IFlowStep> Steps => _steps.AsReadOnly();
    private readonly List<IFlowStep> _steps = new();
    
    /// <summary>
    /// Gets or sets the branch priority for resource allocation.
    /// </summary>
    public int Priority { get; set; }
    
    /// <summary>
    /// Gets or sets the merge configuration for this branch.
    /// </summary>
    public IMergeConfig MergeConfig { get; set; } = new MergeConfig();
    
    /// <summary>
    /// Creates a new instance of the FlowBranch class.
    /// </summary>
    public FlowBranch()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowBranch class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the branch.</param>
    /// <param name="name">The name of the branch.</param>
    /// <param name="description">The description of the branch.</param>
    public FlowBranch(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowBranch class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the branch.</param>
    /// <param name="name">The name of the branch.</param>
    /// <param name="description">The description of the branch.</param>
    /// <param name="branchPath">The path of the branch.</param>
    /// <param name="parentBranchPath">The parent branch path (null for the main branch).</param>
    /// <param name="parentStepId">The step ID in the parent branch where this branch splits off.</param>
    /// <param name="priority">The branch priority for resource allocation.</param>
    /// <param name="mergeConfig">The merge configuration for this branch.</param>
    public FlowBranch(
        string id,
        string name,
        string description,
        string branchPath,
        string? parentBranchPath,
        int? parentStepId,
        int priority,
        IMergeConfig mergeConfig)
        : base(id, name, description)
    {
        BranchPath = branchPath;
        ParentBranchPath = parentBranchPath;
        ParentStepId = parentStepId;
        Priority = priority;
        MergeConfig = mergeConfig;
    }
    
    /// <summary>
    /// Adds a step to the branch.
    /// </summary>
    /// <param name="step">The step to add.</param>
    public void AddStep(IFlowStep step)
    {
        _steps.Add(step);
    }
}
