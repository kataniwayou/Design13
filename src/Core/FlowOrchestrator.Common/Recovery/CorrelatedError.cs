using FlowOrchestrator.Common.Errors;

namespace FlowOrchestrator.Common.Recovery;

/// <summary>
/// Represents a group of correlated errors.
/// </summary>
public class CorrelatedError
{
    /// <summary>
    /// Gets or sets the type of correlation.
    /// </summary>
    public CorrelationType CorrelationType { get; set; }
    
    /// <summary>
    /// Gets or sets the strength of the correlation (0.0 to 1.0).
    /// </summary>
    public float CorrelationStrength { get; set; }
    
    /// <summary>
    /// Gets or sets the related errors.
    /// </summary>
    public IEnumerable<ErrorContext> RelatedErrors { get; set; } = Enumerable.Empty<ErrorContext>();
}

/// <summary>
/// Represents the type of correlation between errors.
/// </summary>
public enum CorrelationType
{
    /// <summary>
    /// Errors with the same error code.
    /// </summary>
    SameErrorCode,
    
    /// <summary>
    /// Errors from the same component.
    /// </summary>
    SameComponent,
    
    /// <summary>
    /// Errors of the same type.
    /// </summary>
    SameErrorType,
    
    /// <summary>
    /// Errors that occurred within a short time window.
    /// </summary>
    TimeProximity
}
