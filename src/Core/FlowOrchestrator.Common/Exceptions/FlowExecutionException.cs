namespace FlowOrchestrator.Common.Exceptions;

/// <summary>
/// Exception thrown when a flow execution error occurs.
/// </summary>
public class FlowExecutionException : FlowOrchestratorException
{
    /// <summary>
    /// Gets the flow ID that caused the execution error.
    /// </summary>
    public string? FlowId { get; }
    
    /// <summary>
    /// Gets the step ID that caused the execution error.
    /// </summary>
    public string? StepId { get; }
    
    /// <summary>
    /// Gets the branch path that caused the execution error.
    /// </summary>
    public string? BranchPath { get; }
    
    /// <summary>
    /// Creates a new instance of the FlowExecutionException class.
    /// </summary>
    public FlowExecutionException()
        : base("FLOW-ERR-0003", "A flow execution error occurred.")
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowExecutionException class with the specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public FlowExecutionException(string message)
        : base("FLOW-ERR-0003", message)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowExecutionException class with the specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FlowExecutionException(string message, Exception innerException)
        : base("FLOW-ERR-0003", message, innerException)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the FlowExecutionException class with the specified flow ID, step ID, branch path, and error message.
    /// </summary>
    /// <param name="flowId">The flow ID that caused the execution error.</param>
    /// <param name="stepId">The step ID that caused the execution error.</param>
    /// <param name="branchPath">The branch path that caused the execution error.</param>
    /// <param name="message">The error message.</param>
    public FlowExecutionException(string flowId, string stepId, string branchPath, string message)
        : base("FLOW-ERR-0003", message)
    {
        FlowId = flowId;
        StepId = stepId;
        BranchPath = branchPath;
    }
    
    /// <summary>
    /// Creates a new instance of the FlowExecutionException class with the specified flow ID, step ID, branch path, error message, and inner exception.
    /// </summary>
    /// <param name="flowId">The flow ID that caused the execution error.</param>
    /// <param name="stepId">The step ID that caused the execution error.</param>
    /// <param name="branchPath">The branch path that caused the execution error.</param>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FlowExecutionException(string flowId, string stepId, string branchPath, string message, Exception innerException)
        : base("FLOW-ERR-0003", message, innerException)
    {
        FlowId = flowId;
        StepId = stepId;
        BranchPath = branchPath;
    }
}
