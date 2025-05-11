using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.ServiceManager;

/// <summary>
/// Interface for service management operations
/// </summary>
public interface IServiceManager
{
    // Service registration and discovery
    Task<ServiceRegistrationResult> RegisterServiceAsync(ServiceRegistration registration);
    Task<ServiceInfo> GetServiceInfoAsync(string serviceId, string version);
    Task<IEnumerable<ServiceInfo>> DiscoverServicesAsync(ServiceDiscoveryQuery query);
    Task<bool> UnregisterServiceAsync(string serviceId, string version);
    
    // Service health and status
    Task<ServiceHealthStatus> CheckServiceHealthAsync(string serviceId, string version);
    Task<ServiceStatusUpdate> UpdateServiceStatusAsync(string serviceId, string version, ServiceStatus status);
    Task<IEnumerable<ServiceHealthStatus>> GetAllServiceHealthStatusesAsync();
    
    // Service lifecycle management
    Task<ServiceLifecycleResult> StartServiceAsync(string serviceId, string version);
    Task<ServiceLifecycleResult> StopServiceAsync(string serviceId, string version);
    Task<ServiceLifecycleResult> RestartServiceAsync(string serviceId, string version);
    
    // Service dependency management
    Task<DependencyValidationResult> ValidateServiceDependenciesAsync(string serviceId, string version);
    Task<IEnumerable<ServiceDependency>> GetServiceDependenciesAsync(string serviceId, string version);
    Task<DependencyResolutionResult> ResolveDependencyConflictsAsync(string serviceId, string version);
    
    // Service monitoring and metrics
    Task<ServiceMetrics> GetServiceMetricsAsync(string serviceId, string version, TimeRange timeRange);
    Task<ServiceUsageReport> GenerateServiceUsageReportAsync(string serviceId, string version, TimeRange timeRange);
    Task<ServiceAuditLog> GetServiceAuditLogAsync(string serviceId, string version, TimeRange timeRange);
}
