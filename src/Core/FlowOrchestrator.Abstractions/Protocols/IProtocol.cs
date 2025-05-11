namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Interface for protocols used for communication with external systems.
/// </summary>
public interface IProtocol
{
    /// <summary>
    /// Gets the unique identifier for this protocol.
    /// </summary>
    string Id { get; }
    
    /// <summary>
    /// Gets the name of this protocol.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this protocol.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the version of this protocol.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Gets the protocol type (e.g., HTTP, FTP, JDBC, etc.).
    /// </summary>
    string ProtocolType { get; }
    
    /// <summary>
    /// Gets the supported data formats for this protocol.
    /// </summary>
    IEnumerable<string> SupportedDataFormats { get; }
    
    /// <summary>
    /// Gets the capabilities of this protocol.
    /// </summary>
    ProtocolCapabilities Capabilities { get; }
    
    /// <summary>
    /// Gets the security requirements for this protocol.
    /// </summary>
    SecurityRequirements SecurityRequirements { get; }
    
    /// <summary>
    /// Creates a protocol handler for the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration for the protocol handler.</param>
    /// <returns>A protocol handler instance.</returns>
    IProtocolHandler CreateHandler(string configuration);
    
    /// <summary>
    /// Validates the configuration for this protocol.
    /// </summary>
    /// <param name="configuration">The configuration to validate.</param>
    /// <returns>True if the configuration is valid, false otherwise.</returns>
    bool ValidateConfiguration(string configuration);
}
