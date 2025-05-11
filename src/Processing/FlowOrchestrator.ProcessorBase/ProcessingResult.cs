namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the result of a processing operation.
/// </summary>
public class ProcessingResult
{
    /// <summary>
    /// Gets or sets the unique identifier for this processing operation.
    /// </summary>
    public string ProcessingId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether the processing operation was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the processing operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the output data from the processing operation.
    /// </summary>
    public object? OutputData { get; set; }
    
    /// <summary>
    /// Gets or sets the output data type from the processing operation.
    /// </summary>
    public string? OutputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the processing operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the processing operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the processing operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records processed.
    /// </summary>
    public int RecordsProcessed { get; set; }
    
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
    /// Gets or sets the validation results for the input data.
    /// </summary>
    public ValidationResult? InputValidationResult { get; set; }
    
    /// <summary>
    /// Gets or sets the validation results for the output data.
    /// </summary>
    public ValidationResult? OutputValidationResult { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation results.
    /// </summary>
    public TransformationResult? TransformationResult { get; set; }
    
    /// <summary>
    /// Gets or sets the mapping results.
    /// </summary>
    public MappingResult? MappingResult { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment results.
    /// </summary>
    public EnrichmentResult? EnrichmentResult { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the processing operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the processing operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful processing result.
    /// </summary>
    /// <param name="processingId">The processing ID.</param>
    /// <param name="outputData">The output data.</param>
    /// <param name="outputDataType">The output data type.</param>
    /// <param name="recordsProcessed">The number of records processed.</param>
    /// <param name="totalRecords">The total number of records.</param>
    /// <returns>A successful processing result.</returns>
    public static ProcessingResult Success(string processingId, object? outputData, string? outputDataType, int recordsProcessed = 1, int totalRecords = 1)
    {
        var startTime = DateTime.UtcNow.AddSeconds(-1); // Simulate a 1-second processing
        var endTime = DateTime.UtcNow;
        
        return new ProcessingResult
        {
            ProcessingId = processingId,
            IsSuccessful = true,
            OutputData = outputData,
            OutputDataType = outputDataType,
            RecordsProcessed = recordsProcessed,
            TotalRecords = totalRecords,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
    
    /// <summary>
    /// Creates a failed processing result.
    /// </summary>
    /// <param name="processingId">The processing ID.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="recordsProcessed">The number of records processed before the failure.</param>
    /// <param name="recordsFailed">The number of records that failed to process.</param>
    /// <param name="totalRecords">The total number of records.</param>
    /// <returns>A failed processing result.</returns>
    public static ProcessingResult Failure(string processingId, string errorMessage, int recordsProcessed = 0, int recordsFailed = 0, int totalRecords = 0)
    {
        var startTime = DateTime.UtcNow.AddSeconds(-1); // Simulate a 1-second processing
        var endTime = DateTime.UtcNow;
        
        return new ProcessingResult
        {
            ProcessingId = processingId,
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            RecordsProcessed = recordsProcessed,
            RecordsFailed = recordsFailed,
            TotalRecords = totalRecords,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
