using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a task scheduler that manages scheduled flows.
/// </summary>
public class TaskSchedulerEntity : VersionedEntity
{
    /// <summary>
    /// Gets or sets the list of scheduled flows managed by this scheduler.
    /// </summary>
    public IReadOnlyList<ScheduledFlowEntity> ScheduledFlows => _scheduledFlows.AsReadOnly();
    private readonly List<ScheduledFlowEntity> _scheduledFlows = new();
    
    /// <summary>
    /// Gets or sets the maximum number of concurrent flows that can be executed.
    /// </summary>
    public int MaxConcurrentFlows { get; set; } = 10;
    
    /// <summary>
    /// Gets or sets the polling interval in seconds.
    /// </summary>
    public int PollingIntervalSeconds { get; set; } = 60;
    
    /// <summary>
    /// Gets or sets whether this scheduler is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the time zone ID for this scheduler.
    /// </summary>
    public string TimeZoneId { get; set; } = "UTC";
    
    /// <summary>
    /// Gets or sets the configuration for this scheduler.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Creates a new instance of the TaskSchedulerEntity class.
    /// </summary>
    public TaskSchedulerEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the TaskSchedulerEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the task scheduler.</param>
    /// <param name="name">The name of the task scheduler.</param>
    /// <param name="description">The description of the task scheduler.</param>
    public TaskSchedulerEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the TaskSchedulerEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the task scheduler.</param>
    /// <param name="name">The name of the task scheduler.</param>
    /// <param name="description">The description of the task scheduler.</param>
    /// <param name="version">The semantic version number.</param>
    /// <param name="versionDescription">The human-readable description of this version.</param>
    /// <param name="previousVersionId">The reference to the previous version (if applicable).</param>
    /// <param name="versionStatus">The status of this version.</param>
    public TaskSchedulerEntity(
        string id,
        string name,
        string description,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus)
        : base(id, name, description, version, versionDescription, previousVersionId, versionStatus)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the TaskSchedulerEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the task scheduler.</param>
    /// <param name="name">The name of the task scheduler.</param>
    /// <param name="description">The description of the task scheduler.</param>
    /// <param name="maxConcurrentFlows">The maximum number of concurrent flows that can be executed.</param>
    /// <param name="pollingIntervalSeconds">The polling interval in seconds.</param>
    /// <param name="isEnabled">Whether this scheduler is enabled.</param>
    /// <param name="timeZoneId">The time zone ID for this scheduler.</param>
    /// <param name="configuration">The configuration for this scheduler.</param>
    public TaskSchedulerEntity(
        string id,
        string name,
        string description,
        int maxConcurrentFlows,
        int pollingIntervalSeconds,
        bool isEnabled,
        string timeZoneId,
        string configuration)
        : base(id, name, description)
    {
        MaxConcurrentFlows = maxConcurrentFlows;
        PollingIntervalSeconds = pollingIntervalSeconds;
        IsEnabled = isEnabled;
        TimeZoneId = timeZoneId;
        Configuration = configuration;
    }
    
    /// <summary>
    /// Adds a scheduled flow to this scheduler.
    /// </summary>
    /// <param name="scheduledFlow">The scheduled flow to add.</param>
    public void AddScheduledFlow(ScheduledFlowEntity scheduledFlow)
    {
        _scheduledFlows.Add(scheduledFlow);
    }
    
    /// <summary>
    /// Removes a scheduled flow from this scheduler.
    /// </summary>
    /// <param name="scheduledFlowId">The ID of the scheduled flow to remove.</param>
    /// <returns>True if the scheduled flow was removed, false otherwise.</returns>
    public bool RemoveScheduledFlow(string scheduledFlowId)
    {
        var scheduledFlow = _scheduledFlows.FirstOrDefault(sf => sf.Id == scheduledFlowId);
        if (scheduledFlow != null)
        {
            return _scheduledFlows.Remove(scheduledFlow);
        }
        
        return false;
    }
}
