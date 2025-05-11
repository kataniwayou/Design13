using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.TaskScheduler;

/// <summary>
/// Interface for task scheduling operations
/// </summary>
public interface ITaskScheduler
{
    // Task scheduling
    Task<TaskScheduleResult> ScheduleTaskAsync(ScheduledTask task);
    Task<TaskScheduleResult> ScheduleRecurringTaskAsync(RecurringTaskDefinition definition);
    Task<bool> CancelTaskAsync(string taskId);
    Task<bool> PauseTaskAsync(string taskId);
    Task<bool> ResumeTaskAsync(string taskId);
    
    // Task retrieval
    Task<ScheduledTask> GetTaskAsync(string taskId);
    Task<IEnumerable<ScheduledTask>> GetTasksByStatusAsync(TaskStatus status);
    Task<IEnumerable<ScheduledTask>> GetTasksByTypeAsync(string taskType);
    Task<IEnumerable<ScheduledTask>> GetTasksByScheduleTimeAsync(DateTime startTime, DateTime endTime);
    
    // Task execution
    Task<TaskExecutionResult> ExecuteTaskAsync(string taskId);
    Task<TaskExecutionResult> ExecuteTaskImmediatelyAsync(ScheduledTask task);
    Task<TaskExecutionStatus> GetTaskExecutionStatusAsync(string executionId);
    Task<IEnumerable<TaskExecutionSummary>> GetTaskExecutionHistoryAsync(string taskId, int maxResults = 10);
    
    // Task monitoring
    Task<TaskMonitoringResult> GetTaskMonitoringDataAsync(string taskId, TimeRange timeRange);
    Task<IEnumerable<TaskAlert>> GetTaskAlertsAsync(string taskId);
    Task<bool> AcknowledgeTaskAlertAsync(string alertId);
    
    // Recurring task management
    Task<RecurringTaskDefinition> GetRecurringTaskDefinitionAsync(string definitionId);
    Task<bool> UpdateRecurringTaskDefinitionAsync(RecurringTaskDefinition definition);
    Task<bool> DeleteRecurringTaskDefinitionAsync(string definitionId);
    Task<IEnumerable<RecurringTaskDefinition>> GetAllRecurringTaskDefinitionsAsync();
}
