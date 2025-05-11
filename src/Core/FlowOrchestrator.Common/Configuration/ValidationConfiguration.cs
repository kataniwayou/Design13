namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for validation.
/// </summary>
public class ValidationConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether validation is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the validation rules.
    /// </summary>
    public List<string> ValidationRules { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the validation rule types.
    /// </summary>
    public List<string> ValidationRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets a value indicating whether to fail on validation errors.
    /// </summary>
    public bool FailOnValidationErrors { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to fail on validation warnings.
    /// </summary>
    public bool FailOnValidationWarnings { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the maximum number of validation errors to report.
    /// </summary>
    public int MaxValidationErrors { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets the maximum number of validation warnings to report.
    /// </summary>
    public int MaxValidationWarnings { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets the additional parameters for validation.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
