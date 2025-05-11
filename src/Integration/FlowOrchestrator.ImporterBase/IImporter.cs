using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;

namespace FlowOrchestrator.ImporterBase;

/// <summary>
/// Defines the interface for all importers in the system.
/// </summary>
public interface IImporter : IDisposable
{
    /// <summary>
    /// Gets the unique identifier for this importer.
    /// </summary>
    string ImporterId { get; }
    
    /// <summary>
    /// Gets the name of this importer.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this importer.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the type of this importer.
    /// </summary>
    string ImporterType { get; }
    
    /// <summary>
    /// Gets the version of this importer.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Gets the status of this importer.
    /// </summary>
    ImporterStatus Status { get; }
    
    /// <summary>
    /// Gets the configuration for this importer.
    /// </summary>
    ImporterConfiguration Configuration { get; }
    
    /// <summary>
    /// Gets the connection manager for this importer.
    /// </summary>
    IConnectionManager ConnectionManager { get; }
    
    /// <summary>
    /// Initializes the importer with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InitializeAsync(ImporterConfiguration configuration, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Opens the connection to the data source.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task OpenAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Closes the connection to the data source.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CloseAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Imports data from the data source.
    /// </summary>
    /// <param name="importContext">The import context.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the imported data.</returns>
    Task<ImportResult> ImportAsync(ImportContext importContext, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Tests the connection to the data source.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing a value indicating whether the connection test was successful.</returns>
    Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the schema of the data source.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the schema of the data source.</returns>
    Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the capabilities of this importer.
    /// </summary>
    /// <returns>The capabilities of this importer.</returns>
    ImporterCapabilities GetCapabilities();
}
