namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Interface for importer services that import data from external sources.
/// </summary>
public interface IImporterService : IService
{
    /// <summary>
    /// Gets the source type that this importer supports.
    /// </summary>
    string SourceType { get; }
    
    /// <summary>
    /// Gets the supported protocols for this importer.
    /// </summary>
    IEnumerable<string> SupportedProtocols { get; }
    
    /// <summary>
    /// Gets the supported data formats for this importer.
    /// </summary>
    IEnumerable<string> SupportedDataFormats { get; }
    
    /// <summary>
    /// Imports data from the specified source.
    /// </summary>
    /// <param name="sourceConfiguration">The configuration for the source.</param>
    /// <param name="importOptions">The options for the import operation.</param>
    /// <returns>The result of the import operation.</returns>
    Task<ImportResult> ImportAsync(string sourceConfiguration, string importOptions);
    
    /// <summary>
    /// Tests the connection to the specified source.
    /// </summary>
    /// <param name="sourceConfiguration">The configuration for the source.</param>
    /// <returns>True if the connection was successful, false otherwise.</returns>
    Task<bool> TestConnectionAsync(string sourceConfiguration);
    
    /// <summary>
    /// Gets the schema of the data from the specified source.
    /// </summary>
    /// <param name="sourceConfiguration">The configuration for the source.</param>
    /// <returns>The schema of the data.</returns>
    Task<string> GetSchemaAsync(string sourceConfiguration);
}
