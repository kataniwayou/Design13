using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the ICompensatingAction interface.
/// </summary>
public class CompensatingAction : BaseEntity, ICompensatingAction
{
    /// <summary>
    /// Gets or sets the action type.
    /// </summary>
    public string ActionType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the action configuration.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the step ID that this action compensates for.
    /// </summary>
    public string StepId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch path that this action compensates for.
    /// </summary>
    public string BranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the execution order of this action.
    /// </summary>
    public int ExecutionOrder { get; set; }
    
    /// <summary>
    /// Creates a new instance of the CompensatingAction class.
    /// </summary>
    public CompensatingAction()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the CompensatingAction class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the action.</param>
    /// <param name="name">The name of the action.</param>
    /// <param name="description">The description of the action.</param>
    public CompensatingAction(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the CompensatingAction class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the action.</param>
    /// <param name="name">The name of the action.</param>
    /// <param name="description">The description of the action.</param>
    /// <param name="actionType">The action type.</param>
    /// <param name="configuration">The action configuration.</param>
    /// <param name="stepId">The step ID that this action compensates for.</param>
    /// <param name="branchPath">The branch path that this action compensates for.</param>
    /// <param name="executionOrder">The execution order of this action.</param>
    public CompensatingAction(
        string id,
        string name,
        string description,
        string actionType,
        string configuration,
        string stepId,
        string branchPath,
        int executionOrder)
        : base(id, name, description)
    {
        ActionType = actionType;
        Configuration = configuration;
        StepId = stepId;
        BranchPath = branchPath;
        ExecutionOrder = executionOrder;
    }
}
