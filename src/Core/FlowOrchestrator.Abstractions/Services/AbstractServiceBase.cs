namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Abstract base class for all services.
/// </summary>
public abstract class AbstractServiceBase : IService
{
    /// <summary>
    /// Gets the unique identifier for this service.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the name of this service.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Gets the description of this service.
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Gets the version of this service.
    /// </summary>
    public string Version { get; }
    
    /// <summary>
    /// Gets or sets the current status of the service.
    /// </summary>
    protected ServiceStatus Status { get; set; }
    
    /// <summary>
    /// Gets or sets the current health status of the service.
    /// </summary>
    protected HealthStatus Health { get; set; }
    
    /// <summary>
    /// Gets or sets the configuration for this service.
    /// </summary>
    protected string Configuration { get; set; }
    
    /// <summary>
    /// Gets or sets whether the service is initialized.
    /// </summary>
    protected bool IsInitialized { get; set; }
    
    /// <summary>
    /// Gets or sets whether the service is running.
    /// </summary>
    protected bool IsRunning { get; set; }
    
    /// <summary>
    /// Creates a new instance of the AbstractServiceBase class.
    /// </summary>
    /// <param name="id">The unique identifier for the service.</param>
    /// <param name="name">The name of the service.</param>
    /// <param name="description">The description of the service.</param>
    /// <param name="version">The version of the service.</param>
    protected AbstractServiceBase(string id, string name, string description, string version)
    {
        Id = id;
        Name = name;
        Description = description;
        Version = version;
        Status = ServiceStatus.NotInitialized;
        Health = HealthStatus.Unknown;
        Configuration = string.Empty;
        IsInitialized = false;
        IsRunning = false;
    }
    
    /// <summary>
    /// Initializes the service with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration for the service.</param>
    /// <returns>True if initialization was successful, false otherwise.</returns>
    public virtual bool Initialize(string configuration)
    {
        if (IsInitialized)
        {
            return true;
        }
        
        Configuration = configuration;
        Status = ServiceStatus.Initialized;
        IsInitialized = true;
        return true;
    }
    
    /// <summary>
    /// Starts the service.
    /// </summary>
    /// <returns>True if the service was started successfully, false otherwise.</returns>
    public virtual bool Start()
    {
        if (!IsInitialized)
        {
            return false;
        }
        
        if (IsRunning)
        {
            return true;
        }
        
        Status = ServiceStatus.Starting;
        
        try
        {
            // Derived classes should override this method and call base.Start()
            Status = ServiceStatus.Running;
            Health = HealthStatus.Healthy;
            IsRunning = true;
            return true;
        }
        catch
        {
            Status = ServiceStatus.Failed;
            Health = HealthStatus.Unhealthy;
            IsRunning = false;
            return false;
        }
    }
    
    /// <summary>
    /// Stops the service.
    /// </summary>
    /// <returns>True if the service was stopped successfully, false otherwise.</returns>
    public virtual bool Stop()
    {
        if (!IsRunning)
        {
            return true;
        }
        
        Status = ServiceStatus.Stopping;
        
        try
        {
            // Derived classes should override this method and call base.Stop()
            Status = ServiceStatus.Stopped;
            IsRunning = false;
            return true;
        }
        catch
        {
            Status = ServiceStatus.Failed;
            Health = HealthStatus.Unhealthy;
            return false;
        }
    }
    
    /// <summary>
    /// Gets the current status of the service.
    /// </summary>
    /// <returns>The current status of the service.</returns>
    public virtual ServiceStatus GetStatus()
    {
        return Status;
    }
    
    /// <summary>
    /// Gets the health status of the service.
    /// </summary>
    /// <returns>The health status of the service.</returns>
    public virtual HealthStatus GetHealthStatus()
    {
        return Health;
    }
}
