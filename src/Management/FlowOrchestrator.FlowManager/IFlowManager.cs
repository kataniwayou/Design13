using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.FlowManager;

/// <summary>
/// Interface for flow management operations
/// </summary>
public interface IFlowManager
{
    // Flow definition management
    Task<FlowRegistrationResult> RegisterFlowAsync(FlowDefinition flowDefinition);
    Task<FlowDefinition> GetFlowDefinitionAsync(string flowId, string version);
    Task<bool> UpdateFlowDefinitionAsync(FlowDefinition flowDefinition);
    Task<bool> DeleteFlowAsync(string flowId, string version);
    
    // Flow versioning
    Task<FlowVersionInfo> GetFlowVersionInfoAsync(string flowId, string version);
    Task<IEnumerable<FlowVersionInfo>> GetFlowVersionHistoryAsync(string flowId);
    Task<FlowVersioningResult> CreateNewFlowVersionAsync(string flowId, string baseVersion, FlowDefinition newDefinition);
    Task<bool> DeprecateFlowVersionAsync(string flowId, string version, string reason);
    
    // Flow deployment
    Task<FlowDeploymentResult> DeployFlowAsync(string flowId, string version, string environment);
    Task<FlowDeploymentResult> UndeployFlowAsync(string flowId, string version, string environment);
    Task<FlowDeploymentStatus> GetFlowDeploymentStatusAsync(string flowId, string version, string environment);
    Task<IEnumerable<FlowDeploymentInfo>> GetFlowDeploymentsAsync(string flowId, string version);
    
    // Flow discovery
    Task<IEnumerable<FlowSummary>> DiscoverFlowsAsync(FlowDiscoveryQuery query);
    Task<FlowSearchResult> SearchFlowsAsync(string searchTerm, int maxResults = 20);
    Task<IEnumerable<FlowCategory>> GetFlowCategoriesAsync();
    
    // Flow validation
    Task<FlowValidationResult> ValidateFlowAsync(FlowDefinition flowDefinition);
    Task<FlowCompatibilityResult> CheckFlowCompatibilityAsync(string flowId, string version, string targetEnvironment);
    Task<FlowDependencyResult> AnalyzeFlowDependenciesAsync(string flowId, string version);
    
    // Flow execution management
    Task<FlowExecutionResult> ExecuteFlowAsync(string flowId, string version, FlowExecutionParameters parameters);
    Task<FlowExecutionStatus> GetFlowExecutionStatusAsync(string executionId);
    Task<bool> CancelFlowExecutionAsync(string executionId);
    Task<IEnumerable<FlowExecutionSummary>> GetFlowExecutionHistoryAsync(string flowId, string version, int maxResults = 20);
    
    // Flow analytics
    Task<FlowUsageStatistics> GetFlowUsageStatisticsAsync(string flowId, string version, TimeRange timeRange);
    Task<FlowPerformanceMetrics> GetFlowPerformanceMetricsAsync(string flowId, string version, TimeRange timeRange);
    Task<FlowAuditLog> GetFlowAuditLogAsync(string flowId, string version, TimeRange timeRange);
}
