namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a rule validation operation.
/// </summary>
public class RuleValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the rule is valid.
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the rule is invalid.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the validation errors.
    /// </summary>
    public List<string> ValidationErrors { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the validation warnings.
    /// </summary>
    public List<string> ValidationWarnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the rule that was validated.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the validation was performed.
    /// </summary>
    public DateTime ValidationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the validation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a valid rule validation result.
    /// </summary>
    /// <param name="rule">The rule that was validated.</param>
    /// <returns>A valid rule validation result.</returns>
    public static RuleValidationResult Valid(TransformationRule rule)
    {
        return new RuleValidationResult
        {
            IsValid = true,
            Rule = rule
        };
    }
    
    /// <summary>
    /// Creates an invalid rule validation result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="rule">The rule that was validated.</param>
    /// <param name="validationErrors">The validation errors.</param>
    /// <returns>An invalid rule validation result.</returns>
    public static RuleValidationResult Invalid(string errorMessage, TransformationRule rule, List<string>? validationErrors = null)
    {
        return new RuleValidationResult
        {
            IsValid = false,
            ErrorMessage = errorMessage,
            ValidationErrors = validationErrors ?? new List<string> { errorMessage },
            Rule = rule
        };
    }
}
