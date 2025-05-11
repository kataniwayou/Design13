using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.VersionManager;

/// <summary>
/// Represents a version registration request
/// </summary>
public class VersionRegistration
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
    /// Version description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Release notes
    /// </summary>
    public string? ReleaseNotes { get; set; }
    
    /// <summary>
    /// Version dependencies
    /// </summary>
    public List<VersionDependency> Dependencies { get; set; } = new List<VersionDependency>();
    
    /// <summary>
    /// Version features
    /// </summary>
    public List<VersionFeature> Features { get; set; } = new List<VersionFeature>();
    
    /// <summary>
    /// Version artifacts
    /// </summary>
    public List<VersionArtifact> Artifacts { get; set; } = new List<VersionArtifact>();
    
    /// <summary>
    /// Version metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Version tags
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
}

/// <summary>
/// Represents the result of a version registration operation
/// </summary>
public class VersionRegistrationResult
{
    /// <summary>
    /// Whether the registration was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Registration token
    /// </summary>
    public string RegistrationToken { get; set; } = string.Empty;
    
    /// <summary>
    /// Error message if registration failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Validation issues found during registration
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
    
    /// <summary>
    /// Registration timestamp
    /// </summary>
    public DateTime RegistrationTimestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents version information
/// </summary>
public class VersionInfo
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
    /// Version description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Version status
    /// </summary>
    public VersionStatus Status { get; set; }
    
    /// <summary>
    /// Release notes
    /// </summary>
    public string? ReleaseNotes { get; set; }
    
    /// <summary>
    /// Version creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Version creator
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Version last modified timestamp
    /// </summary>
    public DateTime LastModifiedAt { get; set; }
    
    /// <summary>
    /// Version last modifier
    /// </summary>
    public string LastModifiedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Version dependencies
    /// </summary>
    public List<VersionDependency> Dependencies { get; set; } = new List<VersionDependency>();
    
    /// <summary>
    /// Version features
    /// </summary>
    public List<VersionFeature> Features { get; set; } = new List<VersionFeature>();
    
    /// <summary>
    /// Version artifacts
    /// </summary>
    public List<VersionArtifact> Artifacts { get; set; } = new List<VersionArtifact>();
    
    /// <summary>
    /// Version metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Version tags
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
}

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
/// Represents a version feature
/// </summary>
public class VersionFeature
{
    /// <summary>
    /// Feature name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Feature description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the feature is new in this version
    /// </summary>
    public bool IsNew { get; set; } = false;
    
    /// <summary>
    /// Whether the feature is deprecated
    /// </summary>
    public bool IsDeprecated { get; set; } = false;
    
    /// <summary>
    /// Feature documentation URL
    /// </summary>
    public string? DocumentationUrl { get; set; }
}

/// <summary>
/// Represents a version artifact
/// </summary>
public class VersionArtifact
{
    /// <summary>
    /// Artifact name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Artifact type
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Artifact URL
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Artifact checksum
    /// </summary>
    public string? Checksum { get; set; }
    
    /// <summary>
    /// Artifact size in bytes
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Artifact metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
}
