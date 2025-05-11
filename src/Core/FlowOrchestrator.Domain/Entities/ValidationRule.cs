using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IValidationRule interface.
/// </summary>
public class ValidationRule : BaseEntity, IValidationRule
{
    /// <summary>
    /// Gets or sets the rule type.
    /// </summary>
    public string RuleType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the rule configuration.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the severity of the rule.
    /// </summary>
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
    
    /// <summary>
    /// Gets or sets the error message template for when the rule fails.
    /// </summary>
    public string ErrorMessageTemplate { get; set; } = string.Empty;
    
    /// <summary>
    /// Creates a new instance of the ValidationRule class.
    /// </summary>
    public ValidationRule()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ValidationRule class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the rule.</param>
    /// <param name="name">The name of the rule.</param>
    /// <param name="description">The description of the rule.</param>
    public ValidationRule(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ValidationRule class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the rule.</param>
    /// <param name="name">The name of the rule.</param>
    /// <param name="description">The description of the rule.</param>
    /// <param name="ruleType">The rule type.</param>
    /// <param name="configuration">The rule configuration.</param>
    /// <param name="severity">The severity of the rule.</param>
    /// <param name="errorMessageTemplate">The error message template for when the rule fails.</param>
    public ValidationRule(
        string id,
        string name,
        string description,
        string ruleType,
        string configuration,
        ValidationSeverity severity,
        string errorMessageTemplate)
        : base(id, name, description)
    {
        RuleType = ruleType;
        Configuration = configuration;
        Severity = severity;
        ErrorMessageTemplate = errorMessageTemplate;
    }
}
