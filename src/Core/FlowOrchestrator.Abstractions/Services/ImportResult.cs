namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the result of an import operation.
/// </summary>
public class ImportResult
{
    /// <summary>
    /// Gets or sets whether the import was successful.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the import failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the memory address where the imported data is stored.
    /// </summary>
    public string? MemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records imported.
    /// </summary>
    public int RecordsImported { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records skipped.
    /// </summary>
    public int RecordsSkipped { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records that failed to import.
    /// </summary>
    public int RecordsFailed { get; set; }
    
    /// <summary>
    /// Gets or sets the data format of the imported data.
    /// </summary>
    public string? DataFormat { get; set; }
    
    /// <summary>
    /// Gets or sets the schema of the imported data.
    /// </summary>
    public string? Schema { get; set; }
    
    /// <summary>
    /// Gets or sets the metadata associated with the imported data.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the timestamp when the import started.
    /// </summary>
    public DateTime StartTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the import completed.
    /// </summary>
    public DateTime EndTimestamp { get; set; }
    
    /// <summary>
    /// Gets the duration of the import operation.
    /// </summary>
    public TimeSpan Duration => EndTimestamp - StartTimestamp;
}
