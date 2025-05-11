namespace FlowOrchestrator.Common.Errors;

/// <summary>
/// Represents a classification of errors in the system.
/// </summary>
public class ErrorClassification
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string ErrorCode { get; }
    
    /// <summary>
    /// Gets the error category.
    /// </summary>
    public ErrorCategory Category { get; }
    
    /// <summary>
    /// Gets the error severity.
    /// </summary>
    public ErrorSeverity Severity { get; }
    
    /// <summary>
    /// Gets whether the error is retryable.
    /// </summary>
    public bool IsRetryable { get; }
    
    /// <summary>
    /// Gets the error message template.
    /// </summary>
    public string MessageTemplate { get; }
    
    /// <summary>
    /// Gets the recommended action to take when this error occurs.
    /// </summary>
    public string RecommendedAction { get; }
    
    /// <summary>
    /// Gets the documentation URL for this error.
    /// </summary>
    public string? DocumentationUrl { get; }
    
    /// <summary>
    /// Creates a new instance of the ErrorClassification class with the specified properties.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <param name="category">The error category.</param>
    /// <param name="severity">The error severity.</param>
    /// <param name="isRetryable">Whether the error is retryable.</param>
    /// <param name="messageTemplate">The error message template.</param>
    /// <param name="recommendedAction">The recommended action to take when this error occurs.</param>
    /// <param name="documentationUrl">The documentation URL for this error.</param>
    public ErrorClassification(
        string errorCode,
        ErrorCategory category,
        ErrorSeverity severity,
        bool isRetryable,
        string messageTemplate,
        string recommendedAction,
        string? documentationUrl = null)
    {
        ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
        Category = category;
        Severity = severity;
        IsRetryable = isRetryable;
        MessageTemplate = messageTemplate ?? throw new ArgumentNullException(nameof(messageTemplate));
        RecommendedAction = recommendedAction ?? throw new ArgumentNullException(nameof(recommendedAction));
        DocumentationUrl = documentationUrl;
    }
    
    /// <summary>
    /// Formats the error message template with the specified parameters.
    /// </summary>
    /// <param name="parameters">The parameters to use for formatting the message template.</param>
    /// <returns>The formatted error message.</returns>
    public string FormatMessage(params object[] parameters)
    {
        return string.Format(MessageTemplate, parameters);
    }
}
