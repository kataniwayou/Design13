namespace FlowOrchestrator.ProtocolAdapters;

/// <summary>
/// Interface for protocol adapters.
/// </summary>
public interface IProtocolAdapter
{
    /// <summary>
    /// Gets the name of the protocol.
    /// </summary>
    string ProtocolName { get; }
    
    /// <summary>
    /// Gets the version of the protocol.
    /// </summary>
    string ProtocolVersion { get; }
    
    /// <summary>
    /// Gets a value indicating whether the protocol is secure.
    /// </summary>
    bool IsSecure { get; }
    
    /// <summary>
    /// Gets the default port for the protocol.
    /// </summary>
    int DefaultPort { get; }
    
    /// <summary>
    /// Gets the capabilities of the protocol adapter.
    /// </summary>
    /// <returns>The capabilities of the protocol adapter.</returns>
    ProtocolCapabilities GetCapabilities();
    
    /// <summary>
    /// Converts data from the protocol format to the internal format.
    /// </summary>
    /// <param name="data">The data to convert.</param>
    /// <param name="options">The conversion options.</param>
    /// <returns>The converted data.</returns>
    object ConvertFromProtocol(object data, ProtocolConversionOptions? options = null);
    
    /// <summary>
    /// Converts data from the internal format to the protocol format.
    /// </summary>
    /// <param name="data">The data to convert.</param>
    /// <param name="options">The conversion options.</param>
    /// <returns>The converted data.</returns>
    object ConvertToProtocol(object data, ProtocolConversionOptions? options = null);
}
