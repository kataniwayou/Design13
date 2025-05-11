namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the validation was successful.
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Gets or sets the validation errors.
    /// </summary>
    public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    
    /// <summary>
    /// Gets or sets the validation warnings.
    /// </summary>
    public List<ValidationWarning> Warnings { get; set; } = new List<ValidationWarning>();
    
    /// <summary>
    /// Gets or sets the validation information messages.
    /// </summary>
    public List<ValidationInfo> InfoMessages { get; set; } = new List<ValidationInfo>();
    
    /// <summary>
    /// Gets or sets the validation rule that was applied.
    /// </summary>
    public string? ValidationRule { get; set; }
    
    /// <summary>
    /// Gets or sets the validation rule type that was applied.
    /// </summary>
    public string? ValidationRuleType { get; set; }
    
    /// <summary>
    /// Gets or sets the data that was validated.
    /// </summary>
    public object? ValidatedData { get; set; }
    
    /// <summary>
    /// Gets or sets the data type that was validated.
    /// </summary>
    public string? ValidatedDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the validation operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the validation operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the validation operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the validation operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful validation result.
    /// </summary>
    /// <param name="validatedData">The data that was validated.</param>
    /// <param name="validatedDataType">The data type that was validated.</param>
    /// <param name="validationRule">The validation rule that was applied.</param>
    /// <param name="validationRuleType">The validation rule type that was applied.</param>
    /// <returns>A successful validation result.</returns>
    public static ValidationResult Success(object? validatedData, string? validatedDataType, string? validationRule = null, string? validationRuleType = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-100); // Simulate a 100ms validation
        var endTime = DateTime.UtcNow;
        
        return new ValidationResult
        {
            IsValid = true,
            ValidatedData = validatedData,
            ValidatedDataType = validatedDataType,
            ValidationRule = validationRule,
            ValidationRuleType = validationRuleType,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
    
    /// <summary>
    /// Creates a failed validation result.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <param name="validatedData">The data that was validated.</param>
    /// <param name="validatedDataType">The data type that was validated.</param>
    /// <param name="validationRule">The validation rule that was applied.</param>
    /// <param name="validationRuleType">The validation rule type that was applied.</param>
    /// <returns>A failed validation result.</returns>
    public static ValidationResult Failure(List<ValidationError> errors, object? validatedData, string? validatedDataType, string? validationRule = null, string? validationRuleType = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-100); // Simulate a 100ms validation
        var endTime = DateTime.UtcNow;
        
        return new ValidationResult
        {
            IsValid = false,
            Errors = errors,
            ValidatedData = validatedData,
            ValidatedDataType = validatedDataType,
            ValidationRule = validationRule,
            ValidationRuleType = validationRuleType,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
