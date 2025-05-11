using FlowOrchestrator.ProcessorBase;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Provides a centralized service for data transformation across all processor implementations.
/// </summary>
public class TransformationEngine : ITransformationEngine
{
    private readonly ILogger<TransformationEngine> _logger;
    private readonly ITransformationRuleManager _ruleManager;
    private readonly ITransformationPerformanceOptimizer _performanceOptimizer;
    private readonly ITransformationErrorHandler _errorHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransformationEngine"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="ruleManager">The transformation rule manager.</param>
    /// <param name="performanceOptimizer">The transformation performance optimizer.</param>
    /// <param name="errorHandler">The transformation error handler.</param>
    public TransformationEngine(
        ILogger<TransformationEngine> logger,
        ITransformationRuleManager ruleManager,
        ITransformationPerformanceOptimizer performanceOptimizer,
        ITransformationErrorHandler errorHandler)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ruleManager = ruleManager ?? throw new ArgumentNullException(nameof(ruleManager));
        _performanceOptimizer = performanceOptimizer ?? throw new ArgumentNullException(nameof(performanceOptimizer));
        _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    /// <inheritdoc />
    public async Task<TransformationResult> TransformAsync(DataPackage input, TransformationRule rule, CancellationToken cancellationToken = default)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (rule == null) throw new ArgumentNullException(nameof(rule));

        _logger.LogInformation("Transforming data using rule {RuleId}", rule.RuleId);

        try
        {
            // Validate the rule
            var validationResult = await _ruleManager.ValidateRuleAsync(rule, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Transformation rule validation failed: {ErrorMessage}", validationResult.ErrorMessage);
                return TransformationResult.Failure(
                    validationResult.ErrorMessage ?? "Transformation rule validation failed",
                    input.Data,
                    input.DataType,
                    rule.RuleId,
                    rule.RuleType);
            }

            // Optimize the rule if needed
            TransformationRule optimizedRule = rule;
            if (rule.OptimizationEnabled)
            {
                var optimizationResult = await _performanceOptimizer.OptimizeRuleAsync(rule, cancellationToken);
                if (optimizationResult.IsSuccessful)
                {
                    optimizedRule = optimizationResult.OptimizedRule ?? rule;
                }
            }

            // Apply the transformation
            var startTime = DateTime.UtcNow;
            var outputData = await ApplyTransformationAsync(input.Data, optimizedRule, cancellationToken);
            var endTime = DateTime.UtcNow;

            return TransformationResult.Success(
                input.Data,
                input.DataType,
                outputData,
                optimizedRule.OutputDataType,
                optimizedRule.RuleId,
                optimizedRule.RuleType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error transforming data using rule {RuleId}", rule.RuleId);

            // Handle the error
            var transformationError = new TransformationError
            {
                Exception = ex,
                Input = input,
                Rule = rule,
                ErrorMessage = ex.Message,
                ErrorTime = DateTime.UtcNow
            };

            var recoveryStrategy = await _errorHandler.DetermineRecoveryStrategyAsync(transformationError, cancellationToken);
            if (recoveryStrategy != null)
            {
                var recoveryResult = await _errorHandler.ExecuteRecoveryAsync(transformationError, recoveryStrategy, cancellationToken);
                if (recoveryResult.IsSuccessful)
                {
                    _logger.LogInformation("Successfully recovered from transformation error using strategy {StrategyName}", recoveryStrategy.StrategyName);
                    return TransformationResult.Success(
                        input.Data,
                        input.DataType,
                        recoveryResult.OutputData,
                        rule.OutputDataType,
                        rule.RuleId,
                        rule.RuleType);
                }
            }

            return TransformationResult.Failure(
                ex.Message,
                input.Data,
                input.DataType,
                rule.RuleId,
                rule.RuleType);
        }
    }

    /// <inheritdoc />
    public async Task<ValidationResult> ValidateTransformationAsync(DataPackage input, TransformationRule rule, CancellationToken cancellationToken = default)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (rule == null) throw new ArgumentNullException(nameof(rule));

