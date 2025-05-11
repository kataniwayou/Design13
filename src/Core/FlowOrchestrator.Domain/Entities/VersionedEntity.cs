using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Base implementation of the IVersionedEntity interface.
/// </summary>
public abstract class VersionedEntity : BaseEntity, IVersionedEntity
{
    /// <summary>
    /// Gets or sets the semantic version number (MAJOR.MINOR.PATCH).
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// Gets or sets the timestamp when the entity was first created.
    /// </summary>
    public DateTime CreatedTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the timestamp when the entity was last modified.
    /// </summary>
    public DateTime LastModifiedTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the human-readable description of this version.
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
    /// Creates a new instance of the VersionedEntity class.
    /// </summary>
    protected VersionedEntity()
    {
    }

    /// <summary>
    /// Creates a new instance of the VersionedEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    protected VersionedEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }

    /// <summary>
    /// Creates a new instance of the VersionedEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="version">The semantic version number.</param>
    /// <param name="versionDescription">The human-readable description of this version.</param>
    /// <param name="previousVersionId">The reference to the previous version (if applicable).</param>
    /// <param name="versionStatus">The status of this version.</param>
    protected VersionedEntity(
        string id,
        string name,
        string description,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus)
        : base(id, name, description)
    {
        Version = version;
        VersionDescription = versionDescription;
        PreviousVersionId = previousVersionId ?? string.Empty;
        VersionStatus = versionStatus;
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
