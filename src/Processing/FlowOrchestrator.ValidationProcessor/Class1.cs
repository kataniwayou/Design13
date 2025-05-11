using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.ProcessorBase;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Processor for data validation.
/// </summary>
public class ValidationProcessor : FlowOrchestrator.ProcessorBase.ProcessorBase
{
    private readonly IValidatorFactory _validatorFactory;

    /// <summary>
    /// Gets the type of this processor.
    /// </summary>
    public override string ProcessorType => "ValidationProcessor";

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationProcessor"/> class.
    /// </summary>
    /// <param name="processorId">The unique identifier for this processor.</param>
    /// <param name="name">The name of this processor.</param>
    /// <param name="description">The description of this processor.</param>
    /// <param name="validatorFactory">The validator factory.</param>
    /// <param name="logger">The logger.</param>
    public ValidationProcessor(
        string processorId,
        string name,
        string description,
        IValidatorFactory validatorFactory,
        ILogger<ValidationProcessor> logger)
        : base(processorId, name, description, logger)
    {
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }

    /// <inheritdoc />
    public override async Task<ProcessingResult> ProcessAsync(ProcessingContext processingContext, CancellationToken cancellationToken = default)
    {
        if (processingContext == null) throw new ArgumentNullException(nameof(processingContext));

        try
        {
            Status = ProcessorStatus.Processing;

            // Validate the input data
            var validationResult = await ValidateDataAsync(processingContext, cancellationToken);

            Status = ProcessorStatus.Idle;
            return validationResult;
        }
        catch (Exception ex)
        {
            Status = ProcessorStatus.Error;
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                $"Error validating data: {ex.Message}",
                0,
                1,
                1);
        }
    }

    /// <inheritdoc />
    public override ProcessorCapabilities GetCapabilities()
    {
        return new ProcessorCapabilities
        {
            SupportsValidation = true,
            SupportsTransformation = false,
            SupportsMapping = false,
            SupportsEnrichment = false,
            SupportsCache = true,
            SupportsParallelProcessing = true,
            SupportsErrorRecovery = true,
            SupportsSchemaDiscovery = false,
            SupportsStreaming = false,
            SupportsBatching = true,
            MaxBatchSize = 1000,
            MaxParallelProcessingTasks = Environment.ProcessorCount,
            SupportedInputDataTypes = new List<string> { "json", "object", "string" },
            SupportedOutputDataTypes = new List<string> { "validation-result" },
            SupportedValidationRuleTypes = new List<string> { "fluent-validation", "custom" },
            SupportedErrorRecoveryStrategies = new List<string> { "retry", "skip" }
        };
    }

    private async Task<ProcessingResult> ValidateDataAsync(ProcessingContext processingContext, CancellationToken cancellationToken)
    {
        // Parse the validation rules
        var validationRules = processingContext.InputValidationRules;
        if (string.IsNullOrWhiteSpace(validationRules))
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "No validation rules specified",
                0,
                1,
                1);
        }

        // Get the validator for the input data type
        var validatorType = GetValidatorType(processingContext.InputDataType);
        if (validatorType == null)
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                $"No validator found for input data type: {processingContext.InputDataType}",
                0,
                1,
                1);
        }

        // Get the validator
        var validator = _validatorFactory.GetValidator(validatorType);
        if (validator == null)
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                $"Failed to create validator for type: {validatorType.Name}",
                0,
                1,
                1);
        }

        // Convert the input data to the expected type
        var inputData = ConvertInputData(processingContext.InputData, validatorType);
        if (inputData == null)
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "Failed to convert input data to the expected type",
                0,
                1,
                1);
        }

        // Validate the input data
        var result = await validator.ValidateAsync(
            new ValidationContext<object>(inputData)
            {
                RootContextData = { ["ValidationRules"] = validationRules }
            },
            cancellationToken);

        // Create the validation result
        var validationErrors = result.Errors.Select(error => new ValidationError
        {
            ErrorCode = error.ErrorCode,
            ErrorMessage = error.ErrorMessage,
            PropertyName = error.PropertyName,
            PropertyValue = error.AttemptedValue,
            Severity = (ProcessorBase.ValidationSeverity)ConvertSeverity(error.Severity)
        }).ToList();

        var processorValidationResult = result.IsValid
            ? ValidationResult.Success(inputData, processingContext.InputDataType, validationRules, "fluent-validation")
            : ValidationResult.Failure(validationErrors, inputData, processingContext.InputDataType, validationRules, "fluent-validation");

        // Create the processing result
        var processingResult = new ProcessingResult
        {
            ProcessingId = processingContext.ProcessingId,
            IsSuccessful = result.IsValid,
            ErrorMessage = result.IsValid ? null : "Validation failed",
            OutputData = processorValidationResult,
            OutputDataType = "validation-result",
            StartTime = DateTime.UtcNow.AddMilliseconds(-100), // Simulate a 100ms processing time
            EndTime = DateTime.UtcNow,
            DurationMs = 100,
            RecordsProcessed = 1,
            TotalRecords = 1,
            InputValidationResult = processorValidationResult
        };

        return processingResult;
    }

    private Type? GetValidatorType(string? inputDataType)
    {
        // This is a simplified implementation. In a real system, this would look up the validator type
        // based on the input data type.

        // For demonstration purposes, we'll just return a generic object validator
        return typeof(IValidator<object>);
    }

    private object? ConvertInputData(object? inputData, Type validatorType)
    {
        // This is a simplified implementation. In a real system, this would convert the input data
        // to the expected type for the validator.

        if (inputData == null)
        {
            return null;
        }

        // If the input data is already the expected type, return it
        if (validatorType.IsInstanceOfType(inputData))
        {
            return inputData;
        }

        // If the input data is a string, try to deserialize it
        if (inputData is string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<object>(jsonString);
            }
            catch
            {
                return jsonString;
            }
        }

        // Otherwise, just return the input data
        return inputData;
    }

    private ValidationSeverity ConvertSeverity(FluentValidation.Severity severity)
    {
        return severity switch
        {
            FluentValidation.Severity.Info => ValidationSeverity.Info,
            FluentValidation.Severity.Warning => ValidationSeverity.Warning,
            FluentValidation.Severity.Error => ValidationSeverity.Error,
            _ => ValidationSeverity.Error
        };
    }
}
