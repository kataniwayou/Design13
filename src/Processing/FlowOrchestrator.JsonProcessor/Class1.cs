using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.ProcessorBase;
using FlowOrchestrator.TransformationEngine;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace FlowOrchestrator.JsonProcessor;

/// <summary>
/// Processor for JSON data.
/// </summary>
public class JsonProcessor : FlowOrchestrator.ProcessorBase.ProcessorBase
{
    private readonly ITransformationEngine _transformationEngine;

    /// <summary>
    /// Gets the type of this processor.
    /// </summary>
    public override string ProcessorType => "JsonProcessor";

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonProcessor"/> class.
    /// </summary>
    /// <param name="processorId">The unique identifier for this processor.</param>
    /// <param name="name">The name of this processor.</param>
    /// <param name="description">The description of this processor.</param>
    /// <param name="transformationEngine">The transformation engine.</param>
    /// <param name="logger">The logger.</param>
    public JsonProcessor(
        string processorId,
        string name,
        string description,
        ITransformationEngine transformationEngine,
        ILogger<JsonProcessor> logger)
        : base(processorId, name, description, logger)
    {
        _transformationEngine = transformationEngine ?? throw new ArgumentNullException(nameof(transformationEngine));
    }

    /// <inheritdoc />
    public override async Task<ProcessingResult> ProcessAsync(ProcessingContext processingContext, CancellationToken cancellationToken = default)
    {
        if (processingContext == null) throw new ArgumentNullException(nameof(processingContext));

        try
        {
            Status = ProcessorStatus.Processing;

            // Validate the input data
            if (processingContext.ValidateInput)
            {
                var validationResult = ValidateJson(processingContext.InputData, processingContext.InputValidationRules);
                if (!validationResult.IsValid)
                {
                    return ProcessingResult.Failure(
                        processingContext.ProcessingId,
                        "Input validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                        0,
                        1,
                        1);
                }
            }

            // Process the JSON data
            var result = await ProcessJsonAsync(processingContext, cancellationToken);

            // Validate the output data
            if (processingContext.ValidateOutput && result.OutputData != null)
            {
                var validationResult = ValidateJson(result.OutputData, processingContext.OutputValidationRules);
                if (!validationResult.IsValid)
                {
                    return ProcessingResult.Failure(
                        processingContext.ProcessingId,
                        "Output validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                        0,
                        1,
                        1);
                }
            }

            Status = ProcessorStatus.Idle;
            return result;
        }
        catch (Exception ex)
        {
            Status = ProcessorStatus.Error;
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                $"Error processing JSON data: {ex.Message}",
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
            SupportsTransformation = true,
            SupportsMapping = true,
            SupportsEnrichment = true,
            SupportsCache = true,
            SupportsParallelProcessing = true,
            SupportsErrorRecovery = true,
            SupportsSchemaDiscovery = true,
            SupportsStreaming = true,
            SupportsBatching = true,
            MaxBatchSize = 1000,
            MaxParallelProcessingTasks = Environment.ProcessorCount,
            SupportedInputDataTypes = new List<string> { "json", "string" },
            SupportedOutputDataTypes = new List<string> { "json", "string", "object" },
            SupportedValidationRuleTypes = new List<string> { "json-schema" },
            SupportedTransformationRuleTypes = new List<string> { "jq", "jsonata", "custom" },
            SupportedMappingRuleTypes = new List<string> { "json-map" },
            SupportedEnrichmentRuleTypes = new List<string> { "json-enrich" },
            SupportedErrorRecoveryStrategies = new List<string> { "retry", "fallback", "skip" }
        };
    }

