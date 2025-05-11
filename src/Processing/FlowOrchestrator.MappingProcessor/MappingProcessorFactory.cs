using AutoMapper;
using FlowOrchestrator.TransformationEngine;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.MappingProcessor;

/// <summary>
/// Factory for creating mapping processors.
/// </summary>
public class MappingProcessorFactory : IMappingProcessorFactory
{
    private readonly ITransformationEngine _transformationEngine;
    private readonly IMapper _mapper;
    private readonly ILoggerFactory _loggerFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProcessorFactory"/> class.
    /// </summary>
    /// <param name="transformationEngine">The transformation engine.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public MappingProcessorFactory(
        ITransformationEngine transformationEngine,
        IMapper mapper,
        ILoggerFactory loggerFactory)
    {
        _transformationEngine = transformationEngine ?? throw new ArgumentNullException(nameof(transformationEngine));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
    
    /// <inheritdoc />
    public MappingProcessor CreateProcessor(string processorId, string name, string description)
    {
        var logger = _loggerFactory.CreateLogger<MappingProcessor>();
        return new MappingProcessor(processorId, name, description, _transformationEngine, _mapper, logger);
    }
}
