using System.Collections.Generic;
using System.Linq;

namespace FlowOrchestrator.Common;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public class ValidationResult
{
    private readonly List<string> _errors = new List<string>();
    private readonly List<string> _warnings = new List<string>();

    /// <summary>
    /// Gets a value indicating whether the validation passed (no errors).
    /// </summary>
    public bool IsValid => !_errors.Any();

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();

    /// <summary>
    /// Gets the validation warnings.
    /// </summary>
    public IReadOnlyList<string> Warnings => _warnings.AsReadOnly();

    /// <summary>
    /// Adds an error to the validation result.
    /// </summary>
    /// <param name="error">The error message.</param>
    public void AddError(string error)
    {
        if (!string.IsNullOrEmpty(error))
        {
            _errors.Add(error);
        }
    }

    /// <summary>
    /// Adds a warning to the validation result.
    /// </summary>
    /// <param name="warning">The warning message.</param>
    public void AddWarning(string warning)
    {
        if (!string.IsNullOrEmpty(warning))
        {
            _warnings.Add(warning);
        }
    }

    /// <summary>
    /// Adds all errors and warnings from another validation result.
    /// </summary>
    /// <param name="other">The other validation result.</param>
    public void Merge(ValidationResult other)
    {
        if (other == null)
        {
            return;
        }

        foreach (var error in other.Errors)
        {
            AddError(error);
        }

        foreach (var warning in other.Warnings)
        {
            AddWarning(warning);
        }
    }

    /// <summary>
    /// Returns a string representation of the validation result.
    /// </summary>
    /// <returns>A string representation of the validation result.</returns>
    public override string ToString()
    {
        var result = IsValid ? "Valid" : "Invalid";

        if (_errors.Any())
        {
            result += $" (Errors: {_errors.Count})";
        }

        if (_warnings.Any())
        {
            result += $" (Warnings: {_warnings.Count})";
        }

        return result;
    }
}
