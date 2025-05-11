using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IFlowStep interface.
/// </summary>
public class FlowStep : BaseEntity, IFlowStep
{
    /// <summary>
    /// Gets or sets the type of the step (IMPORT, PROCESS, EXPORT).
    /// </summary>
    public StepType StepType { get; set; }
    
    /// <summary>
    /// Gets or sets the position of the step within its branch.
    /// </summary>
    public int StepId { get; set; }
    
    /// <summary>
    /// Gets or sets the branch path to which this step belongs.
    /// </summary>
    public string BranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the component ID that implements this step.
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the configuration for this step.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Creates a new instance of the FlowStep class.
    /// </summary>
    public FlowStep()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowStep class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the step.</param>
    /// <param name="name">The name of the step.</param>
    /// <param name="description">The description of the step.</param>
    public FlowStep(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowStep class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the step.</param>
    /// <param name="name">The name of the step.</param>
    /// <param name="description">The description of the step.</param>
    /// <param name="stepType">The type of the step.</param>
    /// <param name="stepId">The position of the step within its branch.</param>
    /// <param name="branchPath">The branch path to which this step belongs.</param>
    /// <param name="componentId">The component ID that implements this step.</param>
    /// <param name="configuration">The configuration for this step.</param>
    public FlowStep(
        string id,
        string name,
        string description,
        StepType stepType,
        int stepId,
        string branchPath,
        string componentId,
        string configuration)
        : base(id, name, description)
    {
        StepType = stepType;
        StepId = stepId;
        BranchPath = branchPath;
        ComponentId = componentId;
        Configuration = configuration;
    }
}