        _logger.LogInformation("Validating transformation for rule {RuleId}", rule.RuleId);

        try
        {
            // Validate the rule
            var ruleValidationResult = await _ruleManager.ValidateRuleAsync(rule, cancellationToken);
            if (!ruleValidationResult.IsValid)
            {
                _logger.LogError("Transformation rule validation failed: {ErrorMessage}", ruleValidationResult.ErrorMessage);
                return ValidationResult.Failure(
                    new List<ValidationError>
                    {
                        new ValidationError
                        {
                            ErrorCode = "RULE_VALIDATION_FAILED",
                            ErrorMessage = ruleValidationResult.ErrorMessage ?? "Transformation rule validation failed",
                            Severity = ValidationSeverity.Error
                        }
                    },
                    input.Data,
                    input.DataType,
                    rule.RuleId,
                    rule.RuleType);
            }

            // Validate the input data against the rule's input requirements
            var inputValidationResult = ValidateInputData(input, rule);
            if (!inputValidationResult.IsValid)
            {
                _logger.LogError("Input data validation failed for rule {RuleId}", rule.RuleId);
                return inputValidationResult;
            }

            return ValidationResult.Success(
                input.Data,
                input.DataType,
                rule.RuleId,
                rule.RuleType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating transformation for rule {RuleId}", rule.RuleId);

            return ValidationResult.Failure(
                new List<ValidationError>
                {
                    new ValidationError
                    {
                        ErrorCode = "VALIDATION_ERROR",
                        ErrorMessage = ex.Message,
                        Severity = ValidationSeverity.Error
                    }
                },
                input.Data,
                input.DataType,
                rule.RuleId,
                rule.RuleType);
        }
    }

    /// <inheritdoc />
    public async Task<TransformationResult> ApplyMappingAsync(DataPackage input, MappingDefinition mapping, CancellationToken cancellationToken = default)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (mapping == null) throw new ArgumentNullException(nameof(mapping));

        _logger.LogInformation("Applying mapping {MappingId} to data", mapping.MappingId);

        try
        {
            // Convert mapping to transformation rule
            var rule = new TransformationRule
            {
                RuleId = mapping.MappingId,
                RuleType = "Mapping",
                RuleDefinition = mapping.MappingContent,
                InputDataType = mapping.SourceType,
                OutputDataType = mapping.TargetType,
                OptimizationEnabled = true
            };

            // Apply the transformation
            return await TransformAsync(input, rule, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying mapping {MappingId}", mapping.MappingId);

            return TransformationResult.Failure(
                ex.Message,
                input.Data,
                input.DataType,
                mapping.MappingId,
                "Mapping");
        }
    }

    /// <inheritdoc />
    public async Task<TransformationResult> ApplyCustomTransformationAsync(DataPackage input, CustomTransformation transformation, CancellationToken cancellationToken = default)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (transformation == null) throw new ArgumentNullException(nameof(transformation));

        _logger.LogInformation("Applying custom transformation {TransformationId} to data", transformation.TransformationId);

        try
        {
            // Convert custom transformation to transformation rule
            var rule = new TransformationRule
            {
                RuleId = transformation.TransformationId,
                RuleType = "Custom",
                RuleDefinition = transformation.TransformationDefinition,
                InputDataType = transformation.InputDataType,
                OutputDataType = transformation.OutputDataType,
                OptimizationEnabled = transformation.OptimizationEnabled
            };

            // Apply the transformation
            return await TransformAsync(input, rule, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying custom transformation {TransformationId}", transformation.TransformationId);

            return TransformationResult.Failure(
                ex.Message,
                input.Data,
                input.DataType,
                transformation.TransformationId,
                "Custom");
        }
    }

