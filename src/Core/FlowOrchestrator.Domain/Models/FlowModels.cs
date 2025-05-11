namespace FlowOrchestrator.Domain.Models;

/// <summary>
/// Represents a parameter validation rule
/// </summary>
public class ParameterValidationRule
{
    /// <summary>
    /// Rule type
    /// </summary>
    public string RuleType { get; set; } = string.Empty;

    /// <summary>
    /// Rule parameters
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Error message
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}

/// <summary>
/// Represents a flow discovery query
/// </summary>
public class FlowDiscoveryQuery
{
    /// <summary>
    /// Flow ID pattern
    /// </summary>
    public string? FlowIdPattern { get; set; }

    /// <summary>
    /// Flow name pattern
    /// </summary>
    public string? NamePattern { get; set; }

    /// <summary>
    /// Flow category
    /// </summary>
    public string? Category { get; set; }

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
/// Represents a flow summary
/// </summary>
public class FlowSummary
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
    /// Flow name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Flow description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Flow category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Flow tags
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();

    /// <summary>
    /// Flow creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Flow creator
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Flow last modified timestamp
    /// </summary>
    public DateTime LastModifiedAt { get; set; }

    /// <summary>
    /// Flow last modifier
    /// </summary>
    public string LastModifiedBy { get; set; } = string.Empty;
}

/// <summary>
/// Represents a flow search result
/// </summary>
public class FlowSearchResult
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
    public List<FlowSummary> Results { get; set; } = new List<FlowSummary>();
}

/// <summary>
/// Represents a flow category
/// </summary>
public class FlowCategory
{
    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Category description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Category color
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Category icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Category order
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Number of flows in the category
    /// </summary>
    public int FlowCount { get; set; }
}

/// <summary>
/// Represents a flow validation result
/// </summary>
public class FlowValidationResult
{
    /// <summary>
    /// Whether the flow is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;

    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Validation issues
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
}

/// <summary>
/// Represents a flow compatibility result
/// </summary>
public class FlowCompatibilityResult
{
    /// <summary>
    /// Whether the flow is compatible
    /// </summary>
    public bool IsCompatible { get; set; }

    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;

    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Target environment
    /// </summary>
    public string TargetEnvironment { get; set; } = string.Empty;

    /// <summary>
    /// Compatibility issues
    /// </summary>
    public List<CompatibilityIssue> CompatibilityIssues { get; set; } = new List<CompatibilityIssue>();
}

/// <summary>
/// Represents a flow dependency result
/// </summary>
public class FlowDependencyResult
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
    /// Direct dependencies
    /// </summary>
    public List<FlowDependency> DirectDependencies { get; set; } = new List<FlowDependency>();

    /// <summary>
    /// Transitive dependencies
    /// </summary>
    public List<FlowDependency> TransitiveDependencies { get; set; } = new List<FlowDependency>();

    /// <summary>
    /// Unresolved dependencies
    /// </summary>
    public List<UnresolvedDependency> UnresolvedDependencies { get; set; } = new List<UnresolvedDependency>();
}

/// <summary>
/// Represents a flow dependency
/// </summary>
public class FlowDependency
{
    /// <summary>
    /// Dependency type
    /// </summary>
    public string DependencyType { get; set; } = string.Empty;

    /// <summary>
    /// Dependency ID
    /// </summary>
    public string DependencyId { get; set; } = string.Empty;

    /// <summary>
    /// Dependency version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Whether the dependency is required
    /// </summary>
    public bool IsRequired { get; set; } = true;
}

/// <summary>
/// Represents flow execution parameters
/// </summary>
public class FlowExecutionParameters
{
    /// <summary>
    /// Parameter values
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Execution context
    /// </summary>
    public Dictionary<string, object> Context { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Execution options
    /// </summary>
    public FlowExecutionOptions Options { get; set; } = new FlowExecutionOptions();
}

/// <summary>
/// Represents flow execution options
/// </summary>
public class FlowExecutionOptions
{
    /// <summary>
    /// Execution timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 300;

    /// <summary>
    /// Whether to enable parallel execution
    /// </summary>
    public bool EnableParallelExecution { get; set; } = false;

    /// <summary>
    /// Maximum degree of parallelism
    /// </summary>
    public int MaxParallelism { get; set; } = 4;

    /// <summary>
    /// Whether to enable logging
    /// </summary>
    public bool EnableLogging { get; set; } = true;

    /// <summary>
    /// Logging level
    /// </summary>
    public string LoggingLevel { get; set; } = "Information";

    /// <summary>
    /// Whether to enable metrics collection
    /// </summary>
    public bool EnableMetrics { get; set; } = true;

    /// <summary>
    /// Whether to enable tracing
    /// </summary>
    public bool EnableTracing { get; set; } = true;

    /// <summary>
    /// Error handling mode
    /// </summary>
    public string ErrorHandlingMode { get; set; } = "ContinueOnError";
}
