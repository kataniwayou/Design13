namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the result of a processing operation.
/// </summary>
public class ProcessingResult
{
    /// <summary>
    /// Gets or sets whether the processing was successful.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the processing failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the memory address where the processed data is stored.
    /// </summary>
    public string? OutputMemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records processed.
    /// </summary>
    public int RecordsProcessed { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records skipped.
    /// </summary>
    public int RecordsSkipped { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records that failed to process.
    /// </summary>
    public int RecordsFailed { get; set; }
    
    /// <summary>
    /// Gets or sets the data format of the processed data.
    /// </summary>
    public string? DataFormat { get; set; }
    
    /// <summary>
    /// Gets or sets the schema of the processed data.
    /// </summary>
    public string? Schema { get; set; }
    
    /// <summary>
    /// Gets or sets the metadata associated with the processed data.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the timestamp when the processing started.
    /// </summary>
    public DateTime StartTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the processing completed.
    /// </summary>
    public DateTime EndTimestamp { get; set; }
    
    /// <summary>
    /// Gets the duration of the processing operation.
    /// </summary>
    public TimeSpan Duration => EndTimestamp - StartTimestamp;
    
    /// <summary>
    /// Gets or sets the validation results if validation was performed.
    /// </summary>
    public List<ValidationResult> ValidationResults { get; set; } = new List<ValidationResult>();
}
