namespace FlowOrchestrator.ProtocolAdapters;

/// <summary>
/// Interface for protocol adapter factories.
/// </summary>
public interface IProtocolAdapterFactory
{
    /// <summary>
    /// Gets a protocol adapter by name.
    /// </summary>
    /// <param name="protocolName">The name of the protocol.</param>
    /// <returns>The protocol adapter.</returns>
    IProtocolAdapter GetAdapter(string protocolName);
    
    /// <summary>
    /// Gets all available protocol adapters.
    /// </summary>
    /// <returns>The protocol adapters.</returns>
    IEnumerable<IProtocolAdapter> GetAllAdapters();
    
    /// <summary>
    /// Determines whether a protocol adapter with the specified name exists.
    /// </summary>
    /// <param name="protocolName">The name of the protocol.</param>
    /// <returns><c>true</c> if a protocol adapter with the specified name exists; otherwise, <c>false</c>.</returns>
    bool HasAdapter(string protocolName);
}
