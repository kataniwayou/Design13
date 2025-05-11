namespace FlowOrchestrator.ExporterBase;

/// <summary>
/// Represents the status of an exporter.
/// </summary>
public enum ExporterStatus
{
    /// <summary>
    /// The exporter has been created but not yet initialized.
    /// </summary>
    Created,
    
    /// <summary>
    /// The exporter has been initialized with a configuration.
    /// </summary>
    Initialized,
    
    /// <summary>
    /// The exporter is open and ready to export data.
    /// </summary>
    Open,
    
    /// <summary>
    /// The exporter is closed.
    /// </summary>
    Closed,
    
    /// <summary>
    /// The exporter is in an error state.
    /// </summary>
    Error,
    
    /// <summary>
    /// The exporter is currently exporting data.
    /// </summary>
    Exporting
}
