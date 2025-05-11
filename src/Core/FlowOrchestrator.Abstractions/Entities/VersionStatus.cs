namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Represents the status of a versioned entity.
/// </summary>
public enum VersionStatus
{
    /// <summary>
    /// The entity is in draft status.
    /// </summary>
    Draft,

    /// <summary>
    /// The entity is in review status.
    /// </summary>
    Review,

    /// <summary>
    /// The entity is in approved status.
    /// </summary>
    Approved,

    /// <summary>
    /// The entity is in published status (active and can be used in new flows).
    /// </summary>
    Published,

    /// <summary>
    /// The entity version is deprecated and should not be used in new flows,
    /// but existing flows using this version will continue to work.
    /// </summary>
    Deprecated,

    /// <summary>
    /// The entity version is archived and cannot be used in any flows.
    /// </summary>
    Archived
}
