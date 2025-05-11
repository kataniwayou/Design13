using FlowOrchestrator.Abstractions.Entities;
using System.Collections.Generic;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Gets or sets the rule ID that was validated.
    /// </summary>
    public string RuleId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rule name that was validated.
    /// </summary>
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the validation passed.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the severity of the validation result.
    /// </summary>
    public ValidationSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the error message if the validation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the path to the data element that failed validation.
    /// </summary>
    public string? DataPath { get; set; }

    /// <summary>
    /// Gets or sets the value of the data element that failed validation.
    /// </summary>
    public string? DataValue { get; set; }

    /// <summary>
    /// Gets or sets the expected value or pattern for the data element.
    /// </summary>
    public string? ExpectedValue { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the validation was performed.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the collection of error messages.
    /// </summary>
    public List<string> Errors { get; } = new List<string>();
}
