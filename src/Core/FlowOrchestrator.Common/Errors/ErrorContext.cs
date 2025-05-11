namespace FlowOrchestrator.Common.Errors;

/// <summary>
/// Provides context information about an error that occurred during flow execution.
/// </summary>
public class ErrorContext
{
    /// <summary>
    /// Gets or sets the unique identifier for this error.
    /// </summary>
    public string ErrorId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error type.
    /// </summary>
    public string ErrorType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the component where the error occurred.
    /// </summary>
    public string ComponentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the error occurred.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the severity of the error.
    /// </summary>
    public ErrorSeverity Severity { get; set; } = ErrorSeverity.Error;

    /// <summary>
    /// Gets or sets the stack trace of the error.
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// Gets or sets the inner error context, if any.
    /// </summary>
    public ErrorContext? InnerError { get; set; }

    /// <summary>
    /// Gets or sets additional data associated with the error.
    /// </summary>
    public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Creates a new instance of the <see cref="ErrorContext"/> class from an exception.
    /// </summary>
    /// <param name="exception">The exception to create the error context from.</param>
    /// <param name="componentName">The name of the component where the error occurred.</param>
    /// <returns>A new error context.</returns>
    public static ErrorContext FromException(Exception exception, string componentName)
    {
        if (exception == null) throw new ArgumentNullException(nameof(exception));
        if (string.IsNullOrEmpty(componentName)) throw new ArgumentNullException(nameof(componentName));

        var errorContext = new ErrorContext
        {
            ErrorCode = exception.GetType().Name,
            ErrorMessage = exception.Message,
            ErrorType = exception.GetType().FullName ?? exception.GetType().Name,
            ComponentName = componentName,
            StackTrace = exception.StackTrace,
            Severity = ErrorSeverity.Error
        };

        if (exception.InnerException != null)
        {
            errorContext.InnerError = FromException(exception.InnerException, componentName);
        }

        return errorContext;
    }
}


