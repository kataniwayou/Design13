using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Abstract base class for source assignment entities.
/// </summary>
public abstract class AbstractSourceAssignmentEntity : AbstractEntity, IVersionedEntity
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
    /// Gets or sets the source parameters.
    /// </summary>
    public Dictionary<string, string> SourceParameters { get; set; } = new Dictionary<string, string>();
    
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
    /// Creates a new instance of the AbstractSourceAssignmentEntity class.
    /// </summary>
    protected AbstractSourceAssignmentEntity() : base()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the AbstractSourceAssignmentEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="sourceId">The source ID.</param>
    protected AbstractSourceAssignmentEntity(string id, string name, string description, string flowId, string sourceId) 
        : base(id, name, description)
    {
        FlowId = flowId;
        SourceId = sourceId;
    }
    
    /// <summary>
    /// Creates a new instance of the AbstractSourceAssignmentEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="sourceId">The source ID.</param>
    /// <param name="version">The version.</param>
    /// <param name="versionDescription">The version description.</param>
    /// <param name="previousVersionId">The previous version ID.</param>
    /// <param name="versionStatus">The version status.</param>
    protected AbstractSourceAssignmentEntity(
        string id, 
        string name, 
        string description, 
        string flowId,
        string sourceId,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus) 
        : base(id, name, description)
    {
        FlowId = flowId;
        SourceId = sourceId;
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
        
        if (string.IsNullOrEmpty(SourceId))
        {
            result.AddError("SourceId is required.");
        }
        
        return result;
    }
    
    /// <summary>
    /// Gets the entity type.
    /// </summary>
    /// <returns>The entity type.</returns>
    public override string GetEntityType() => "SourceAssignmentEntity";
    
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
