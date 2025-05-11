namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the status of a processor.
/// </summary>
public enum ProcessorStatus
{
    /// <summary>
    /// The processor has been created but not yet initialized.
    /// </summary>
    Created,
    
    /// <summary>
    /// The processor has been initialized with a configuration.
    /// </summary>
    Initialized,
    
    /// <summary>
    /// The processor is currently processing data.
    /// </summary>
    Processing,
    
    /// <summary>
    /// The processor is in an error state.
    /// </summary>
    Error,
    
    /// <summary>
    /// The processor is idle and ready to process data.
    /// </summary>
    Idle,
    
    /// <summary>
    /// The processor has been paused.
    /// </summary>
    Paused,
    
    /// <summary>
    /// The processor has been stopped.
    /// </summary>
    Stopped
}
