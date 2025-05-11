using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Abstract base class for destination entities.
/// </summary>
public abstract class AbstractDestinationEntity : AbstractEntity, IVersionedEntity
{
    /// <summary>
    /// Gets or sets the destination type.
    /// </summary>
    public string DestinationType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the connection parameters.
    /// </summary>
    public Dictionary<string, string> ConnectionParameters { get; set; } = new Dictionary<string, string>();
    
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
    /// Creates a new instance of the AbstractDestinationEntity class.
    /// </summary>
    protected AbstractDestinationEntity() : base()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the AbstractDestinationEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="destinationType">The destination type.</param>
    protected AbstractDestinationEntity(string id, string name, string description, string destinationType) 
        : base(id, name, description)
    {
        DestinationType = destinationType;
    }
    
    /// <summary>
    /// Creates a new instance of the AbstractDestinationEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="version">The version.</param>
    /// <param name="versionDescription">The version description.</param>
    /// <param name="previousVersionId">The previous version ID.</param>
    /// <param name="versionStatus">The version status.</param>
    protected AbstractDestinationEntity(
        string id, 
        string name, 
        string description, 
        string destinationType,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus) 
        : base(id, name, description)
    {
        DestinationType = destinationType;
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
        
        if (string.IsNullOrEmpty(DestinationType))
        {
            result.AddError("DestinationType is required.");
        }
        
        return result;
    }
    
    /// <summary>
    /// Gets the entity type.
    /// </summary>
    /// <returns>The entity type.</returns>
    public override string GetEntityType() => "DestinationEntity";
    
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
