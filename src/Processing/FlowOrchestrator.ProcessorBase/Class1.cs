using FlowOrchestrator.Common.Configuration;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Base class for all processors in the system.
/// </summary>
public abstract class ProcessorBase : IProcessor
{
    private bool _disposed;
    private readonly ILogger _logger;

    /// <summary>
    /// Gets the unique identifier for this processor.
    /// </summary>
    public string ProcessorId { get; }

    /// <summary>
    /// Gets the name of this processor.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the description of this processor.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the type of this processor.
    /// </summary>
    public abstract string ProcessorType { get; }

    /// <summary>
    /// Gets the version of this processor.
    /// </summary>
    public virtual string Version => "1.0.0";

    /// <summary>
    /// Gets the status of this processor.
    /// </summary>
    public ProcessorStatus Status { get; protected set; }

    /// <summary>
    /// Gets the configuration for this processor.
    /// </summary>
    public ProcessorConfiguration Configuration { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessorBase"/> class.
    /// </summary>
    /// <param name="processorId">The unique identifier for this processor.</param>
    /// <param name="name">The name of this processor.</param>
    /// <param name="description">The description of this processor.</param>
    /// <param name="logger">The logger.</param>
    protected ProcessorBase(
        string processorId,
        string name,
        string description,
        ILogger logger)
    {
        ProcessorId = processorId ?? throw new ArgumentNullException(nameof(processorId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        Status = ProcessorStatus.Created;
        Configuration = new ProcessorConfiguration();
    }

    /// <inheritdoc />
    public virtual async Task InitializeAsync(ProcessorConfiguration configuration, CancellationToken cancellationToken = default)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        _logger.LogInformation("Initializing processor {ProcessorId} with configuration {ConfigurationId}",
            ProcessorId, configuration.ConfigurationId);

        Configuration = configuration;
        Status = ProcessorStatus.Initialized;

        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public abstract Task<ProcessingResult> ProcessAsync(ProcessingContext processingContext, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public abstract ProcessorCapabilities GetCapabilities();

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="ProcessorBase"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // Dispose managed resources
        }

        // Dispose unmanaged resources

        _disposed = true;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="ProcessorBase"/> class.
    /// </summary>
    ~ProcessorBase()
    {
        Dispose(false);
    }
}
