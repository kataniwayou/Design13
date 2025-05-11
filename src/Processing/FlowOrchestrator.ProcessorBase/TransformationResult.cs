namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the result of a transformation operation.
/// </summary>
public class TransformationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the transformation was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the transformation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the input data that was transformed.
    /// </summary>
    public object? InputData { get; set; }
    
    /// <summary>
    /// Gets or sets the input data type that was transformed.
    /// </summary>
    public string? InputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the output data from the transformation.
    /// </summary>
    public object? OutputData { get; set; }
    
    /// <summary>
    /// Gets or sets the output data type from the transformation.
    /// </summary>
    public string? OutputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation rule that was applied.
    /// </summary>
    public string? TransformationRule { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation rule type that was applied.
    /// </summary>
    public string? TransformationRuleType { get; set; }
    
    /// <summary>
    /// Gets or sets the start time of the transformation operation.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time of the transformation operation.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the duration of the transformation operation in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the warnings that occurred during the transformation operation.
    /// </summary>
    public List<string> Warnings { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the additional information about the transformation operation.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful transformation result.
    /// </summary>
    /// <param name="inputData">The input data that was transformed.</param>
    /// <param name="inputDataType">The input data type that was transformed.</param>
    /// <param name="outputData">The output data from the transformation.</param>
    /// <param name="outputDataType">The output data type from the transformation.</param>
    /// <param name="transformationRule">The transformation rule that was applied.</param>
    /// <param name="transformationRuleType">The transformation rule type that was applied.</param>
    /// <returns>A successful transformation result.</returns>
    public static TransformationResult Success(object? inputData, string? inputDataType, object? outputData, string? outputDataType, string? transformationRule = null, string? transformationRuleType = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-200); // Simulate a 200ms transformation
        var endTime = DateTime.UtcNow;
        
        return new TransformationResult
        {
            IsSuccessful = true,
            InputData = inputData,
            InputDataType = inputDataType,
            OutputData = outputData,
            OutputDataType = outputDataType,
            TransformationRule = transformationRule,
            TransformationRuleType = transformationRuleType,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
    
    /// <summary>
    /// Creates a failed transformation result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="inputData">The input data that was transformed.</param>
    /// <param name="inputDataType">The input data type that was transformed.</param>
    /// <param name="transformationRule">The transformation rule that was applied.</param>
    /// <param name="transformationRuleType">The transformation rule type that was applied.</param>
    /// <returns>A failed transformation result.</returns>
    public static TransformationResult Failure(string errorMessage, object? inputData, string? inputDataType, string? transformationRule = null, string? transformationRuleType = null)
    {
        var startTime = DateTime.UtcNow.AddMilliseconds(-200); // Simulate a 200ms transformation
        var endTime = DateTime.UtcNow;
        
        return new TransformationResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            InputData = inputData,
            InputDataType = inputDataType,
            TransformationRule = transformationRule,
            TransformationRuleType = transformationRuleType,
            StartTime = startTime,
            EndTime = endTime,
            DurationMs = (long)(endTime - startTime).TotalMilliseconds
        };
    }
}
