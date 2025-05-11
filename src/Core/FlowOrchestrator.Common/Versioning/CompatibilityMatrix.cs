namespace FlowOrchestrator.Common.Versioning;

/// <summary>
/// Represents a compatibility matrix for components or entities.
/// </summary>
public class CompatibilityMatrix
{
    private readonly Dictionary<string, List<CompatibilityEntry>> _compatibilityEntries = new();
    
    /// <summary>
    /// Gets the compatibility entries for all components.
    /// </summary>
    public IReadOnlyDictionary<string, IReadOnlyList<CompatibilityEntry>> Entries
    {
        get
        {
            var result = new Dictionary<string, IReadOnlyList<CompatibilityEntry>>();
            foreach (var entry in _compatibilityEntries)
            {
                result[entry.Key] = entry.Value.AsReadOnly();
            }
            
            return result;
        }
    }
    
    /// <summary>
    /// Adds a compatibility entry to the matrix.
    /// </summary>
    /// <param name="componentId">The component ID.</param>
    /// <param name="entry">The compatibility entry to add.</param>
    public void AddEntry(string componentId, CompatibilityEntry entry)
    {
        if (!_compatibilityEntries.TryGetValue(componentId, out var entries))
        {
            entries = new List<CompatibilityEntry>();
            _compatibilityEntries[componentId] = entries;
        }
        
        entries.Add(entry);
    }
    
    /// <summary>
    /// Checks if a component version is compatible with another component version.
    /// </summary>
    /// <param name="componentId">The component ID.</param>
    /// <param name="version">The component version.</param>
    /// <param name="dependencyId">The dependency component ID.</param>
    /// <param name="dependencyVersion">The dependency component version.</param>
    /// <returns>True if the component version is compatible with the dependency version, false otherwise.</returns>
    public bool IsCompatible(string componentId, string version, string dependencyId, string dependencyVersion)
    {
        if (!_compatibilityEntries.TryGetValue(componentId, out var entries))
        {
            return false;
        }
        
        var componentVersionInfo = VersionInfo.Parse(version);
        var dependencyVersionInfo = VersionInfo.Parse(dependencyVersion);
        
        foreach (var entry in entries)
        {
            if (entry.DependencyId == dependencyId)
            {
                var componentVersionRange = new VersionRange(entry.MinVersion, entry.MaxVersion);
                var dependencyVersionRange = new VersionRange(entry.MinDependencyVersion, entry.MaxDependencyVersion);
                
                if (componentVersionRange.Contains(componentVersionInfo) && dependencyVersionRange.Contains(dependencyVersionInfo))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    
    /// <summary>
    /// Gets the compatible dependency versions for a component version.
    /// </summary>
    /// <param name="componentId">The component ID.</param>
    /// <param name="version">The component version.</param>
    /// <param name="dependencyId">The dependency component ID.</param>
    /// <returns>A version range representing the compatible dependency versions, or null if no compatibility entry is found.</returns>
    public VersionRange? GetCompatibleDependencyVersions(string componentId, string version, string dependencyId)
    {
        if (!_compatibilityEntries.TryGetValue(componentId, out var entries))
        {
            return null;
        }
        
        var componentVersionInfo = VersionInfo.Parse(version);
        
        foreach (var entry in entries)
        {
            if (entry.DependencyId == dependencyId)
            {
                var componentVersionRange = new VersionRange(entry.MinVersion, entry.MaxVersion);
                
                if (componentVersionRange.Contains(componentVersionInfo))
                {
                    return new VersionRange(entry.MinDependencyVersion, entry.MaxDependencyVersion);
                }
            }
        }
        
        return null;
    }
    
    /// <summary>
    /// Gets the compatible component versions for a dependency version.
    /// </summary>
    /// <param name="componentId">The component ID.</param>
    /// <param name="dependencyId">The dependency component ID.</param>
    /// <param name="dependencyVersion">The dependency component version.</param>
    /// <returns>A version range representing the compatible component versions, or null if no compatibility entry is found.</returns>
    public VersionRange? GetCompatibleComponentVersions(string componentId, string dependencyId, string dependencyVersion)
    {
        if (!_compatibilityEntries.TryGetValue(componentId, out var entries))
        {
            return null;
        }
        
        var dependencyVersionInfo = VersionInfo.Parse(dependencyVersion);
        
        foreach (var entry in entries)
        {
            if (entry.DependencyId == dependencyId)
            {
                var dependencyVersionRange = new VersionRange(entry.MinDependencyVersion, entry.MaxDependencyVersion);
                
                if (dependencyVersionRange.Contains(dependencyVersionInfo))
                {
                    return new VersionRange(entry.MinVersion, entry.MaxVersion);
                }
            }
        }
        
        return null;
    }
}
