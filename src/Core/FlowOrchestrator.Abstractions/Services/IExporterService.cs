namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Interface for exporter services that export data to external destinations.
/// </summary>
public interface IExporterService : IService
{
    /// <summary>
    /// Gets the destination type that this exporter supports.
    /// </summary>
    string DestinationType { get; }
    
    /// <summary>
    /// Gets the supported protocols for this exporter.
    /// </summary>
    IEnumerable<string> SupportedProtocols { get; }
    
    /// <summary>
    /// Gets the supported data formats for this exporter.
    /// </summary>
    IEnumerable<string> SupportedDataFormats { get; }
    
    /// <summary>
    /// Exports data to the specified destination.
    /// </summary>
    /// <param name="memoryAddress">The memory address of the data to export.</param>
    /// <param name="destinationConfiguration">The configuration for the destination.</param>
    /// <param name="exportOptions">The options for the export operation.</param>
    /// <returns>The result of the export operation.</returns>
    Task<ExportResult> ExportAsync(string memoryAddress, string destinationConfiguration, string exportOptions);
    
    /// <summary>
    /// Tests the connection to the specified destination.
    /// </summary>
    /// <param name="destinationConfiguration">The configuration for the destination.</param>
    /// <returns>True if the connection was successful, false otherwise.</returns>
    Task<bool> TestConnectionAsync(string destinationConfiguration);
    
    /// <summary>
    /// Validates that the data schema is compatible with the destination.
    /// </summary>
    /// <param name="schema">The schema of the data to export.</param>
    /// <param name="destinationConfiguration">The configuration for the destination.</param>
    /// <returns>True if the schema is compatible, false otherwise.</returns>
    Task<bool> ValidateSchemaAsync(string schema, string destinationConfiguration);
}
