namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Represents a validation rule.
/// </summary>
public class ValidationRule
{
    /// <summary>
    /// Gets or sets the property name to validate.
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether the property is required.
    /// </summary>
    public bool Required { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum length of the property value.
    /// </summary>
    public int? MinLength { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum length of the property value.
    /// </summary>
    public int? MaxLength { get; set; }
    
    /// <summary>
    /// Gets or sets the regular expression pattern to match.
    /// </summary>
    public string? Pattern { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum value for numeric properties.
    /// </summary>
    public decimal? MinValue { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum value for numeric properties.
    /// </summary>
    public decimal? MaxValue { get; set; }
    
    /// <summary>
    /// Gets or sets the error message to display if validation fails.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the severity of the validation rule.
    /// </summary>
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
    
    /// <summary>
    /// Gets or sets the custom validation function.
    /// </summary>
    public string? CustomValidation { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this rule.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