    /// <inheritdoc />
    public async Task<BatchTransformationResult> TransformBatchAsync(IEnumerable<DataPackage> inputs, TransformationRule rule, CancellationToken cancellationToken = default)
    {
        if (inputs == null) throw new ArgumentNullException(nameof(inputs));
        if (rule == null) throw new ArgumentNullException(nameof(rule));

        var inputsList = inputs.ToList();
        _logger.LogInformation("Transforming batch of {Count} items using rule {RuleId}", inputsList.Count, rule.RuleId);

        var results = new List<TransformationResult>();
        var startTime = DateTime.UtcNow;

        try
        {
            // Validate the rule
            var validationResult = await _ruleManager.ValidateRuleAsync(rule, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Transformation rule validation failed: {ErrorMessage}", validationResult.ErrorMessage);
                return BatchTransformationResult.Failure(
                    validationResult.ErrorMessage ?? "Transformation rule validation failed",
                    inputsList,
                    rule);
            }

            // Optimize the rule if needed
            TransformationRule optimizedRule = rule;
            if (rule.OptimizationEnabled)
            {
                var optimizationResult = await _performanceOptimizer.OptimizeRuleAsync(rule, cancellationToken);
                if (optimizationResult.IsSuccessful)
                {
                    optimizedRule = optimizationResult.OptimizedRule ?? rule;
                }
            }

            // Process each input in the batch
            foreach (var input in inputsList)
            {
                var result = await TransformAsync(input, optimizedRule, cancellationToken);
                results.Add(result);

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Batch transformation cancelled after processing {Count} items", results.Count);
                    break;
                }
            }

            var endTime = DateTime.UtcNow;
            var durationMs = (long)(endTime - startTime).TotalMilliseconds;

            var successCount = results.Count(r => r.IsSuccessful);
            var failureCount = results.Count - successCount;

            return new BatchTransformationResult
            {
                IsSuccessful = failureCount == 0,
                Results = results,
                SuccessCount = successCount,
                FailureCount = failureCount,
                TotalCount = results.Count,
                Rule = optimizedRule,
                StartTime = startTime,
                EndTime = endTime,
                DurationMs = durationMs
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error transforming batch using rule {RuleId}", rule.RuleId);

            var endTime = DateTime.UtcNow;
            var durationMs = (long)(endTime - startTime).TotalMilliseconds;

            return BatchTransformationResult.Failure(
                ex.Message,
                inputsList,
                rule,
                results,
                startTime,
                endTime,
                durationMs);
        }
    }

    /// <inheritdoc />
    public async Task<TransformationOptimizationResult> OptimizeTransformationAsync(TransformationRule rule, CancellationToken cancellationToken = default)
    {
        if (rule == null) throw new ArgumentNullException(nameof(rule));

        _logger.LogInformation("Optimizing transformation rule {RuleId}", rule.RuleId);

        try
        {
            // Validate the rule first
            var validationResult = await _ruleManager.ValidateRuleAsync(rule, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Transformation rule validation failed: {ErrorMessage}", validationResult.ErrorMessage);
                return TransformationOptimizationResult.Failure(
                    validationResult.ErrorMessage ?? "Transformation rule validation failed",
                    rule);
            }

            // Optimize the rule
            var optimizationResult = await _performanceOptimizer.OptimizeRuleAsync(rule, cancellationToken);
            if (!optimizationResult.IsSuccessful)
            {
                _logger.LogWarning("Rule optimization failed: {ErrorMessage}", optimizationResult.ErrorMessage);
                return TransformationOptimizationResult.Failure(
                    optimizationResult.ErrorMessage ?? "Rule optimization failed",
                    rule);
            }

            return TransformationOptimizationResult.Success(
                rule,
                optimizationResult.OptimizedRule,
                optimizationResult.OptimizationFactor,
                optimizationResult.OptimizationTechniques);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error optimizing transformation rule {RuleId}", rule.RuleId);

            return TransformationOptimizationResult.Failure(
                ex.Message,
                rule);
        }
    }

