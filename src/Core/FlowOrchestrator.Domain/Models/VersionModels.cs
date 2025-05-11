namespace FlowOrchestrator.Domain.Models;

/// <summary>
/// Represents a version dependency
/// </summary>
public class VersionDependency
{
    /// <summary>
    /// Dependency component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// Dependency version or version range
    /// </summary>
    public string VersionRange { get; set; } = string.Empty;

    /// <summary>
    /// Whether the dependency is required
    /// </summary>
    public bool IsRequired { get; set; } = true;

    /// <summary>
    /// Dependency type
    /// </summary>
    public DependencyType Type { get; set; } = DependencyType.Runtime;
}

/// <summary>
/// Represents a version validation result
/// </summary>
public class VersionValidationResult
{
    /// <summary>
    /// Whether the version is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Validation issues
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
}

/// <summary>
/// Represents a version dependency result
/// </summary>
public class VersionDependencyResult
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Direct dependencies
    /// </summary>
    public List<VersionDependency> DirectDependencies { get; set; } = new List<VersionDependency>();

    /// <summary>
    /// Transitive dependencies
    /// </summary>
    public List<VersionDependency> TransitiveDependencies { get; set; } = new List<VersionDependency>();

    /// <summary>
    /// Unresolved dependencies
    /// </summary>
    public List<UnresolvedDependency> UnresolvedDependencies { get; set; } = new List<UnresolvedDependency>();
}

/// <summary>
/// Represents a version impact analysis result
/// </summary>
public class VersionImpactAnalysisResult
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Affected components
    /// </summary>
    public List<AffectedComponent> AffectedComponents { get; set; } = new List<AffectedComponent>();

    /// <summary>
    /// Affected flows
    /// </summary>
    public List<AffectedFlow> AffectedFlows { get; set; } = new List<AffectedFlow>();

    /// <summary>
    /// Affected environments
    /// </summary>
    public List<AffectedEnvironment> AffectedEnvironments { get; set; } = new List<AffectedEnvironment>();
}

/// <summary>
/// Represents an affected component
/// </summary>
public class AffectedComponent
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Impact level
    /// </summary>
    public ImpactLevel ImpactLevel { get; set; }

    /// <summary>
    /// Impact reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}

/// <summary>
/// Represents an affected flow
/// </summary>
public class AffectedFlow
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;

    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Impact level
    /// </summary>
    public ImpactLevel ImpactLevel { get; set; }

    /// <summary>
    /// Impact reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}

/// <summary>
/// Represents an affected environment
/// </summary>
public class AffectedEnvironment
{
    /// <summary>
    /// Environment name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Impact level
    /// </summary>
    public ImpactLevel ImpactLevel { get; set; }

    /// <summary>
    /// Impact reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Affected deployments
    /// </summary>
    public List<string> AffectedDeployments { get; set; } = new List<string>();
}

/// <summary>
/// Impact level enumeration
/// </summary>
public enum ImpactLevel
{
    /// <summary>
    /// No impact
    /// </summary>
    None,

    /// <summary>
    /// Low impact
    /// </summary>
    Low,

    /// <summary>
    /// Medium impact
    /// </summary>
    Medium,

    /// <summary>
    /// High impact
    /// </summary>
    High,

    /// <summary>
    /// Critical impact
    /// </summary>
    Critical
}

/// <summary>
/// Represents a version discovery query
/// </summary>
public class VersionDiscoveryQuery
{
    /// <summary>
    /// Component ID pattern
    /// </summary>
    public string? ComponentIdPattern { get; set; }

    /// <summary>
    /// Component type
    /// </summary>
    public string? ComponentType { get; set; }

    /// <summary>
    /// Version status
    /// </summary>
    public VersionStatus? Status { get; set; }

    /// <summary>
    /// Tags to filter by
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();

    /// <summary>
    /// Maximum number of results to return
    /// </summary>
    public int MaxResults { get; set; } = 100;

    /// <summary>
    /// Whether to include details in the results
    /// </summary>
    public bool IncludeDetails { get; set; } = false;
}

/// <summary>
/// Represents a version summary
/// </summary>
public class VersionSummary
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Component name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Component type
    /// </summary>
    public string ComponentType { get; set; } = string.Empty;

    /// <summary>
    /// Version status
    /// </summary>
    public VersionStatus Status { get; set; }

    /// <summary>
    /// Version creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Version creator
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Version tags
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
}

/// <summary>
/// Represents a version search result
/// </summary>
public class VersionSearchResult
{
    /// <summary>
    /// Search term
    /// </summary>
    public string SearchTerm { get; set; } = string.Empty;

    /// <summary>
    /// Total results
    /// </summary>
    public int TotalResults { get; set; }

    /// <summary>
    /// Results
    /// </summary>
    public List<VersionSummary> Results { get; set; } = new List<VersionSummary>();
}

/// <summary>
/// Represents a version comparison result
/// </summary>
public class VersionComparisonResult
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// First version
    /// </summary>
    public string Version1 { get; set; } = string.Empty;

    /// <summary>
    /// Second version
    /// </summary>
    public string Version2 { get; set; } = string.Empty;

    /// <summary>
    /// Differences
    /// </summary>
    public List<VersionDifference> Differences { get; set; } = new List<VersionDifference>();

    /// <summary>
    /// Breaking changes
    /// </summary>
    public List<BreakingChange> BreakingChanges { get; set; } = new List<BreakingChange>();
}

/// <summary>
/// Represents a version difference
/// </summary>
public class VersionDifference
{
    /// <summary>
    /// Difference type
    /// </summary>
    public string DifferenceType { get; set; } = string.Empty;

    /// <summary>
    /// Path
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Value in the first version
    /// </summary>
    public object? Value1 { get; set; }

    /// <summary>
    /// Value in the second version
    /// </summary>
    public object? Value2 { get; set; }
}

/// <summary>
/// Represents a breaking change
/// </summary>
public class BreakingChange
{
    /// <summary>
    /// Change type
    /// </summary>
    public string ChangeType { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Impact
    /// </summary>
    public string Impact { get; set; } = string.Empty;

    /// <summary>
    /// Mitigation
    /// </summary>
    public string? Mitigation { get; set; }
}

/// <summary>
/// Represents a version diff result
/// </summary>
public class VersionDiffResult
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    /// First version
    /// </summary>
    public string Version1 { get; set; } = string.Empty;

    /// <summary>
    /// Second version
    /// </summary>
    public string Version2 { get; set; } = string.Empty;

    /// <summary>
    /// Added items
    /// </summary>
    public List<DiffItem> AddedItems { get; set; } = new List<DiffItem>();

    /// <summary>
    /// Removed items
    /// </summary>
    public List<DiffItem> RemovedItems { get; set; } = new List<DiffItem>();

    /// <summary>
    /// Modified items
    /// </summary>
    public List<DiffItem> ModifiedItems { get; set; } = new List<DiffItem>();
}

/// <summary>
/// Represents a diff item
/// </summary>
public class DiffItem
{
    /// <summary>
    /// Item type
    /// </summary>
    public string ItemType { get; set; } = string.Empty;

    /// <summary>
    /// Item name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Item path
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    public string? Details { get; set; }
}
