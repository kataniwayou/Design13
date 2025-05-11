namespace FlowOrchestrator.Common.Versioning;

/// <summary>
/// Represents a compatibility entry in a compatibility matrix.
/// </summary>
public class CompatibilityEntry
{
    /// <summary>
    /// Gets the dependency component ID.
    /// </summary>
    public string DependencyId { get; }
    
    /// <summary>
    /// Gets the minimum component version.
    /// </summary>
    public VersionInfo MinVersion { get; }
    
    /// <summary>
    /// Gets the maximum component version.
    /// </summary>
    public VersionInfo MaxVersion { get; }
    
    /// <summary>
    /// Gets the minimum dependency version.
    /// </summary>
    public VersionInfo MinDependencyVersion { get; }
    
    /// <summary>
    /// Gets the maximum dependency version.
    /// </summary>
    public VersionInfo MaxDependencyVersion { get; }
    
    /// <summary>
    /// Gets or sets the compatibility notes.
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Creates a new instance of the CompatibilityEntry class with the specified properties.
    /// </summary>
    /// <param name="dependencyId">The dependency component ID.</param>
    /// <param name="minVersion">The minimum component version.</param>
    /// <param name="maxVersion">The maximum component version.</param>
    /// <param name="minDependencyVersion">The minimum dependency version.</param>
    /// <param name="maxDependencyVersion">The maximum dependency version.</param>
    /// <param name="notes">The compatibility notes.</param>
    public CompatibilityEntry(
        string dependencyId,
        VersionInfo minVersion,
        VersionInfo maxVersion,
        VersionInfo minDependencyVersion,
        VersionInfo maxDependencyVersion,
        string? notes = null)
    {
        DependencyId = dependencyId ?? throw new ArgumentNullException(nameof(dependencyId));
        MinVersion = minVersion ?? throw new ArgumentNullException(nameof(minVersion));
        MaxVersion = maxVersion ?? throw new ArgumentNullException(nameof(maxVersion));
        MinDependencyVersion = minDependencyVersion ?? throw new ArgumentNullException(nameof(minDependencyVersion));
        MaxDependencyVersion = maxDependencyVersion ?? throw new ArgumentNullException(nameof(maxDependencyVersion));
        Notes = notes;
    }
}
