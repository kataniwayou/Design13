using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.ConfigurationManager;

/// <summary>
/// Interface for configuration management operations
/// </summary>
public interface IConfigurationManager
{
    // Configuration store operations
    Task<ConfigurationItem> GetConfigurationAsync(string key, string environment);
    Task<bool> SetConfigurationAsync(ConfigurationItem item);
    Task<bool> DeleteConfigurationAsync(string key, string environment);
    Task<IEnumerable<ConfigurationItem>> GetConfigurationsByPrefixAsync(string prefix, string environment);
    
    // Configuration validation
    Task<ConfigurationValidationResult> ValidateConfigurationAsync(ConfigurationItem item);
    Task<ConfigurationValidationResult> ValidateConfigurationSetAsync(IEnumerable<ConfigurationItem> items);
    Task<bool> RegisterValidationSchemaAsync(ConfigurationValidationSchema schema);
    
    // Configuration deployment
    Task<ConfigurationDeploymentResult> DeployConfigurationAsync(string key, string sourceEnvironment, string targetEnvironment);
    Task<ConfigurationDeploymentResult> DeployConfigurationSetAsync(IEnumerable<string> keys, string sourceEnvironment, string targetEnvironment);
    Task<ConfigurationDeploymentStatus> GetDeploymentStatusAsync(string deploymentId);
    
    // Parameter schema management
    Task<ParameterSchema> GetParameterSchemaAsync(string schemaId, string version);
    Task<bool> RegisterParameterSchemaAsync(ParameterSchema schema);
    Task<bool> UpdateParameterSchemaAsync(ParameterSchema schema);
    Task<bool> DeleteParameterSchemaAsync(string schemaId, string version);
    
    // Configuration history and versioning
    Task<IEnumerable<ConfigurationHistoryEntry>> GetConfigurationHistoryAsync(string key, string environment, int maxEntries = 10);
    Task<ConfigurationItem> GetConfigurationVersionAsync(string key, string environment, string version);
    Task<bool> RevertToVersionAsync(string key, string environment, string version);
    
    // Environment management
    Task<IEnumerable<EnvironmentInfo>> GetEnvironmentsAsync();
    Task<bool> CreateEnvironmentAsync(EnvironmentInfo environment);
    Task<bool> UpdateEnvironmentAsync(EnvironmentInfo environment);
    Task<bool> DeleteEnvironmentAsync(string environmentName);
}
