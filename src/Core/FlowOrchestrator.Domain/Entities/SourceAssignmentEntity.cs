using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents an assignment of a source to a flow.
/// </summary>
public class SourceAssignmentEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the source ID.
    /// </summary>
    public string SourceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the step ID in the flow where this source is used.
    /// </summary>
    public string StepId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch path in the flow where this source is used.
    /// </summary>
    public string BranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the configuration for this source assignment.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the filter to apply to the source data.
    /// </summary>
    public string Filter { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the transformation to apply to the source data.
    /// </summary>
    public string Transformation { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets whether this source assignment is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Creates a new instance of the SourceAssignmentEntity class.
    /// </summary>
    public SourceAssignmentEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the SourceAssignmentEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the source assignment.</param>
    /// <param name="name">The name of the source assignment.</param>
    /// <param name="description">The description of the source assignment.</param>
    public SourceAssignmentEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the SourceAssignmentEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the source assignment.</param>
    /// <param name="name">The name of the source assignment.</param>
    /// <param name="description">The description of the source assignment.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="sourceId">The source ID.</param>
    /// <param name="stepId">The step ID in the flow where this source is used.</param>
    /// <param name="branchPath">The branch path in the flow where this source is used.</param>
    /// <param name="configuration">The configuration for this source assignment.</param>
    /// <param name="filter">The filter to apply to the source data.</param>
    /// <param name="transformation">The transformation to apply to the source data.</param>
    /// <param name="isEnabled">Whether this source assignment is enabled.</param>
    public SourceAssignmentEntity(
        string id,
        string name,
        string description,
        string flowId,
        string sourceId,
        string stepId,
        string branchPath,
        string configuration,
        string filter,
        string transformation,
        bool isEnabled)
        : base(id, name, description)
    {
        FlowId = flowId;
        SourceId = sourceId;
        StepId = stepId;
        BranchPath = branchPath;
        Configuration = configuration;
        Filter = filter;
        Transformation = transformation;
        IsEnabled = isEnabled;
    }
}
