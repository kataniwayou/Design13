using AutoMapper;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.ProcessorBase;
using FlowOrchestrator.TransformationEngine;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlowOrchestrator.MappingProcessor;

/// <summary>
/// Processor for data mapping.
/// </summary>
public class MappingProcessor : FlowOrchestrator.ProcessorBase.ProcessorBase
{
    private readonly ITransformationEngine _transformationEngine;
    private readonly IMapper _mapper;

    /// <summary>
    /// Gets the type of this processor.
    /// </summary>
    public override string ProcessorType => "MappingProcessor";

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProcessor"/> class.
    /// </summary>
    /// <param name="processorId">The unique identifier for this processor.</param>
    /// <param name="name">The name of this processor.</param>
    /// <param name="description">The description of this processor.</param>
    /// <param name="transformationEngine">The transformation engine.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public MappingProcessor(
        string processorId,
        string name,
        string description,
        ITransformationEngine transformationEngine,
        IMapper mapper,
        ILogger<MappingProcessor> logger)
        : base(processorId, name, description, logger)
    {
        _transformationEngine = transformationEngine ?? throw new ArgumentNullException(nameof(transformationEngine));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc />
    public override async Task<ProcessingResult> ProcessAsync(ProcessingContext processingContext, CancellationToken cancellationToken = default)
    {
        if (processingContext == null) throw new ArgumentNullException(nameof(processingContext));

        try
        {
            Status = ProcessorStatus.Processing;

            // Map the data
            var result = await MapDataAsync(processingContext, cancellationToken);

            Status = ProcessorStatus.Idle;
            return result;
        }
        catch (Exception ex)
        {
            Status = ProcessorStatus.Error;
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                $"Error mapping data: {ex.Message}",
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
            SupportsValidation = false,
            SupportsTransformation = false,
            SupportsMapping = true,
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
            SupportedOutputDataTypes = new List<string> { "json", "object", "string" },
            SupportedMappingRuleTypes = new List<string> { "json-map", "auto-mapper", "custom" },
            SupportedErrorRecoveryStrategies = new List<string> { "retry", "skip" }
        };
    }

    private async Task<ProcessingResult> MapDataAsync(ProcessingContext processingContext, CancellationToken cancellationToken)
    {
        // Parse the mapping rules
        var mappingRules = processingContext.MappingRules;
        if (string.IsNullOrWhiteSpace(mappingRules))
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "No mapping rules specified",
                0,
                1,
                1);
        }

        // Parse the input data
        var inputData = ParseInputData(processingContext.InputData);
        if (inputData == null)
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "Failed to parse input data",
                0,
                1,
                1);
        }

        // Parse the mapping rules
        var mappingDefinition = ParseMappingRules(mappingRules);
        if (mappingDefinition == null)
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "Failed to parse mapping rules",
                0,
                1,
                1);
        }

        // Apply the mapping
        object? mappedData;

        if (mappingDefinition.MappingType == "auto-mapper")
        {
            // Use AutoMapper
            mappedData = MapWithAutoMapper(inputData, mappingDefinition);
        }
        else if (mappingDefinition.MappingType == "json-map")
        {
            // Use the transformation engine
            var dataPackage = new DataPackage
            {
                PackageId = processingContext.ProcessingId,
                Data = inputData,
                DataType = processingContext.InputDataType ?? "json",
                DataFormat = "json"
            };

            var transformationMappingDefinition = new TransformationEngine.MappingDefinition
            {
                MappingId = Guid.NewGuid().ToString(),
                Name = mappingDefinition.Name,
                Description = mappingDefinition.Description,
                MappingContent = mappingDefinition.MappingContent,
                MappingLanguage = "json",
                SourceType = processingContext.InputDataType ?? "json",
                TargetType = processingContext.OutputDataType ?? "json"
            };

            var mappingResult = await _transformationEngine.ApplyMappingAsync(dataPackage, transformationMappingDefinition, cancellationToken);
            if (!mappingResult.IsSuccessful)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    $"Mapping failed: {mappingResult.ErrorMessage}",
                    0,
                    1,
                    1);
            }

            mappedData = mappingResult.OutputData;
        }
        else
        {
            // Custom mapping
            mappedData = MapWithCustomMapping(inputData, mappingDefinition);
        }

        // Create the mapping result
        var result = MappingResult.Success(
            inputData,
            processingContext.InputDataType ?? "unknown",
            mappedData,
            processingContext.OutputDataType ?? "unknown",
            mappingRules,
            mappingDefinition.MappingType);

        // Create the processing result
        return ProcessingResult.Success(
            processingContext.ProcessingId,
            mappedData,
            processingContext.OutputDataType ?? "unknown",
            1,
            1);
    }

    private object? ParseInputData(object? inputData)
    {
        if (inputData == null)
        {
            return null;
        }

        // If the input data is a string, try to parse it as JSON
        if (inputData is string jsonString)
        {
            try
            {
                return JToken.Parse(jsonString);
            }
            catch
            {
                return jsonString;
            }
        }

        // Otherwise, just return the input data
        return inputData;
    }

    private MappingDefinition? ParseMappingRules(string rules)
    {
        try
        {
            return JsonConvert.DeserializeObject<MappingDefinition>(rules);
        }
        catch
        {
            return null;
        }
    }

    private object? MapWithAutoMapper(object? inputData, MappingDefinition mappingDefinition)
    {
        // This is a simplified implementation. In a real system, this would use AutoMapper
        // to map the input data to the output data.

        // For demonstration purposes, we'll just return the input data
        return inputData;
    }

    private object? MapWithCustomMapping(object? inputData, MappingDefinition mappingDefinition)
    {
        // This is a simplified implementation. In a real system, this would use a custom mapping
        // implementation to map the input data to the output data.

        // For demonstration purposes, we'll just return the input data
        return inputData;
    }
}
