namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Interface for manager services that manage other services or resources.
/// </summary>
public interface IManagerService : IService
{
    /// <summary>
    /// Gets the manager type that this manager implements.
    /// </summary>
    string ManagerType { get; }
    
    /// <summary>
    /// Gets the list of managed services or resources.
    /// </summary>
    /// <returns>The list of managed services or resources.</returns>
    Task<IEnumerable<string>> GetManagedResourcesAsync();
    
    /// <summary>
    /// Gets the status of a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>The status of the managed service or resource.</returns>
    Task<ResourceStatus> GetResourceStatusAsync(string resourceId);
    
    /// <summary>
    /// Starts a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>True if the service or resource was started successfully, false otherwise.</returns>
    Task<bool> StartResourceAsync(string resourceId);
    
    /// <summary>
    /// Stops a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>True if the service or resource was stopped successfully, false otherwise.</returns>
    Task<bool> StopResourceAsync(string resourceId);
    
    /// <summary>
    /// Configures a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <param name="configuration">The configuration for the service or resource.</param>
    /// <returns>True if the service or resource was configured successfully, false otherwise.</returns>
    Task<bool> ConfigureResourceAsync(string resourceId, string configuration);
    
    /// <summary>
    /// Gets the configuration of a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>The configuration of the managed service or resource.</returns>
    Task<string> GetResourceConfigurationAsync(string resourceId);
}
