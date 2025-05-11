namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Interface for protocol handlers that handle communication with external systems.
/// </summary>
public interface IProtocolHandler : IDisposable
{
    /// <summary>
    /// Gets the protocol that this handler implements.
    /// </summary>
    IProtocol Protocol { get; }
    
    /// <summary>
    /// Gets the configuration for this handler.
    /// </summary>
    string Configuration { get; }
    
    /// <summary>
    /// Gets the connection status of this handler.
    /// </summary>
    ConnectionStatus ConnectionStatus { get; }
    
    /// <summary>
    /// Opens a connection to the external system.
    /// </summary>
    /// <returns>True if the connection was opened successfully, false otherwise.</returns>
    Task<bool> OpenConnectionAsync();
    
    /// <summary>
    /// Closes the connection to the external system.
    /// </summary>
    /// <returns>True if the connection was closed successfully, false otherwise.</returns>
    Task<bool> CloseConnectionAsync();
    
    /// <summary>
    /// Tests the connection to the external system.
    /// </summary>
    /// <returns>True if the connection test was successful, false otherwise.</returns>
    Task<bool> TestConnectionAsync();
    
    /// <summary>
    /// Reads data from the external system.
    /// </summary>
    /// <param name="resource">The resource to read from.</param>
    /// <param name="options">The options for the read operation.</param>
    /// <returns>The result of the read operation.</returns>
    Task<ProtocolOperationResult> ReadAsync(string resource, string options);
    
    /// <summary>
    /// Writes data to the external system.
    /// </summary>
    /// <param name="resource">The resource to write to.</param>
    /// <param name="data">The data to write.</param>
    /// <param name="options">The options for the write operation.</param>
    /// <returns>The result of the write operation.</returns>
    Task<ProtocolOperationResult> WriteAsync(string resource, byte[] data, string options);
    
    /// <summary>
    /// Deletes a resource from the external system.
    /// </summary>
    /// <param name="resource">The resource to delete.</param>
    /// <param name="options">The options for the delete operation.</param>
    /// <returns>The result of the delete operation.</returns>
    Task<ProtocolOperationResult> DeleteAsync(string resource, string options);
    
    /// <summary>
    /// Lists resources from the external system.
    /// </summary>
    /// <param name="path">The path to list resources from.</param>
    /// <param name="options">The options for the list operation.</param>
    /// <returns>The result of the list operation.</returns>
    Task<ProtocolOperationResult> ListAsync(string path, string options);
    
    /// <summary>
    /// Executes a query on the external system.
    /// </summary>
    /// <param name="query">The query to execute.</param>
    /// <param name="options">The options for the query operation.</param>
    /// <returns>The result of the query operation.</returns>
    Task<ProtocolOperationResult> QueryAsync(string query, string options);
}
