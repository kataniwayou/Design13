using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the result of a registration operation.
/// </summary>
public class RegistrationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the registration was successful.
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Gets or sets the registered entity ID.
    /// </summary>
    public string? EntityId { get; set; }
    
    /// <summary>
    /// Gets or sets the validation result.
    /// </summary>
    public ValidationResult? ValidationResult { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the registration failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the exception if the registration failed.
    /// </summary>
    public Exception? Exception { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp of the registration.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets additional details about the registration.
    /// </summary>
    public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Creates a successful registration result.
    /// </summary>
    /// <param name="entityId">The registered entity ID.</param>
    /// <param name="validationResult">Optional validation result.</param>
    /// <param name="details">Optional registration details.</param>
    /// <returns>A successful registration result.</returns>
    public static RegistrationResult Success(string entityId, ValidationResult? validationResult = null, Dictionary<string, string>? details = null)
    {
        return new RegistrationResult
        {
            IsSuccess = true,
            EntityId = entityId,
            ValidationResult = validationResult ?? new ValidationResult(),
            Details = details ?? new Dictionary<string, string>()
        };
    }
    
    /// <summary>
    /// Creates a failed registration result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="validationResult">Optional validation result.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <returns>A failed registration result.</returns>
    public static RegistrationResult Failure(string errorMessage, ValidationResult? validationResult = null, Exception? exception = null)
    {
        return new RegistrationResult
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            ValidationResult = validationResult,
            Exception = exception
        };
    }
    
    /// <summary>
    /// Creates a validation failure registration result.
    /// </summary>
    /// <param name="validationResult">The validation result.</param>
    /// <returns>A validation failure registration result.</returns>
    public static RegistrationResult ValidationFailure(ValidationResult validationResult)
    {
        return new RegistrationResult
        {
            IsSuccess = false,
            ValidationResult = validationResult,
            ErrorMessage = "Validation failed"
        };
    }
}
