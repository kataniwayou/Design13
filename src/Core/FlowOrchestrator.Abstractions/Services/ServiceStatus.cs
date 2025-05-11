namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the status of a service.
/// </summary>
public enum ServiceStatus
{
    /// <summary>
    /// The service is not initialized.
    /// </summary>
    NotInitialized,
    
    /// <summary>
    /// The service is initialized but not started.
    /// </summary>
    Initialized,
    
    /// <summary>
    /// The service is starting.
    /// </summary>
    Starting,
    
    /// <summary>
    /// The service is running.
    /// </summary>
    Running,
    
    /// <summary>
    /// The service is stopping.
    /// </summary>
    Stopping,
    
    /// <summary>
    /// The service is stopped.
    /// </summary>
    Stopped,
    
    /// <summary>
    /// The service has failed.
    /// </summary>
    Failed
}
