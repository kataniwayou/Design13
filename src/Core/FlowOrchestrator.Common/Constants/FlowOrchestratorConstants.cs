namespace FlowOrchestrator.Common.Constants;

/// <summary>
/// Constants used throughout the FlowOrchestrator system.
/// </summary>
public static class FlowOrchestratorConstants
{
    /// <summary>
    /// Constants related to flow execution.
    /// </summary>
    public static class Execution
    {
        /// <summary>
        /// The name of the main branch.
        /// </summary>
        public const string MainBranchPath = "main";
        
        /// <summary>
        /// The default priority for branches.
        /// </summary>
        public const int DefaultBranchPriority = 100;
        
        /// <summary>
        /// The maximum number of retry attempts for retryable errors.
        /// </summary>
        public const int DefaultMaxRetryAttempts = 3;
        
        /// <summary>
        /// The default retry delay in milliseconds.
        /// </summary>
        public const int DefaultRetryDelayMs = 1000;
    }
    
    /// <summary>
    /// Constants related to flow configuration.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The default flow configuration file name.
        /// </summary>
        public const string DefaultFlowConfigFileName = "flow.json";
        
        /// <summary>
        /// The default flow configuration schema file name.
        /// </summary>
        public const string DefaultFlowConfigSchemaFileName = "flow-schema.json";
    }
    
    /// <summary>
    /// Constants related to validation.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// The default validation rule error message template.
        /// </summary>
        public const string DefaultErrorMessageTemplate = "Validation rule '{0}' failed: {1}";
    }
    
    /// <summary>
    /// Constants related to versioning.
    /// </summary>
    public static class Versioning
    {
        /// <summary>
        /// The default initial version.
        /// </summary>
        public const string DefaultInitialVersion = "1.0.0";
    }
}
