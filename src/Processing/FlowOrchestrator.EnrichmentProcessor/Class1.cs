using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.ProcessorBase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Processor for data enrichment.
/// </summary>
public class EnrichmentProcessor : FlowOrchestrator.ProcessorBase.ProcessorBase
{
    private readonly IEnrichmentSourceProvider _enrichmentSourceProvider;

    /// <summary>
    /// Gets the type of this processor.
    /// </summary>
    public override string ProcessorType => "EnrichmentProcessor";

    /// <summary>
    /// Initializes a new instance of the <see cref="EnrichmentProcessor"/> class.
    /// </summary>
    /// <param name="processorId">The unique identifier for this processor.</param>
    /// <param name="name">The name of this processor.</param>
    /// <param name="description">The description of this processor.</param>
    /// <param name="enrichmentSourceProvider">The enrichment source provider.</param>
    /// <param name="logger">The logger.</param>
    public EnrichmentProcessor(
        string processorId,
        string name,
        string description,
        IEnrichmentSourceProvider enrichmentSourceProvider,
        ILogger<EnrichmentProcessor> logger)
        : base(processorId, name, description, logger)
    {
        _enrichmentSourceProvider = enrichmentSourceProvider ?? throw new ArgumentNullException(nameof(enrichmentSourceProvider));
    }

    /// <inheritdoc />
    public override async Task<ProcessingResult> ProcessAsync(ProcessingContext processingContext, CancellationToken cancellationToken = default)
    {
        if (processingContext == null) throw new ArgumentNullException(nameof(processingContext));

        try
        {
            Status = ProcessorStatus.Processing;

            // Enrich the data
            var result = await EnrichDataAsync(processingContext, cancellationToken);

            Status = ProcessorStatus.Idle;
            return result;
        }
        catch (Exception ex)
        {
            Status = ProcessorStatus.Error;
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                $"Error enriching data: {ex.Message}",
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
            SupportsMapping = false,
            SupportsEnrichment = true,
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
            SupportedEnrichmentRuleTypes = new List<string> { "json-enrich", "custom" },
            SupportedErrorRecoveryStrategies = new List<string> { "retry", "skip" }
        };
    }

    private async Task<ProcessingResult> EnrichDataAsync(ProcessingContext processingContext, CancellationToken cancellationToken)
    {
        // Parse the enrichment rules
        var enrichmentRules = processingContext.EnrichmentRules;
        if (string.IsNullOrWhiteSpace(enrichmentRules))
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "No enrichment rules specified",
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

        // Parse the enrichment rules
        var rules = ParseEnrichmentRules(enrichmentRules);
        if (rules == null || rules.Count == 0)
        {
            return ProcessingResult.Failure(
                processingContext.ProcessingId,
                "Failed to parse enrichment rules",
                0,
                1,
                1);
        }

        // Apply the enrichment rules
        var enrichedData = inputData;
        var enrichmentSources = new List<string>();

        foreach (var rule in rules)
        {
            // Get the enrichment source
            var source = await _enrichmentSourceProvider.GetEnrichmentSourceAsync(rule.SourceName, cancellationToken);
            if (source == null)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    $"Enrichment source not found: {rule.SourceName}",
                    0,
                    1,
                    1);
            }

            // Add the source to the list
            if (!enrichmentSources.Contains(rule.SourceName))
            {
                enrichmentSources.Add(rule.SourceName);
            }

            // Apply the enrichment rule
            var enrichmentResult = await source.EnrichAsync(enrichedData, rule, cancellationToken);
            if (!enrichmentResult.IsSuccessful)
            {
                return ProcessingResult.Failure(
                    processingContext.ProcessingId,
                    $"Enrichment failed: {enrichmentResult.ErrorMessage}",
                    0,
                    1,
                    1);
            }

            // Update the enriched data
            enrichedData = enrichmentResult.EnrichedData;
        }

        // Create the enrichment result
        var result = EnrichmentResult.Success(
            inputData,
            processingContext.InputDataType ?? "unknown",
            enrichedData,
            processingContext.OutputDataType ?? processingContext.InputDataType ?? "unknown",
            enrichmentRules,
            "json-enrich",
            enrichmentSources);

        // Create the processing result
        return ProcessingResult.Success(
            processingContext.ProcessingId,
            enrichedData,
            processingContext.OutputDataType ?? processingContext.InputDataType ?? "unknown",
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

    private List<EnrichmentRule> ParseEnrichmentRules(string rules)
    {
        try
        {
            return JsonConvert.DeserializeObject<List<EnrichmentRule>>(rules) ?? new List<EnrichmentRule>();
        }
        catch
        {
            return new List<EnrichmentRule>();
        }
    }
}
