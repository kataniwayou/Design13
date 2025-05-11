using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;

namespace FlowOrchestrator.ExporterBase;

/// <summary>
/// Defines the interface for all exporters in the system.
/// </summary>
public interface IExporter : IDisposable
{
    /// <summary>
    /// Gets the unique identifier for this exporter.
    /// </summary>
    string ExporterId { get; }
    
    /// <summary>
    /// Gets the name of this exporter.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this exporter.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the type of this exporter.
    /// </summary>
    string ExporterType { get; }
    
    /// <summary>
    /// Gets the version of this exporter.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Gets the status of this exporter.
    /// </summary>
    ExporterStatus Status { get; }
    
    /// <summary>
    /// Gets the configuration for this exporter.
    /// </summary>
    ExporterConfiguration Configuration { get; }
    
    /// <summary>
    /// Gets the connection manager for this exporter.
    /// </summary>
    IConnectionManager ConnectionManager { get; }
    
    /// <summary>
    /// Initializes the exporter with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InitializeAsync(ExporterConfiguration configuration, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Opens the connection to the data destination.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task OpenAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Closes the connection to the data destination.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CloseAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Exports data to the data destination.
    /// </summary>
    /// <param name="exportContext">The export context.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the export result.</returns>
    Task<ExportResult> ExportAsync(ExportContext exportContext, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Tests the connection to the data destination.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing a value indicating whether the connection test was successful.</returns>
    Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the schema of the data destination.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the schema of the data destination.</returns>
    Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the capabilities of this exporter.
    /// </summary>
    /// <returns>The capabilities of this exporter.</returns>
    ExporterCapabilities GetCapabilities();
}
