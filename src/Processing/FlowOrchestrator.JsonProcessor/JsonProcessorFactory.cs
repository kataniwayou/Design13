using FlowOrchestrator.ProcessorBase;
using FlowOrchestrator.TransformationEngine;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.JsonProcessor;

/// <summary>
/// Factory for creating JSON processors.
/// </summary>
public class JsonProcessorFactory : IJsonProcessorFactory
{
    private readonly ITransformationEngine _transformationEngine;
    private readonly ILoggerFactory _loggerFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonProcessorFactory"/> class.
    /// </summary>
    /// <param name="transformationEngine">The transformation engine.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public JsonProcessorFactory(
        ITransformationEngine transformationEngine,
        ILoggerFactory loggerFactory)
    {
        _transformationEngine = transformationEngine ?? throw new ArgumentNullException(nameof(transformationEngine));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
    
    /// <inheritdoc />
    public JsonProcessor CreateProcessor(string processorId, string name, string description)
    {
        var logger = _loggerFactory.CreateLogger<JsonProcessor>();
        return new JsonProcessor(processorId, name, description, _transformationEngine, logger);
    }
}
