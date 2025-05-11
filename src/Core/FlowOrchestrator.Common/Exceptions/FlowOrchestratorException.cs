namespace FlowOrchestrator.Common.Exceptions;

/// <summary>
/// Base exception class for all FlowOrchestrator exceptions.
/// </summary>
public class FlowOrchestratorException : Exception
{
    /// <summary>
    /// Gets the error code for this exception.
    /// </summary>
    public string ErrorCode { get; }
    
    /// <summary>
    /// Creates a new instance of the FlowOrchestratorException class.
    /// </summary>
    public FlowOrchestratorException()
        : base("An error occurred in the FlowOrchestrator system.")
    {
        ErrorCode = "FLOW-ERR-0001";
    }
    
    /// <summary>
    /// Creates a new instance of the FlowOrchestratorException class with the specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public FlowOrchestratorException(string message)
        : base(message)
    {
        ErrorCode = "FLOW-ERR-0001";
    }
    
    /// <summary>
    /// Creates a new instance of the FlowOrchestratorException class with the specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FlowOrchestratorException(string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = "FLOW-ERR-0001";
    }
    
    /// <summary>
    /// Creates a new instance of the FlowOrchestratorException class with the specified error code and error message.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <param name="message">The error message.</param>
    public FlowOrchestratorException(string errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
    
    /// <summary>
    /// Creates a new instance of the FlowOrchestratorException class with the specified error code, error message, and inner exception.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FlowOrchestratorException(string errorCode, string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