    private ValidationResult ValidateJson(object? data, string? schema)
    {
        if (data == null)
        {
            return ValidationResult.Failure(
                new List<ProcessorBase.ValidationError>
                {
                    new ProcessorBase.ValidationError
                    {
                        ErrorCode = "NULL_DATA",
                        ErrorMessage = "Input data is null",
                        Severity = ProcessorBase.ValidationSeverity.Error
                    }
                },
                data,
                "json");
        }

        if (string.IsNullOrWhiteSpace(schema))
        {
            // No schema provided, consider it valid
            return ValidationResult.Success(data, "json");
        }

        try
        {
            // Parse the JSON data
            JToken jsonData;
            if (data is string jsonString)
            {
                jsonData = JToken.Parse(jsonString);
            }
            else if (data is JToken token)
            {
                jsonData = token;
            }
            else
            {
                jsonData = JToken.FromObject(data);
            }

            // Parse the JSON schema
            var jsonSchema = JSchema.Parse(schema);

            // Validate the JSON data against the schema
            var errors = new List<string>();
            var isValid = jsonData.IsValid(jsonSchema, out IList<string> errorMessages);

            if (!isValid)
            {
                var validationErrors = errorMessages.Select(error => new ProcessorBase.ValidationError
                {
                    ErrorCode = "SCHEMA_VALIDATION",
                    ErrorMessage = error,
                    Severity = ProcessorBase.ValidationSeverity.Error
                }).ToList();

                return ValidationResult.Failure(validationErrors, data, "json", schema, "json-schema");
            }

            return ValidationResult.Success(data, "json", schema, "json-schema");
        }
        catch (Exception ex)
        {
            return ValidationResult.Failure(
                new List<ProcessorBase.ValidationError>
                {
                    new ProcessorBase.ValidationError
                    {
                        ErrorCode = "VALIDATION_ERROR",
                        ErrorMessage = $"Error validating JSON: {ex.Message}",
                        Severity = ProcessorBase.ValidationSeverity.Error
                    }
                },
                data,
                "json",
                schema,
                "json-schema");
        }
    }

    private async Task<ProcessingResult> ProcessJsonAsync(ProcessingContext processingContext, CancellationToken cancellationToken)
    {
        // Parse the input data
        JToken jsonData;
        if (processingContext.InputData is string jsonString)
        {
            jsonData = JToken.Parse(jsonString);
        }
        else if (processingContext.InputData is JToken token)
        {
            jsonData = token;
        }
        else
        {
            jsonData = JToken.FromObject(processingContext.InputData);
        }

        // Create a data package for the transformation engine
        var dataPackage = new DataPackage
        {
            PackageId = processingContext.ProcessingId,
            Data = jsonData,
            DataType = "json",
            DataFormat = "json"
        };

        // Apply transformations if specified
        if (!string.IsNullOrWhiteSpace(processingContext.TransformationRules))
        {
            // Parse the transformation rule
            var transformationRule = JsonConvert.DeserializeObject<TransformationRule>(processingContext.TransformationRules);
            if (transformationRule == null)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    "Invalid transformation rules",
                    0,
                    1,
                    1);
            }

            // Apply the transformation
            var transformationResult = await _transformationEngine.TransformAsync(dataPackage, transformationRule, cancellationToken);
            if (!transformationResult.IsSuccessful)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    $"Transformation failed: {transformationResult.ErrorMessage}",
                    0,
                    1,
                    1);
            }

            // Update the data package with the transformed data
            dataPackage.Data = transformationResult.OutputData;
        }

        // Apply mappings if specified
        if (!string.IsNullOrWhiteSpace(processingContext.MappingRules))
        {
            // Parse the mapping definition
            var mappingDefinition = JsonConvert.DeserializeObject<MappingDefinition>(processingContext.MappingRules);
            if (mappingDefinition == null)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    "Invalid mapping rules",
                    0,
                    1,
                    1);
            }

            // Apply the mapping
            var mappingResult = await _transformationEngine.ApplyMappingAsync(dataPackage, mappingDefinition, cancellationToken);
            if (!mappingResult.IsSuccessful)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    $"Mapping failed: {mappingResult.ErrorMessage}",
                    0,
                    1,
                    1);
            }

            // Update the data package with the mapped data
            dataPackage.Data = mappingResult.OutputData;
        }

        // Apply enrichment if specified
        if (!string.IsNullOrWhiteSpace(processingContext.EnrichmentRules))
        {
            // For demonstration purposes, we'll just add a timestamp to the JSON data
            if (dataPackage.Data is JObject jsonObject)
            {
                jsonObject["enriched_timestamp"] = DateTime.UtcNow;
                jsonObject["enrichment_source"] = "JsonProcessor";
            }
        }

        // Return the processed data
        return ProcessingResult.Success(
            processingContext.ProcessingId,
            dataPackage.Data,
            processingContext.OutputDataType ?? "json",
            1,
            1);
    }
}
