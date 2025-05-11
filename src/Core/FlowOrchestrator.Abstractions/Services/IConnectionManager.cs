using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Interface for connection managers.
/// </summary>
public interface IConnectionManager
{
    /// <summary>
    /// Opens the connection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task OpenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes the connection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CloseAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests the connection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task TestConnectionAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Generic interface for connection managers.
/// </summary>
/// <typeparam name="TConnection">The type of connection.</typeparam>
/// <typeparam name="TConnectionId">The type of connection ID.</typeparam>
public interface IConnectionManager<TConnection, TConnectionId> : IConnectionManager
{
    /// <summary>
    /// Creates a new connection.
    /// </summary>
    /// <param name="parameters">The connection parameters.</param>
    /// <returns>The created connection.</returns>
    Task<TConnection> CreateConnectionAsync(Dictionary<string, string> parameters);

    /// <summary>
    /// Closes a connection.
    /// </summary>
    /// <param name="connection">The connection to close.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CloseConnectionAsync(TConnection connection);

    /// <summary>
    /// Gets the status of a connection.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    /// <returns>The connection status.</returns>
    Task<ConnectionStatusInfo> GetConnectionStatusAsync(TConnectionId connectionId);

    /// <summary>
    /// Validates connection parameters.
    /// </summary>
    /// <param name="parameters">The connection parameters to validate.</param>
    /// <returns>The validation result.</returns>
    Task<ValidationResult> ValidateConnectionAsync(Dictionary<string, string> parameters);

    /// <summary>
    /// Gets a connection by ID.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    /// <returns>The connection, or default if not found.</returns>
    Task<TConnection?> GetConnectionAsync(TConnectionId connectionId);

    /// <summary>
    /// Gets all connections.
    /// </summary>
    /// <returns>A list of all connections.</returns>
    Task<IEnumerable<TConnection>> GetAllConnectionsAsync();

    /// <summary>
    /// Tests a connection with specific parameters.
    /// </summary>
    /// <param name="parameters">The connection parameters.</param>
    /// <returns>The test result.</returns>
    Task<ConnectionTestResult> TestConnectionAsync(Dictionary<string, string> parameters);
}
