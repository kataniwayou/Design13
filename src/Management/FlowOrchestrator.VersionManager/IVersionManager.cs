using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.VersionManager;

/// <summary>
/// Interface for version management operations
/// </summary>
public interface IVersionManager
{
    // Version registration and discovery
    Task<VersionRegistrationResult> RegisterVersionAsync(VersionRegistration registration);
    Task<VersionInfo> GetVersionInfoAsync(string componentId, string version);
    Task<IEnumerable<VersionInfo>> GetVersionHistoryAsync(string componentId);
    Task<bool> UnregisterVersionAsync(string componentId, string version);
    
    // Version compatibility
    Task<VersionCompatibilityResult> CheckVersionCompatibilityAsync(string componentId, string version, string targetComponentId, string targetVersion);
    Task<VersionCompatibilityMatrix> GetCompatibilityMatrixAsync(string componentId);
    Task<bool> UpdateCompatibilityMatrixAsync(VersionCompatibilityMatrix matrix);
    
    // Version lifecycle management
    Task<VersionLifecycleResult> ChangeVersionStatusAsync(string componentId, string version, VersionStatus status, string reason);
    Task<VersionLifecycleResult> DeprecateVersionAsync(string componentId, string version, string reason);
    Task<VersionLifecycleResult> ArchiveVersionAsync(string componentId, string version, string reason);
    
    // Version validation
    Task<VersionValidationResult> ValidateVersionAsync(string componentId, string version);
    Task<VersionDependencyResult> AnalyzeVersionDependenciesAsync(string componentId, string version);
    Task<VersionImpactAnalysisResult> AnalyzeVersionImpactAsync(string componentId, string version);
    
    // Version discovery
    Task<IEnumerable<VersionSummary>> DiscoverVersionsAsync(VersionDiscoveryQuery query);
    Task<VersionSearchResult> SearchVersionsAsync(string searchTerm, int maxResults = 20);
    
    // Version comparison
    Task<VersionComparisonResult> CompareVersionsAsync(string componentId, string version1, string version2);
    Task<VersionDiffResult> GetVersionDiffAsync(string componentId, string version1, string version2);
}
