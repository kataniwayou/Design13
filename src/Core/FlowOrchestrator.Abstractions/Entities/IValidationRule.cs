namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for a validation rule in a flow.
/// </summary>
public interface IValidationRule : IEntity
{
    /// <summary>
    /// Gets the rule type.
    /// </summary>
    string RuleType { get; }
    
    /// <summary>
    /// Gets the rule configuration.
    /// </summary>
    string Configuration { get; }
    
    /// <summary>
    /// Gets the severity of the rule.
    /// </summary>
    ValidationSeverity Severity { get; }
    
    /// <summary>
    /// Gets the error message template for when the rule fails.
    /// </summary>
    string ErrorMessageTemplate { get; }
}
