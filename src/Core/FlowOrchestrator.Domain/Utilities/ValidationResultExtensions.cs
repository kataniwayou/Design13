using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Domain.Utilities;

/// <summary>
/// Extension methods for ValidationResult classes.
/// </summary>
public static class ValidationResultExtensions
{
    /// <summary>
    /// Converts a Common.ValidationResult to an Abstractions.Services.ValidationResult.
    /// </summary>
    /// <param name="commonResult">The Common.ValidationResult to convert.</param>
    /// <returns>The converted Abstractions.Services.ValidationResult.</returns>
    public static Abstractions.Services.ValidationResult ToAbstractionsValidationResult(this Common.ValidationResult commonResult)
    {
        if (commonResult == null)
        {
            return new Abstractions.Services.ValidationResult();
        }

        var result = new Abstractions.Services.ValidationResult
        {
            IsValid = commonResult.IsValid
        };

        foreach (var error in commonResult.Errors)
        {
            result.Errors.Add(error);
        }

        return result;
    }

    /// <summary>
    /// Adds an error to the validation result.
    /// </summary>
    /// <param name="result">The validation result.</param>
    /// <param name="error">The error message.</param>
    public static void AddError(this Abstractions.Services.ValidationResult result, string error)
    {
        if (result == null || string.IsNullOrEmpty(error))
        {
            return;
        }

        result.Errors.Add(error);
        result.IsValid = false;
    }
}
