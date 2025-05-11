using System;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for entities that support versioning in the FlowOrchestrator system.
/// </summary>
public interface IVersionedEntity : IEntity
{
    /// <summary>
    /// Gets or sets the semantic version number (MAJOR.MINOR.PATCH).
    /// </summary>
    string Version { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was first created.
    /// </summary>
    DateTime CreatedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was last modified.
    /// </summary>
    DateTime LastModifiedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the human-readable description of this version.
    /// </summary>
    string VersionDescription { get; set; }

    /// <summary>
    /// Gets or sets the reference to the previous version (if applicable).
    /// </summary>
    string PreviousVersionId { get; set; }

    /// <summary>
    /// Gets or sets the status of this version.
    /// </summary>
    VersionStatus VersionStatus { get; set; }

    /// <summary>
    /// Checks if the entity has been modified.
    /// </summary>
    /// <returns>True if the entity has been modified, false otherwise.</returns>
    bool IsModified();

    /// <summary>
    /// Sets the entity as modified.
    /// </summary>
    void SetModified();

    /// <summary>
    /// Clears the modified flag.
    /// </summary>
    void ClearModified();
}
