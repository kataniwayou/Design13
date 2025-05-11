namespace FlowOrchestrator.ImporterBase;

/// <summary>
/// Represents the result of an import operation.
/// </summary>
public class ImportResult
{
    /// <summary>
    /// Gets or sets the unique identifier for this import operation.
    /// </summary>
    public string ImportId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether the import operation was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the import operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records imported.
    /// </summary>
    public int RecordsImported { get; set; }
    
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
    /// Gets or sets the start time of the import operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the import operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the import operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the resume token for resuming the import operation.
    /// </summary>
    public string? ResumeToken { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether there are more records to import.
    /// </summary>
    public bool HasMoreRecords { get; set; }
    
    /// <summary>
    /// Gets or sets the imported data.
    /// </summary>
    public object? Data { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the import operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the import operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful import result.
    /// </summary>
    /// <param name="importId">The import ID.</param>
    /// <param name="recordsImported">The number of records imported.</param>
    /// <param name="totalRecords">The total number of records.</param>
    /// <param name="data">The imported data.</param>
    /// <returns>A successful import result.</returns>
    public static ImportResult Success(string importId, int recordsImported, int totalRecords, object? data = null)
    {
        var startTime = DateTime.UtcNow.AddSeconds(-1); // Simulate a 1-second import
        var endTime = DateTime.UtcNow;
        
        return new ImportResult
        {
            ImportId = importId,
            IsSuccessful = true,
            RecordsImported = recordsImported,
            TotalRecords = totalRecords,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds,
            Data = data,
            HasMoreRecords = recordsImported < totalRecords
        };
    }
    
    /// <summary>
    /// Creates a failed import result.
    /// </summary>
    /// <param name="importId">The import ID.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="recordsImported">The number of records imported before the failure.</param>
    /// <param name="recordsFailed">The number of records that failed to import.</param>
    /// <param name="totalRecords">The total number of records.</param>
    /// <returns>A failed import result.</returns>
    public static ImportResult Failure(string importId, string errorMessage, int recordsImported = 0, int recordsFailed = 0, int totalRecords = 0)
    {
        var startTime = DateTime.UtcNow.AddSeconds(-1); // Simulate a 1-second import
        var endTime = DateTime.UtcNow;
        
        return new ImportResult
        {
            ImportId = importId,
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            RecordsImported = recordsImported,
            RecordsFailed = recordsFailed,
            TotalRecords = totalRecords,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds,
            HasMoreRecords = recordsImported + recordsFailed < totalRecords
        };
    }
}
