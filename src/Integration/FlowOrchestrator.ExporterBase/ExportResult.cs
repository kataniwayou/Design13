namespace FlowOrchestrator.ExporterBase;

/// <summary>
/// Represents the result of an export operation.
/// </summary>
public class ExportResult
{
    /// <summary>
    /// Gets or sets the unique identifier for this export operation.
    /// </summary>
    public string ExportId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether the export operation was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the export operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records exported.
    /// </summary>
    public int RecordsExported { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records skipped.
    /// </summary>
    public int RecordsSkipped { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records failed.
    /// </summary>
    public int RecordsFailed { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of records.
    /// </summary>
    public int TotalRecords { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the export operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the export operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the export operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the resume token for resuming the export operation.
    /// </summary>
    public string? ResumeToken { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether there are more records to export.
    /// </summary>
    public bool HasMoreRecords { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the export operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the export operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful export result.
    /// </summary>
    /// <param name="exportId">The export ID.</param>
    /// <param name="recordsExported">The number of records exported.</param>
    /// <param name="totalRecords">The total number of records.</param>
    /// <returns>A successful export result.</returns>
    public static ExportResult Success(string exportId, int recordsExported, int totalRecords)
    {
        var startTime = DateTime.UtcNow.AddSeconds(-1); // Simulate a 1-second export
        var endTime = DateTime.UtcNow;
        
        return new ExportResult
        {
            ExportId = exportId,
            IsSuccessful = true,
            RecordsExported = recordsExported,
            TotalRecords = totalRecords,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds,
            HasMoreRecords = recordsExported < totalRecords
        };
    }
    
    /// <summary>
    /// Creates a failed export result.
    /// </summary>
    /// <param name="exportId">The export ID.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="recordsExported">The number of records exported before the failure.</param>
    /// <param name="recordsFailed">The number of records that failed to export.</param>
    /// <param name="totalRecords">The total number of records.</param>
    /// <returns>A failed export result.</returns>
    public static ExportResult Failure(string exportId, string errorMessage, int recordsExported = 0, int recordsFailed = 0, int totalRecords = 0)
    {
        var startTime = DateTime.UtcNow.AddSeconds(-1); // Simulate a 1-second export
        var endTime = DateTime.UtcNow;
        
        return new ExportResult
        {
            ExportId = exportId,
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            RecordsExported = recordsExported,
            RecordsFailed = recordsFailed,
            TotalRecords = totalRecords,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds,
            HasMoreRecords = recordsExported + recordsFailed < totalRecords
        };
    }
}
