namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Core service interface that all services must implement.
/// </summary>
public interface IService
{
    /// <summary>
    /// Gets the unique identifier for this service.
    /// </summary>
    string Id { get; }
    
    /// <summary>
    /// Gets the name of this service.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this service.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the version of this service.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Initializes the service with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration for the service.</param>
    /// <returns>True if initialization was successful, false otherwise.</returns>
    bool Initialize(string configuration);
    
    /// <summary>
    /// Starts the service.
    /// </summary>
    /// <returns>True if the service was started successfully, false otherwise.</returns>
    bool Start();
    
    /// <summary>
    /// Stops the service.
    /// </summary>
    /// <returns>True if the service was stopped successfully, false otherwise.</returns>
    bool Stop();
    
    /// <summary>
    /// Gets the current status of the service.
    /// </summary>
    /// <returns>The current status of the service.</returns>
    ServiceStatus GetStatus();
    
    /// <summary>
    /// Gets the health status of the service.
    /// </summary>
    /// <returns>The health status of the service.</returns>
    HealthStatus GetHealthStatus();
}
