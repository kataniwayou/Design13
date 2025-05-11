namespace FlowOrchestrator.ImporterBase;

/// <summary>
/// Represents the status of an importer.
/// </summary>
public enum ImporterStatus
{
    /// <summary>
    /// The importer has been created but not yet initialized.
    /// </summary>
    Created,
    
    /// <summary>
    /// The importer has been initialized with a configuration.
    /// </summary>
    Initialized,
    
    /// <summary>
    /// The importer is open and ready to import data.
    /// </summary>
    Open,
    
    /// <summary>
    /// The importer is closed.
    /// </summary>
    Closed,
    
    /// <summary>
    /// The importer is in an error state.
    /// </summary>
    Error,
    
    /// <summary>
    /// The importer is currently importing data.
    /// </summary>
    Importing
}
