namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of an error analysis operation.
/// </summary>
public class ErrorAnalysisResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the analysis was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the analysis failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation error that was analyzed.
    /// </summary>
    public TransformationError? Error { get; set; }
    
    /// <summary>
    /// Gets or sets the error category.
    /// </summary>
    public string ErrorCategory { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error cause.
    /// </summary>
    public string ErrorCause { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error impact.
    /// </summary>
    public string ErrorImpact { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error frequency.
    /// </summary>
    public int ErrorFrequency { get; set; }
    
    /// <summary>
    /// Gets or sets the recommended recovery strategies.
    /// </summary>
    public List<RecoveryStrategy> RecommendedRecoveryStrategies { get; set; } = new List<RecoveryStrategy>();
    
    /// <summary>
    /// Gets or sets the error prevention recommendations.
    /// </summary>
    public List<string> ErrorPreventionRecommendations { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the analysis timestamp.
    /// </summary>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the analysis.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful analysis result.
    /// </summary>
    /// <param name="error">The transformation error that was analyzed.</param>
    /// <param name="errorCategory">The error category.</param>
    /// <param name="errorCause">The error cause.</param>
    /// <param name="errorImpact">The error impact.</param>
    /// <param name="errorFrequency">The error frequency.</param>
    /// <param name="recommendedRecoveryStrategies">The recommended recovery strategies.</param>
    /// <param name="errorPreventionRecommendations">The error prevention recommendations.</param>
    /// <returns>A successful analysis result.</returns>
    public static ErrorAnalysisResult Success(
        TransformationError error,
        string errorCategory,
        string errorCause,
        string errorImpact,
        int errorFrequency,
        List<RecoveryStrategy> recommendedRecoveryStrategies,
        List<string> errorPreventionRecommendations)
    {
        return new ErrorAnalysisResult
        {
            IsSuccessful = true,
            Error = error,
            ErrorCategory = errorCategory,
            ErrorCause = errorCause,
            ErrorImpact = errorImpact,
            ErrorFrequency = errorFrequency,
            RecommendedRecoveryStrategies = recommendedRecoveryStrategies,
            ErrorPreventionRecommendations = errorPreventionRecommendations
        };
    }
    
    /// <summary>
    /// Creates a failed analysis result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="error">The transformation error that was analyzed.</param>
    /// <returns>A failed analysis result.</returns>
    public static ErrorAnalysisResult Failure(string errorMessage, TransformationError error)
    {
        return new ErrorAnalysisResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Error = error
        };
    }
}
