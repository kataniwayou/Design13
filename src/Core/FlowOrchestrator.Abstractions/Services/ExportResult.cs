namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the result of an export operation.
/// </summary>
public class ExportResult
{
    /// <summary>
    /// Gets or sets whether the export was successful.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the export failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the destination identifier where the data was exported.
    /// </summary>
    public string? DestinationIdentifier { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records exported.
    /// </summary>
    public int RecordsExported { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records skipped.
    /// </summary>
    public int RecordsSkipped { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records that failed to export.
    /// </summary>
    public int RecordsFailed { get; set; }
    
    /// <summary>
    /// Gets or sets the data format of the exported data.
    /// </summary>
    public string? DataFormat { get; set; }
    
    /// <summary>
    /// Gets or sets the metadata associated with the exported data.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the timestamp when the export started.
    /// </summary>
    public DateTime StartTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the export completed.
    /// </summary>
    public DateTime EndTimestamp { get; set; }
    
    /// <summary>
    /// Gets the duration of the export operation.
    /// </summary>
    public TimeSpan Duration => EndTimestamp - StartTimestamp;
    
    /// <summary>
    /// Gets or sets the confirmation ID from the destination system.
    /// </summary>
    public string? ConfirmationId { get; set; }
}
