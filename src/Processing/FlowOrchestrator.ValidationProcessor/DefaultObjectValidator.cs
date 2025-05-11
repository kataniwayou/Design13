using FluentValidation;
using FluentValidation.Results;
using System.Text.Json;

namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Default validator for objects.
/// </summary>
public class DefaultObjectValidator : AbstractValidator<object>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultObjectValidator"/> class.
    /// </summary>
    public DefaultObjectValidator()
    {
        // No default rules
    }
    
    /// <inheritdoc />
    public override ValidationResult Validate(ValidationContext<object> context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        // Get the validation rules from the context
        if (!context.RootContextData.TryGetValue("ValidationRules", out var rulesObj) || rulesObj is not string rules)
        {
            return new ValidationResult();
        }
        
        // Parse the validation rules
        var validationRules = ParseValidationRules(rules);
        if (validationRules == null || validationRules.Count == 0)
        {
            return new ValidationResult();
        }
        
        // Apply the validation rules
        var errors = new List<ValidationFailure>();
        
        foreach (var rule in validationRules)
        {
            // Apply the rule to the instance
            var ruleErrors = ApplyRule(context.InstanceToValidate, rule);
            errors.AddRange(ruleErrors);
        }
        
        return new ValidationResult(errors);
    }
    
    private List<ValidationRule> ParseValidationRules(string rules)
    {
        try
        {
            return JsonSerializer.Deserialize<List<ValidationRule>>(rules) ?? new List<ValidationRule>();
        }
        catch
        {
            return new List<ValidationRule>();
        }
    }
    
    private List<ValidationFailure> ApplyRule(object instance, ValidationRule rule)
    {
        var errors = new List<ValidationFailure>();
        
        // This is a simplified implementation. In a real system, this would apply the rule
        // to the instance and return any validation failures.
        
        // For demonstration purposes, we'll just check if the property exists and has a value
        if (instance is JsonElement jsonElement)
        {
            if (rule.Required && !jsonElement.TryGetProperty(rule.PropertyName, out _))
            {
                errors.Add(new ValidationFailure(rule.PropertyName, $"Property '{rule.PropertyName}' is required")
                {
                    ErrorCode = "REQUIRED",
                    Severity = ConvertSeverity(rule.Severity)
                });
            }
        }
        else if (instance is System.Collections.IDictionary dictionary)
        {
            if (rule.Required && !dictionary.Contains(rule.PropertyName))
            {
                errors.Add(new ValidationFailure(rule.PropertyName, $"Property '{rule.PropertyName}' is required")
                {
                    ErrorCode = "REQUIRED",
                    Severity = ConvertSeverity(rule.Severity)
                });
            }
        }
        else
        {
            var property = instance.GetType().GetProperty(rule.PropertyName);
            if (rule.Required && (property == null || property.GetValue(instance) == null))
            {
                errors.Add(new ValidationFailure(rule.PropertyName, $"Property '{rule.PropertyName}' is required")
                {
                    ErrorCode = "REQUIRED",
                    Severity = ConvertSeverity(rule.Severity)
                });
            }
        }
        
        return errors;
    }
    
    private FluentValidation.Severity ConvertSeverity(ValidationSeverity severity)
    {
        return severity switch
        {
            ValidationSeverity.Info => FluentValidation.Severity.Info,
            ValidationSeverity.Warning => FluentValidation.Severity.Warning,
            ValidationSeverity.Error => FluentValidation.Severity.Error,
            ValidationSeverity.Critical => FluentValidation.Severity.Error,
            _ => FluentValidation.Severity.Error
        };
    }
}
