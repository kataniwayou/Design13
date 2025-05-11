using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.ExporterBase;

/// <summary>
/// Base class for all exporters in the system.
/// </summary>
public abstract class ExporterBase : IExporter
{
    private bool _disposed;
    private readonly ILogger _logger;
    
    /// <summary>
    /// Gets the unique identifier for this exporter.
    /// </summary>
    public string ExporterId { get; }
    
    /// <summary>
    /// Gets the name of this exporter.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Gets the description of this exporter.
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Gets the type of this exporter.
    /// </summary>
    public abstract string ExporterType { get; }
    
    /// <summary>
    /// Gets the version of this exporter.
    /// </summary>
    public virtual string Version => "1.0.0";
    
    /// <summary>
    /// Gets the status of this exporter.
    /// </summary>
    public ExporterStatus Status { get; protected set; }
    
    /// <summary>
    /// Gets the configuration for this exporter.
    /// </summary>
    public ExporterConfiguration Configuration { get; protected set; }
    
    /// <summary>
    /// Gets the connection manager for this exporter.
    /// </summary>
    public IConnectionManager ConnectionManager { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ExporterBase"/> class.
    /// </summary>
    /// <param name="exporterId">The unique identifier for this exporter.</param>
    /// <param name="name">The name of this exporter.</param>
    /// <param name="description">The description of this exporter.</param>
    /// <param name="connectionManager">The connection manager for this exporter.</param>
    /// <param name="logger">The logger.</param>
    protected ExporterBase(
        string exporterId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger logger)
    {
        ExporterId = exporterId ?? throw new ArgumentNullException(nameof(exporterId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ConnectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        Status = ExporterStatus.Created;
        Configuration = new ExporterConfiguration();
    }
    
    /// <inheritdoc />
    public virtual async Task InitializeAsync(ExporterConfiguration configuration, CancellationToken cancellationToken = default)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        
        _logger.LogInformation("Initializing exporter {ExporterId} with configuration {ConfigurationId}",
            ExporterId, configuration.ConfigurationId);
        
        Configuration = configuration;
        Status = ExporterStatus.Initialized;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public virtual async Task OpenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Opening connection for exporter {ExporterId}", ExporterId);
        
        if (Status != ExporterStatus.Initialized && Status != ExporterStatus.Closed)
        {
            throw new InvalidOperationException($"Cannot open exporter {ExporterId} in status {Status}");
        }
        
        await ConnectionManager.OpenAsync(cancellationToken);
        Status = ExporterStatus.Open;
    }
    
    /// <inheritdoc />
    public virtual async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing connection for exporter {ExporterId}", ExporterId);
        
        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot close exporter {ExporterId} in status {Status}");
        }
        
        await ConnectionManager.CloseAsync(cancellationToken);
        Status = ExporterStatus.Closed;
    }
    
    /// <inheritdoc />
    public abstract Task<ExportResult> ExportAsync(ExportContext exportContext, CancellationToken cancellationToken = default);
    
    /// <inheritdoc />
    public virtual async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Testing connection for exporter {ExporterId}", ExporterId);
        
        try
        {
            await ConnectionManager.TestConnectionAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Connection test failed for exporter {ExporterId}", ExporterId);
            return false;
        }
    }
    
    /// <inheritdoc />
    public abstract Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default);
    
    /// <inheritdoc />
    public abstract ExporterCapabilities GetCapabilities();
    
    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="ExporterBase"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        
        if (disposing)
        {
            // Dispose managed resources
            if (ConnectionManager is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        
        // Dispose unmanaged resources
        
        _disposed = true;
    }
    
    /// <summary>
    /// Finalizes an instance of the <see cref="ExporterBase"/> class.
    /// </summary>
    ~ExporterBase()
    {
        Dispose(false);
    }
}
