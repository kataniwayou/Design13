using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.FlowManager;

/// <summary>
/// Represents a flow definition
/// </summary>
public class FlowDefinition
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
    /// Flow components
    /// </summary>
    public List<FlowComponent> Components { get; set; } = new List<FlowComponent>();
    
    /// <summary>
    /// Flow connections
    /// </summary>
    public List<FlowConnection> Connections { get; set; } = new List<FlowConnection>();
    
    /// <summary>
    /// Flow parameters
    /// </summary>
    public List<FlowParameter> Parameters { get; set; } = new List<FlowParameter>();
    
    /// <summary>
    /// Flow configuration
    /// </summary>
    public FlowConfiguration Configuration { get; set; } = new FlowConfiguration();
    
    /// <summary>
    /// Flow metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Flow creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Flow creator
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow last modified timestamp
    /// </summary>
    public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Flow last modifier
    /// </summary>
    public string LastModifiedBy { get; set; } = string.Empty;
}

/// <summary>
/// Represents a flow component
/// </summary>
public class FlowComponent
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Component type
    /// </summary>
    public string ComponentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Component name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Component description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Component version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Component configuration
    /// </summary>
    public Dictionary<string, object> Configuration { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Component position in the flow
    /// </summary>
    public FlowPosition Position { get; set; } = new FlowPosition();
}

/// <summary>
/// Represents a flow connection
/// </summary>
public class FlowConnection
{
    /// <summary>
    /// Connection ID
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Source component ID
    /// </summary>
    public string SourceComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Source port
    /// </summary>
    public string SourcePort { get; set; } = string.Empty;
    
    /// <summary>
    /// Target component ID
    /// </summary>
    public string TargetComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Target port
    /// </summary>
    public string TargetPort { get; set; } = string.Empty;
    
    /// <summary>
    /// Connection condition
    /// </summary>
    public string? Condition { get; set; }
    
    /// <summary>
    /// Connection priority
    /// </summary>
    public int Priority { get; set; } = 0;
    
    /// <summary>
    /// Connection metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
}

/// <summary>
/// Represents a flow parameter
/// </summary>
public class FlowParameter
{
    /// <summary>
    /// Parameter name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Parameter description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Parameter data type
    /// </summary>
    public string DataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Default parameter value
    /// </summary>
    public object? DefaultValue { get; set; }
    
    /// <summary>
    /// Whether the parameter is required
    /// </summary>
    public bool IsRequired { get; set; } = false;
    
    /// <summary>
    /// Parameter validation rules
    /// </summary>
    public List<ParameterValidationRule> ValidationRules { get; set; } = new List<ParameterValidationRule>();
}

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
/// Represents flow configuration
/// </summary>
public class FlowConfiguration
{
    /// <summary>
    /// Execution timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 300;
    
    /// <summary>
    /// Maximum number of retries
    /// </summary>
    public int MaxRetries { get; set; } = 3;
    
    /// <summary>
    /// Retry delay in seconds
    /// </summary>
    public int RetryDelaySeconds { get; set; } = 5;
    
    /// <summary>
    /// Whether to use exponential backoff for retries
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
    
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

/// <summary>
/// Represents a position in the flow
/// </summary>
public class FlowPosition
{
    /// <summary>
    /// X coordinate
    /// </summary>
    public int X { get; set; } = 0;
    
    /// <summary>
    /// Y coordinate
    /// </summary>
    public int Y { get; set; } = 0;
}

/// <summary>
/// Represents the result of a flow registration operation
/// </summary>
public class FlowRegistrationResult
{
    /// <summary>
    /// Whether the registration was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Error message if registration failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Validation issues found during registration
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
}
