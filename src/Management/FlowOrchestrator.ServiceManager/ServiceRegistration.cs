using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.ServiceManager;

/// <summary>
/// Represents a service registration request
/// </summary>
public class ServiceRegistration
{
    /// <summary>
    /// Unique identifier for the service
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Service name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Service description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Service type
    /// </summary>
    public ServiceType Type { get; set; }
    
    /// <summary>
    /// Service endpoint information
    /// </summary>
    public ServiceEndpoint Endpoint { get; set; } = new ServiceEndpoint();
    
    /// <summary>
    /// Service capabilities
    /// </summary>
    public List<ServiceCapability> Capabilities { get; set; } = new List<ServiceCapability>();
    
    /// <summary>
    /// Service dependencies
    /// </summary>
    public List<ServiceDependency> Dependencies { get; set; } = new List<ServiceDependency>();
    
    /// <summary>
    /// Service health check configuration
    /// </summary>
    public HealthCheckConfiguration HealthCheck { get; set; } = new HealthCheckConfiguration();
    
    /// <summary>
    /// Service metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
}

/// <summary>
/// Represents the result of a service registration operation
/// </summary>
public class ServiceRegistrationResult
{
    /// <summary>
    /// Whether the registration was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Registration token for the service
    /// </summary>
    public string RegistrationToken { get; set; } = string.Empty;
    
    /// <summary>
    /// Error message if registration failed
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Validation issues found during registration
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
    
    /// <summary>
    /// Timestamp of the registration
    /// </summary>
    public DateTime RegistrationTimestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents service endpoint information
/// </summary>
public class ServiceEndpoint
{
    /// <summary>
    /// Service address
    /// </summary>
    public string Address { get; set; } = string.Empty;
    
    /// <summary>
    /// Service port
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Service protocol
    /// </summary>
    public string Protocol { get; set; } = "http";
    
    /// <summary>
    /// Service path
    /// </summary>
    public string Path { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the service uses TLS
    /// </summary>
    public bool UseTls { get; set; }
    
    /// <summary>
    /// Authentication type for the service
    /// </summary>
    public string AuthenticationType { get; set; } = string.Empty;
}

/// <summary>
/// Represents a service capability
/// </summary>
public class ServiceCapability
{
    /// <summary>
    /// Capability name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Capability version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Capability description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Capability parameters
    /// </summary>
    public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}

/// <summary>
/// Represents a service dependency
/// </summary>
public class ServiceDependency
{
    /// <summary>
    /// Dependency service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
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
/// Represents health check configuration for a service
/// </summary>
public class HealthCheckConfiguration
{
    /// <summary>
    /// Health check endpoint path
    /// </summary>
    public string Path { get; set; } = "/health";
    
    /// <summary>
    /// Health check interval in seconds
    /// </summary>
    public int IntervalSeconds { get; set; } = 30;
    
    /// <summary>
    /// Health check timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 5;
    
    /// <summary>
    /// Number of failures before marking service as unhealthy
    /// </summary>
    public int FailureThreshold { get; set; } = 3;
    
    /// <summary>
    /// Number of successes before marking service as healthy
    /// </summary>
    public int SuccessThreshold { get; set; } = 1;
}

/// <summary>
/// Dependency type enumeration
/// </summary>
public enum DependencyType
{
    /// <summary>
    /// Runtime dependency
    /// </summary>
    Runtime,
    
    /// <summary>
    /// Build-time dependency
    /// </summary>
    BuildTime,
    
    /// <summary>
    /// Optional dependency
    /// </summary>
    Optional
}

/// <summary>
/// Service type enumeration
/// </summary>
public enum ServiceType
{
    /// <summary>
    /// Core service
    /// </summary>
    Core,
    
    /// <summary>
    /// Infrastructure service
    /// </summary>
    Infrastructure,
    
    /// <summary>
    /// Processing service
    /// </summary>
    Processing,
    
    /// <summary>
    /// Integration service
    /// </summary>
    Integration,
    
    /// <summary>
    /// Management service
    /// </summary>
    Management,
    
    /// <summary>
    /// Observability service
    /// </summary>
    Observability
}
