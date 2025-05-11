namespace FlowOrchestrator.Common.Exceptions;

/// <summary>
/// Exception thrown when a flow validation error occurs.
/// </summary>
public class FlowValidationException : FlowOrchestratorException
{
    /// <summary>
    /// Gets the flow ID that caused the validation error.
    /// </summary>
    public string? FlowId { get; }
    
    /// <summary>
    /// Creates a new instance of the FlowValidationException class.
    /// </summary>
    public FlowValidationException()
        : base("FLOW-ERR-0002", "A flow validation error occurred.")
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowValidationException class with the specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public FlowValidationException(string message)
        : base("FLOW-ERR-0002", message)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowValidationException class with the specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FlowValidationException(string message, Exception innerException)
        : base("FLOW-ERR-0002", message, innerException)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowValidationException class with the specified flow ID and error message.
    /// </summary>
    /// <param name="flowId">The flow ID that caused the validation error.</param>
    /// <param name="message">The error message.</param>
    public FlowValidationException(string flowId, string message)
        : base("FLOW-ERR-0002", message)
    {
        FlowId = flowId;
    }
    
    /// <summary>
    /// Creates a new instance of the FlowValidationException class with the specified flow ID, error message, and inner exception.
    /// </summary>
    /// <param name="flowId">The flow ID that caused the validation error.</param>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FlowValidationException(string flowId, string message, Exception innerException)
        : base("FLOW-ERR-0002", message, innerException)
    {
        FlowId = flowId;
    }
}
