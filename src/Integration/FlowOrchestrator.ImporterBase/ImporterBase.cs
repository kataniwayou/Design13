using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.ImporterBase;

/// <summary>
/// Base class for all importers in the system.
/// </summary>
public abstract class ImporterBase : IImporter
{
    private bool _disposed;
    private readonly ILogger _logger;
    
    /// <summary>
    /// Gets the unique identifier for this importer.
    /// </summary>
    public string ImporterId { get; }
    
    /// <summary>
    /// Gets the name of this importer.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Gets the description of this importer.
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Gets the type of this importer.
    /// </summary>
    public abstract string ImporterType { get; }
    
    /// <summary>
    /// Gets the version of this importer.
    /// </summary>
    public virtual string Version => "1.0.0";
    
    /// <summary>
    /// Gets the status of this importer.
    /// </summary>
    public ImporterStatus Status { get; protected set; }
    
    /// <summary>
    /// Gets the configuration for this importer.
    /// </summary>
    public ImporterConfiguration Configuration { get; protected set; }
    
    /// <summary>
    /// Gets the connection manager for this importer.
    /// </summary>
    public IConnectionManager ConnectionManager { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterBase"/> class.
    /// </summary>
    /// <param name="importerId">The unique identifier for this importer.</param>
    /// <param name="name">The name of this importer.</param>
    /// <param name="description">The description of this importer.</param>
    /// <param name="connectionManager">The connection manager for this importer.</param>
    /// <param name="logger">The logger.</param>
    protected ImporterBase(
        string importerId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger logger)
    {
        ImporterId = importerId ?? throw new ArgumentNullException(nameof(importerId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ConnectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        Status = ImporterStatus.Created;
        Configuration = new ImporterConfiguration();
    }
    
    /// <inheritdoc />
    public virtual async Task InitializeAsync(ImporterConfiguration configuration, CancellationToken cancellationToken = default)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        
        _logger.LogInformation("Initializing importer {ImporterId} with configuration {ConfigurationId}",
            ImporterId, configuration.ConfigurationId);
        
        Configuration = configuration;
        Status = ImporterStatus.Initialized;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public virtual async Task OpenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Opening connection for importer {ImporterId}", ImporterId);
        
        if (Status != ImporterStatus.Initialized && Status != ImporterStatus.Closed)
        {
            throw new InvalidOperationException($"Cannot open importer {ImporterId} in status {Status}");
        }
        
        await ConnectionManager.OpenAsync(cancellationToken);
        Status = ImporterStatus.Open;
    }
    
    /// <inheritdoc />
    public virtual async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing connection for importer {ImporterId}", ImporterId);
        
        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot close importer {ImporterId} in status {Status}");
        }
        
        await ConnectionManager.CloseAsync(cancellationToken);
        Status = ImporterStatus.Closed;
    }
    
    /// <inheritdoc />
    public abstract Task<ImportResult> ImportAsync(ImportContext importContext, CancellationToken cancellationToken = default);
    
    /// <inheritdoc />
    public virtual async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Testing connection for importer {ImporterId}", ImporterId);
        
        try
        {
            await ConnectionManager.TestConnectionAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Connection test failed for importer {ImporterId}", ImporterId);
            return false;
        }
    }
    
    /// <inheritdoc />
    public abstract Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default);
    
    /// <inheritdoc />
    public abstract ImporterCapabilities GetCapabilities();
    
    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="ImporterBase"/> and optionally releases the managed resources.
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
    /// Finalizes an instance of the <see cref="ImporterBase"/> class.
    /// </summary>
    ~ImporterBase()
    {
        Dispose(false);
    }
}
