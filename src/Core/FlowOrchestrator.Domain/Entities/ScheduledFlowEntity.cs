using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a scheduled flow in the system.
/// </summary>
public class ScheduledFlowEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the scheduler ID.
    /// </summary>
    public string SchedulerId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the CRON expression for the schedule.
    /// </summary>
    public string CronExpression { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the start date and time for the schedule.
    /// </summary>
    public DateTime? StartDateTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end date and time for the schedule.
    /// </summary>
    public DateTime? EndDateTime { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of executions.
    /// </summary>
    public int? MaxExecutions { get; set; }
    
    /// <summary>
    /// Gets or sets the current number of executions.
    /// </summary>
    public int CurrentExecutions { get; set; }
    
    /// <summary>
    /// Gets or sets the last execution date and time.
    /// </summary>
    public DateTime? LastExecutionDateTime { get; set; }
    
    /// <summary>
    /// Gets or sets the next execution date and time.
    /// </summary>
    public DateTime? NextExecutionDateTime { get; set; }
    
    /// <summary>
    /// Gets or sets whether this scheduled flow is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the priority of this scheduled flow.
    /// </summary>
    public int Priority { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets the configuration for this scheduled flow.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Creates a new instance of the ScheduledFlowEntity class.
    /// </summary>
    public ScheduledFlowEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ScheduledFlowEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the scheduled flow.</param>
    /// <param name="name">The name of the scheduled flow.</param>
    /// <param name="description">The description of the scheduled flow.</param>
    public ScheduledFlowEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ScheduledFlowEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the scheduled flow.</param>
    /// <param name="name">The name of the scheduled flow.</param>
    /// <param name="description">The description of the scheduled flow.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="schedulerId">The scheduler ID.</param>
    /// <param name="cronExpression">The CRON expression for the schedule.</param>
    /// <param name="startDateTime">The start date and time for the schedule.</param>
    /// <param name="endDateTime">The end date and time for the schedule.</param>
    /// <param name="maxExecutions">The maximum number of executions.</param>
    /// <param name="isEnabled">Whether this scheduled flow is enabled.</param>
    /// <param name="priority">The priority of this scheduled flow.</param>
    /// <param name="configuration">The configuration for this scheduled flow.</param>
    public ScheduledFlowEntity(
        string id,
        string name,
        string description,
        string flowId,
        string schedulerId,
        string cronExpression,
        DateTime? startDateTime,
        DateTime? endDateTime,
        int? maxExecutions,
        bool isEnabled,
        int priority,
        string configuration)
        : base(id, name, description)
    {
        FlowId = flowId;
        SchedulerId = schedulerId;
        CronExpression = cronExpression;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        MaxExecutions = maxExecutions;
        IsEnabled = isEnabled;
        Priority = priority;
        Configuration = configuration;
    }
    
    /// <summary>
    /// Updates the execution information after a flow execution.
    /// </summary>
    /// <param name="executionDateTime">The date and time of the execution.</param>
    /// <param name="nextExecutionDateTime">The next execution date and time.</param>
    public void UpdateExecutionInfo(DateTime executionDateTime, DateTime? nextExecutionDateTime)
    {
        LastExecutionDateTime = executionDateTime;
        NextExecutionDateTime = nextExecutionDateTime;
        CurrentExecutions++;
        
        // Disable the scheduled flow if the maximum number of executions has been reached
        if (MaxExecutions.HasValue && CurrentExecutions >= MaxExecutions.Value)
        {
            IsEnabled = false;
        }
        
        // Disable the scheduled flow if the end date has been reached
        if (EndDateTime.HasValue && DateTime.UtcNow >= EndDateTime.Value)
        {
            IsEnabled = false;
        }
    }
}
