using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Abstract base class for flow entities.
/// </summary>
public abstract class AbstractFlowEntity : AbstractEntity, IVersionedEntity
{
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; }

    /// <summary>
    /// Gets or sets the importer service ID.
    /// </summary>
    public string ImporterServiceId { get; set; }

    /// <summary>
    /// Gets or sets the importer service version.
    /// </summary>
    public string ImporterServiceVersion { get; set; }

    /// <summary>
    /// Gets or sets the processing chains.
    /// </summary>
    public List<string> ProcessingChains { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the exporters.
    /// </summary>
    public List<string> Exporters { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the connections.
    /// </summary>
    public Dictionary<string, string> Connections { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the last modified timestamp.
    /// </summary>
    public DateTime LastModifiedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the version description.
    /// </summary>
    public string VersionDescription { get; set; }

    /// <summary>
    /// Gets or sets the previous version ID.
    /// </summary>
    public string PreviousVersionId { get; set; }

    /// <summary>
    /// Gets or sets the version status.
    /// </summary>
    public VersionStatus VersionStatus { get; set; }

    /// <summary>
    /// Flag indicating whether the entity has been modified.
    /// </summary>
    protected bool _isModified = false;

    /// <summary>
    /// Creates a new instance of the AbstractFlowEntity class.
    /// </summary>
    protected AbstractFlowEntity() : base()
    {
        FlowId = string.Empty;
        ImporterServiceId = string.Empty;
        ImporterServiceVersion = string.Empty;
        Version = "1.0.0";
        CreatedTimestamp = DateTime.UtcNow;
        LastModifiedTimestamp = DateTime.UtcNow;
        VersionDescription = string.Empty;
        PreviousVersionId = string.Empty;
        VersionStatus = VersionStatus.Draft;
    }

    /// <summary>
    /// Creates a new instance of the AbstractFlowEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    protected AbstractFlowEntity(string id, string name, string description) : base(id, name, description)
    {
        FlowId = id;
        ImporterServiceId = string.Empty;
        ImporterServiceVersion = string.Empty;
        Version = "1.0.0";
        CreatedTimestamp = DateTime.UtcNow;
        LastModifiedTimestamp = DateTime.UtcNow;
        VersionDescription = string.Empty;
        PreviousVersionId = string.Empty;
        VersionStatus = VersionStatus.Draft;
    }

    /// <summary>
    /// Gets the entity ID.
    /// </summary>
    /// <returns>The entity ID.</returns>
    public override string GetEntityId() => FlowId;

    /// <summary>
    /// Gets the entity type.
    /// </summary>
    /// <returns>The entity type.</returns>
    public override string GetEntityType() => "FlowEntity";

    /// <summary>
    /// Validates the entity.
    /// </summary>
    /// <returns>The validation result.</returns>
    public override ValidationResult Validate()
    {
        var result = new ValidationResult();

        if (string.IsNullOrEmpty(FlowId))
        {
            result.AddError("FlowId is required.");
        }

        if (string.IsNullOrEmpty(ImporterServiceId))
        {
            result.AddError("ImporterServiceId is required.");
        }

        if (string.IsNullOrEmpty(ImporterServiceVersion))
        {
            result.AddError("ImporterServiceVersion is required.");
        }

        return result;
    }

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
