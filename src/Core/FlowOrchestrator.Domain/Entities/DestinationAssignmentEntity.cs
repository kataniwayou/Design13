using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents an assignment of a destination to a flow.
/// </summary>
public class DestinationAssignmentEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the destination ID.
    /// </summary>
    public string DestinationId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the step ID in the flow where this destination is used.
    /// </summary>
    public string StepId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch path in the flow where this destination is used.
    /// </summary>
    public string BranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the configuration for this destination assignment.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the filter to apply to the data before exporting.
    /// </summary>
    public string Filter { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the transformation to apply to the data before exporting.
    /// </summary>
    public string Transformation { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets whether this destination assignment is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Creates a new instance of the DestinationAssignmentEntity class.
    /// </summary>
    public DestinationAssignmentEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DestinationAssignmentEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the destination assignment.</param>
    /// <param name="name">The name of the destination assignment.</param>
    /// <param name="description">The description of the destination assignment.</param>
    public DestinationAssignmentEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DestinationAssignmentEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the destination assignment.</param>
    /// <param name="name">The name of the destination assignment.</param>
    /// <param name="description">The description of the destination assignment.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="destinationId">The destination ID.</param>
    /// <param name="stepId">The step ID in the flow where this destination is used.</param>
    /// <param name="branchPath">The branch path in the flow where this destination is used.</param>
    /// <param name="configuration">The configuration for this destination assignment.</param>
    /// <param name="filter">The filter to apply to the data before exporting.</param>
    /// <param name="transformation">The transformation to apply to the data before exporting.</param>
    /// <param name="isEnabled">Whether this destination assignment is enabled.</param>
    public DestinationAssignmentEntity(
        string id,
        string name,
        string description,
        string flowId,
        string destinationId,
        string stepId,
        string branchPath,
        string configuration,
        string filter,
        string transformation,
        bool isEnabled)
        : base(id, name, description)
    {
        FlowId = flowId;
        DestinationId = destinationId;
        StepId = stepId;
        BranchPath = branchPath;
        Configuration = configuration;
        Filter = filter;
        Transformation = transformation;
        IsEnabled = isEnabled;
    }
}
