using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.VersionManager;

/// <summary>
/// Represents a version compatibility matrix
/// </summary>
public class VersionCompatibilityMatrix
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Matrix entries
    /// </summary>
    public List<VersionCompatibilityEntry> Entries { get; set; } = new List<VersionCompatibilityEntry>();
    
    /// <summary>
    /// Matrix last updated timestamp
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Matrix last updater
    /// </summary>
    public string LastUpdatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Adds a compatibility entry to the matrix
    /// </summary>
    /// <param name="entry">Compatibility entry</param>
    public void AddEntry(VersionCompatibilityEntry entry)
    {
        Entries.Add(entry);
    }
    
    /// <summary>
    /// Checks if two versions are compatible
    /// </summary>
    /// <param name="version">Version</param>
    /// <param name="targetComponentId">Target component ID</param>
    /// <param name="targetVersion">Target version</param>
    /// <returns>Compatibility result</returns>
    public VersionCompatibilityResult CheckCompatibility(string version, string targetComponentId, string targetVersion)
    {
        var entry = Entries.FirstOrDefault(e => 
            e.Version == version && 
            e.TargetComponentId == targetComponentId);
        
        if (entry == null)
        {
            return new VersionCompatibilityResult
            {
                IsCompatible = false,
                ComponentId = ComponentId,
                Version = version,
                TargetComponentId = targetComponentId,
                TargetVersion = targetVersion,
                CompatibilityLevel = CompatibilityLevel.Unknown,
                Reason = "No compatibility information available"
            };
        }
        
        var isCompatible = entry.IsVersionCompatible(targetVersion);
        var level = entry.GetCompatibilityLevel(targetVersion);
        
        return new VersionCompatibilityResult
        {
            IsCompatible = isCompatible,
            ComponentId = ComponentId,
            Version = version,
            TargetComponentId = targetComponentId,
            TargetVersion = targetVersion,
            CompatibilityLevel = level,
            Reason = isCompatible ? "Compatible" : "Incompatible version"
        };
    }
}

/// <summary>
/// Represents a version compatibility entry
/// </summary>
public class VersionCompatibilityEntry
{
    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Target component ID
    /// </summary>
    public string TargetComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Compatible version ranges
    /// </summary>
    public List<VersionRange> CompatibleVersions { get; set; } = new List<VersionRange>();
    
    /// <summary>
    /// Incompatible version ranges
    /// </summary>
    public List<VersionRange> IncompatibleVersions { get; set; } = new List<VersionRange>();
    
    /// <summary>
    /// Compatibility notes
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Checks if a version is compatible
    /// </summary>
    /// <param name="targetVersion">Target version</param>
    /// <returns>True if compatible, false otherwise</returns>
    public bool IsVersionCompatible(string targetVersion)
    {
        // Implementation would check if the target version is in the compatible ranges
        // and not in the incompatible ranges
        // This is a placeholder implementation
        return true;
    }
    
    /// <summary>
    /// Gets the compatibility level for a version
    /// </summary>
    /// <param name="targetVersion">Target version</param>
    /// <returns>Compatibility level</returns>
    public CompatibilityLevel GetCompatibilityLevel(string targetVersion)
    {
        // Implementation would determine the compatibility level
        // This is a placeholder implementation
        return CompatibilityLevel.FullyCompatible;
    }
}

/// <summary>
/// Represents a version range
/// </summary>
public class VersionRange
{
    /// <summary>
    /// Minimum version (inclusive)
    /// </summary>
    public string MinVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Maximum version (inclusive)
    /// </summary>
    public string MaxVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the range is a single version
    /// </summary>
    public bool IsSingleVersion => MinVersion == MaxVersion;
    
    /// <summary>
    /// Checks if a version is in the range
    /// </summary>
    /// <param name="version">Version to check</param>
    /// <returns>True if in range, false otherwise</returns>
    public bool IsInRange(string version)
    {
        // Implementation would check if the version is in the range
        // This is a placeholder implementation
        return true;
    }
    
    /// <summary>
    /// Creates a range for a single version
    /// </summary>
    /// <param name="version">Version</param>
    /// <returns>Version range</returns>
    public static VersionRange ForSingleVersion(string version)
    {
        return new VersionRange
        {
            MinVersion = version,
            MaxVersion = version
        };
    }
    
    /// <summary>
    /// Creates a range from a version range expression
    /// </summary>
    /// <param name="rangeExpression">Range expression</param>
    /// <returns>Version range</returns>
    public static VersionRange Parse(string rangeExpression)
    {
        // Implementation would parse the range expression
        // This is a placeholder implementation
        return new VersionRange
        {
            MinVersion = "1.0.0",
            MaxVersion = "2.0.0"
        };
    }
}

/// <summary>
/// Represents a version compatibility result
/// </summary>
public class VersionCompatibilityResult
{
    /// <summary>
    /// Whether the versions are compatible
    /// </summary>
    public bool IsCompatible { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Target component ID
    /// </summary>
    public string TargetComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Target version
    /// </summary>
    public string TargetVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Compatibility level
    /// </summary>
    public CompatibilityLevel CompatibilityLevel { get; set; }
    
    /// <summary>
    /// Compatibility reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;
    
    /// <summary>
    /// Compatibility issues
    /// </summary>
    public List<CompatibilityIssue> Issues { get; set; } = new List<CompatibilityIssue>();
}

/// <summary>
/// Compatibility level enumeration
/// </summary>
public enum CompatibilityLevel
{
    /// <summary>
    /// Unknown compatibility
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Incompatible
    /// </summary>
    Incompatible,
    
    /// <summary>
    /// Partially compatible
    /// </summary>
    PartiallyCompatible,
    
    /// <summary>
    /// Fully compatible
    /// </summary>
    FullyCompatible
}

/// <summary>
/// Represents a compatibility issue
/// </summary>
public class CompatibilityIssue
{
    /// <summary>
    /// Issue type
    /// </summary>
    public string IssueType { get; set; } = string.Empty;
    
    /// <summary>
    /// Issue description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Issue severity
    /// </summary>
    public IssueSeverity Severity { get; set; }
    
    /// <summary>
    /// Affected component
    /// </summary>
    public string AffectedComponent { get; set; } = string.Empty;
    
    /// <summary>
    /// Affected feature
    /// </summary>
    public string? AffectedFeature { get; set; }
    
    /// <summary>
    /// Workaround
    /// </summary>
    public string? Workaround { get; set; }
}
