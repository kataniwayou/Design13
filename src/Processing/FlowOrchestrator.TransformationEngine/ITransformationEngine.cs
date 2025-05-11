using FlowOrchestrator.ProcessorBase;

namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Defines the interface for the transformation engine.
/// </summary>
public interface ITransformationEngine
{
    /// <summary>
    /// Transforms data using the specified transformation rule.
    /// </summary>
    /// <param name="input">The input data package.</param>
    /// <param name="rule">The transformation rule to apply.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the transformation result.</returns>
    Task<TransformationResult> TransformAsync(DataPackage input, TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Validates a transformation without applying it.
    /// </summary>
    /// <param name="input">The input data package.</param>
    /// <param name="rule">The transformation rule to validate.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the validation result.</returns>
    Task<ValidationResult> ValidateTransformationAsync(DataPackage input, TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Applies a mapping to the input data.
    /// </summary>
    /// <param name="input">The input data package.</param>
    /// <param name="mapping">The mapping definition to apply.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the transformation result.</returns>
    Task<TransformationResult> ApplyMappingAsync(DataPackage input, MappingDefinition mapping, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Applies a custom transformation to the input data.
    /// </summary>
    /// <param name="input">The input data package.</param>
    /// <param name="transformation">The custom transformation to apply.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the transformation result.</returns>
    Task<TransformationResult> ApplyCustomTransformationAsync(DataPackage input, CustomTransformation transformation, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Transforms a batch of data using the specified transformation rule.
    /// </summary>
    /// <param name="inputs">The input data packages.</param>
    /// <param name="rule">The transformation rule to apply.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the batch transformation result.</returns>
    Task<BatchTransformationResult> TransformBatchAsync(IEnumerable<DataPackage> inputs, TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Optimizes a transformation rule for better performance.
    /// </summary>
    /// <param name="rule">The transformation rule to optimize.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the optimization result.</returns>
    Task<TransformationOptimizationResult> OptimizeTransformationAsync(TransformationRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Profiles the performance of a transformation rule.
    /// </summary>
    /// <param name="rule">The transformation rule to profile.</param>
    /// <param name="testData">The test data to use for profiling.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the performance report.</returns>
    Task<TransformationPerformanceReport> ProfileTransformationAsync(TransformationRule rule, DataPackage testData, CancellationToken cancellationToken = default);
}