    /// <inheritdoc />
    public async Task<TransformationPerformanceReport> ProfileTransformationAsync(TransformationRule rule, DataPackage testData, CancellationToken cancellationToken = default)
    {
        if (rule == null) throw new ArgumentNullException(nameof(rule));
        if (testData == null) throw new ArgumentNullException(nameof(testData));

        _logger.LogInformation("Profiling transformation rule {RuleId}", rule.RuleId);

        try
        {
            // Validate the rule first
            var validationResult = await _ruleManager.ValidateRuleAsync(rule, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Transformation rule validation failed: {ErrorMessage}", validationResult.ErrorMessage);
                return TransformationPerformanceReport.Failure(
                    validationResult.ErrorMessage ?? "Transformation rule validation failed",
                    rule,
                    testData);
            }

            // Create a performance profile
            var dataSample = new DataSample
            {
                SampleId = Guid.NewGuid().ToString(),
                SampleData = testData.Data,
                SampleDataType = testData.DataType,
                SampleSize = CalculateSampleSize(testData.Data)
            };

            var performanceProfile = await _performanceOptimizer.AnalyzeTransformationPerformanceAsync(rule, dataSample, cancellationToken);

            // Run the transformation multiple times to get performance metrics
            var iterations = 5;
            var results = new List<TransformationResult>();

            for (int i = 0; i < iterations; i++)
            {
                var result = await TransformAsync(testData, rule, cancellationToken);
                results.Add(result);

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Performance profiling cancelled after {Count} iterations", i + 1);
                    break;
                }
            }

            // Calculate average performance metrics
            var averageDuration = results.Average(r => r.DurationMs);
            var minDuration = results.Min(r => r.DurationMs);
            var maxDuration = results.Max(r => r.DurationMs);

            // Create optimization plan
            var optimizationPlan = await _performanceOptimizer.CreateOptimizationPlanAsync(performanceProfile, cancellationToken);

            return new TransformationPerformanceReport
            {
                IsSuccessful = true,
                Rule = rule,
                TestData = testData,
                PerformanceProfile = performanceProfile,
                OptimizationPlan = optimizationPlan,
                AverageDurationMs = averageDuration,
                MinDurationMs = minDuration,
                MaxDurationMs = maxDuration,
                IterationsRun = results.Count,
                MemoryUsageBytes = performanceProfile.MemoryUsageBytes,
                CpuUsagePercentage = performanceProfile.CpuUsagePercentage,
                Bottlenecks = performanceProfile.Bottlenecks,
                OptimizationRecommendations = optimizationPlan.Recommendations
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error profiling transformation rule {RuleId}", rule.RuleId);

            return TransformationPerformanceReport.Failure(
                ex.Message,
                rule,
                testData);
        }
    }

    // Private helper methods

    private async Task<object?> ApplyTransformationAsync(object? inputData, TransformationRule rule, CancellationToken cancellationToken)
    {
        // This is a simplified implementation. In a real system, this would use the rule definition
        // to transform the input data according to the rule type.

        // For demonstration purposes, we'll just return the input data
        await Task.Delay(100, cancellationToken); // Simulate processing time

        return inputData;
    }

    private ValidationResult ValidateInputData(DataPackage input, TransformationRule rule)
    {
        // This is a simplified implementation. In a real system, this would validate the input data
        // against the rule's input requirements.

        // For demonstration purposes, we'll just check that the input data type matches the rule's input data type
        if (input.DataType != rule.InputDataType)
        {
            return ValidationResult.Failure(
                new List<ValidationError>
                {
                    new ValidationError
                    {
                        ErrorCode = "INPUT_TYPE_MISMATCH",
                        ErrorMessage = $"Input data type '{input.DataType}' does not match rule input data type '{rule.InputDataType}'",
                        Severity = ValidationSeverity.Error
                    }
                },
                input.Data,
                input.DataType,
                rule.RuleId,
                rule.RuleType);
        }

        return ValidationResult.Success(
            input.Data,
            input.DataType,
            rule.RuleId,
            rule.RuleType);
    }

    private long CalculateSampleSize(object? data)
    {
        // This is a simplified implementation. In a real system, this would calculate the size of the data.

        // For demonstration purposes, we'll just return a fixed size
        return 1024; // 1 KB
    }
}
