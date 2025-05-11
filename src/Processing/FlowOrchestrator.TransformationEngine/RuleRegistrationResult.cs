namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a rule registration operation.
/// </summary>
public class RuleRegistrationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the registration was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the registration failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the rule that was registered.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the registration ID.
    /// </summary>
    public string RegistrationId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the timestamp when the registration was performed.
    /// </summary>
    public DateTime RegistrationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the registration.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful registration result.
    /// </summary>
    /// <param name="rule">The rule that was registered.</param>
    /// <returns>A successful registration result.</returns>
    public static RuleRegistrationResult Success(TransformationRule rule)
    {
        return new RuleRegistrationResult
        {
            IsSuccessful = true,
            Rule = rule
        };
    }
    
    /// <summary>
    /// Creates a failed registration result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="rule">The rule that was registered.</param>
    /// <returns>A failed registration result.</returns>
    public static RuleRegistrationResult Failure(string errorMessage, TransformationRule rule)
    {
        return new RuleRegistrationResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Rule = rule
        };
    }
}
