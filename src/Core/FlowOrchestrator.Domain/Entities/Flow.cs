using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IFlow interface.
/// </summary>
public class Flow : VersionedEntity, IFlow
{
    /// <summary>
    /// Gets or sets the list of steps in the flow.
    /// </summary>
    public IReadOnlyList<IFlowStep> Steps => _steps.AsReadOnly();
    private readonly List<IFlowStep> _steps = new();
    
    /// <summary>
    /// Gets or sets the list of branches in the flow.
    /// </summary>
    public IReadOnlyList<IFlowBranch> Branches => _branches.AsReadOnly();
    private readonly List<IFlowBranch> _branches = new();
    
    /// <summary>
    /// Gets or sets the validation rules for the flow.
    /// </summary>
    public IReadOnlyList<IValidationRule> ValidationRules => _validationRules.AsReadOnly();
    private readonly List<IValidationRule> _validationRules = new();
    
    /// <summary>
    /// Gets or sets the error handling configuration for the flow.
    /// </summary>
    public IErrorHandlingConfig ErrorHandlingConfig { get; set; } = new ErrorHandlingConfig();
    
    /// <summary>
    /// Gets or sets the merge strategy configuration for branch convergence.
    /// </summary>
    public IMergeStrategyConfig MergeStrategyConfig { get; set; } = new MergeStrategyConfig();
    
    /// <summary>
    /// Creates a new instance of the Flow class.
    /// </summary>
    public Flow()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the Flow class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the flow.</param>
    /// <param name="name">The name of the flow.</param>
    /// <param name="description">The description of the flow.</param>
    public Flow(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the Flow class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the flow.</param>
    /// <param name="name">The name of the flow.</param>
    /// <param name="description">The description of the flow.</param>
    /// <param name="version">The semantic version number.</param>
    /// <param name="versionDescription">The human-readable description of this version.</param>
    /// <param name="previousVersionId">The reference to the previous version (if applicable).</param>
    /// <param name="versionStatus">The status of this version.</param>
    public Flow(
        string id,
        string name,
        string description,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus)
        : base(id, name, description, version, versionDescription, previousVersionId, versionStatus)
    {
    }
    
    /// <summary>
    /// Adds a step to the flow.
    /// </summary>
    /// <param name="step">The step to add.</param>
    public void AddStep(IFlowStep step)
    {
        _steps.Add(step);
    }
    
    /// <summary>
    /// Adds a branch to the flow.
    /// </summary>
    /// <param name="branch">The branch to add.</param>
    public void AddBranch(IFlowBranch branch)
    {
        _branches.Add(branch);
    }
    
    /// <summary>
    /// Adds a validation rule to the flow.
    /// </summary>
    /// <param name="validationRule">The validation rule to add.</param>
    public void AddValidationRule(IValidationRule validationRule)
    {
        _validationRules.Add(validationRule);
    }
    
    /// <summary>
    /// Validates the flow structure and configuration.
    /// </summary>
    /// <returns>True if the flow is valid, false otherwise.</returns>
    public bool Validate()
    {
        // Validate that there is at least one step
        if (!Steps.Any())
        {
            return false;
        }
        
        // Validate that there is at least one branch (the main branch)
        if (!Branches.Any())
        {
            return false;
        }
        
        // Validate that all steps belong to a branch
        foreach (var step in Steps)
        {
            if (!Branches.Any(b => b.BranchPath == step.BranchPath))
            {
                return false;
            }
        }
        
        // Validate that all branches have at least one step
        foreach (var branch in Branches)
        {
            if (!Steps.Any(s => s.BranchPath == branch.BranchPath))
            {
                return false;
            }
        }
        
        // Validate that all branch merge configurations reference valid branches
        foreach (var branch in Branches)
        {
            if (branch.MergeConfig != null && 
                !Branches.Any(b => b.BranchPath == branch.MergeConfig.TargetBranchPath))
            {
                return false;
            }
        }
        
        return true;
    }
}
