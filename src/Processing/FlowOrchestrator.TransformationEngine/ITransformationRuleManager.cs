namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Defines the interface for transformation rule management.
/// </summary>
public interface ITransformationRuleManager
{
    /// <summary>
    /// Compiles a rule from its definition.
    /// </summary>
    /// <param name="ruleDefinition">The rule definition.</param>
    /// <param name="language">The rule language.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the compiled rule.</returns>
    Task<TransformationRule> CompileRuleAsync(string ruleDefinition, string language, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Validates a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to validate.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the validation result.</returns>
    Task<RuleValidationResult> ValidateRuleAsync(TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Registers a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to register.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the registration result.</returns>
    Task<RuleRegistrationResult> RegisterRuleAsync(TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Optimizes a transformation rule.
    /// </summary>
    /// <param name="rule">The rule to optimize.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the optimization result.</returns>
    Task<RuleOptimizationResult> OptimizeRuleAsync(TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Discovers transformation rules based on a query.
    /// </summary>
    /// <param name="query">The discovery query.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the discovered rules.</returns>
    Task<IEnumerable<TransformationRule>> DiscoverRulesAsync(RuleDiscoveryQuery query, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the execution history for a rule.
    /// </summary>
    /// <param name="ruleId">The rule ID.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the rule execution history.</returns>
    Task<RuleExecutionHistory> GetRuleExecutionHistoryAsync(string ruleId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Audits rule usage.
    /// </summary>
    /// <param name="ruleId">The rule ID.</param>
    /// <param name="timeRange">The time range for the audit.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the audit result.</returns>
    Task<RuleAuditResult> AuditRuleUsageAsync(string ruleId, TimeRange timeRange, CancellationToken cancellationToken = default);
}
