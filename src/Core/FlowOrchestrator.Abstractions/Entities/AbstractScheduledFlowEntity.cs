using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Abstract base class for scheduled flow entities.
/// </summary>
public abstract class AbstractScheduledFlowEntity : AbstractEntity, IVersionedEntity
{
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the schedule expression.
    /// </summary>
    public string ScheduleExpression { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the schedule type.
    /// </summary>
    public string ScheduleType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the schedule parameters.
    /// </summary>
    public Dictionary<string, string> ScheduleParameters { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets a value indicating whether the schedule is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the next execution time.
    /// </summary>
    public DateTime? NextExecutionTime { get; set; }
    
    /// <summary>
    /// Gets or sets the last execution time.
    /// </summary>
    public DateTime? LastExecutionTime { get; set; }
    
    /// <summary>
    /// Gets or sets the last execution status.
    /// </summary>
    public string LastExecutionStatus { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the last modified timestamp.
    /// </summary>
    public DateTime LastModifiedTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the version description.
    /// </summary>
    public string VersionDescription { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the reference to the previous version (if applicable).
    /// </summary>
    public string PreviousVersionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the status of this version.
    /// </summary>
    public VersionStatus VersionStatus { get; set; } = VersionStatus.Draft;
    
    /// <summary>
    /// Flag indicating whether the entity has been modified.
    /// </summary>
    protected bool _isModified = false;
    
    /// <summary>
    /// Creates a new instance of the AbstractScheduledFlowEntity class.
    /// </summary>
    protected AbstractScheduledFlowEntity() : base()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the AbstractScheduledFlowEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="scheduleExpression">The schedule expression.</param>
    /// <param name="scheduleType">The schedule type.</param>
    protected AbstractScheduledFlowEntity(
        string id, 
        string name, 
        string description, 
        string flowId,
        string scheduleExpression,
        string scheduleType) 
        : base(id, name, description)
    {
        FlowId = flowId;
        ScheduleExpression = scheduleExpression;
        ScheduleType = scheduleType;
    }
    
    /// <summary>
    /// Creates a new instance of the AbstractScheduledFlowEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="scheduleExpression">The schedule expression.</param>
    /// <param name="scheduleType">The schedule type.</param>
    /// <param name="version">The version.</param>
    /// <param name="versionDescription">The version description.</param>
    /// <param name="previousVersionId">The previous version ID.</param>
    /// <param name="versionStatus">The version status.</param>
    protected AbstractScheduledFlowEntity(
        string id, 
        string name, 
        string description, 
        string flowId,
        string scheduleExpression,
        string scheduleType,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus) 
        : base(id, name, description)
    {
        FlowId = flowId;
        ScheduleExpression = scheduleExpression;
        ScheduleType = scheduleType;
        Version = version;
        VersionDescription = versionDescription;
        PreviousVersionId = previousVersionId ?? string.Empty;
        VersionStatus = versionStatus;
    }
    
    /// <summary>
    /// Validates the entity.
    /// </summary>
    /// <returns>The validation result.</returns>
    public override ValidationResult Validate()
    {
        var result = base.Validate();
        
        if (string.IsNullOrEmpty(FlowId))
        {
            result.AddError("FlowId is required.");
        }
        
        if (string.IsNullOrEmpty(ScheduleExpression))
        {
            result.AddError("ScheduleExpression is required.");
        }
        
        if (string.IsNullOrEmpty(ScheduleType))
        {
            result.AddError("ScheduleType is required.");
        }
        
        return result;
    }
    
    /// <summary>
    /// Gets the entity type.
    /// </summary>
    /// <returns>The entity type.</returns>
    public override string GetEntityType() => "ScheduledFlowEntity";
    
    /// <summary>
    /// Checks if the entity has been modified.
    /// </summary>
    /// <returns>True if the entity has been modified, false otherwise.</returns>
    public bool IsModified() => _isModified;
    
    /// <summary>
    /// Sets the entity as modified.
    /// </summary>
    public void SetModified()
    {
        _isModified = true;
        LastModifiedTimestamp = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Clears the modified flag.
    /// </summary>
    public void ClearModified() => _isModified = false;
}
