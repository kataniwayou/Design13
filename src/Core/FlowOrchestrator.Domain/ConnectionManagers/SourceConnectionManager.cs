using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Domain.Connections;
using FlowOrchestrator.Common;
using FlowOrchestrator.Domain.Utilities;

namespace FlowOrchestrator.Domain.ConnectionManagers;

/// <summary>
/// Manager service for source connections.
/// </summary>
public class SourceConnectionManager : AbstractServiceBase, IConnectionManager<SourceConnection, string>, IConnectionManager
{
    private readonly Dictionary<string, SourceConnection> _connections = new();
    private readonly object _lock = new();

    /// <summary>
    /// Gets the manager type that this manager implements.
    /// </summary>
    public string ManagerType => "SourceConnectionManager";

    /// <summary>
    /// Creates a new instance of the SourceConnectionManager class.
    /// </summary>
    /// <param name="id">The unique identifier for the service.</param>
    /// <param name="name">The name of the service.</param>
    /// <param name="description">The description of the service.</param>
    /// <param name="version">The version of the service.</param>
    public SourceConnectionManager(string id, string name, string description, string version)
        : base(id, name, description, version)
    {
    }

    /// <summary>
    /// Creates a new connection.
    /// </summary>
    /// <param name="parameters">The connection parameters.</param>
    /// <returns>The created connection.</returns>
    public Task<SourceConnection> CreateConnectionAsync(Dictionary<string, string> parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        if (!parameters.TryGetValue("id", out var id))
        {
            id = Guid.NewGuid().ToString();
        }

        if (!parameters.TryGetValue("name", out var name))
        {
            throw new ArgumentException("Name is required", nameof(parameters));
        }

        if (!parameters.TryGetValue("sourceId", out var sourceId))
        {
            throw new ArgumentException("SourceId is required", nameof(parameters));
        }

        if (!parameters.TryGetValue("sourceType", out var sourceType))
        {
            throw new ArgumentException("SourceType is required", nameof(parameters));
        }

        if (!parameters.TryGetValue("protocol", out var protocol))
        {
            throw new ArgumentException("Protocol is required", nameof(parameters));
        }

        parameters.TryGetValue("description", out var description);
        description ??= string.Empty;

        var connection = new SourceConnection(id, name, description, sourceId, sourceType, protocol);

        // Add connection parameters
        foreach (var param in parameters)
        {
            if (param.Key.StartsWith("connection."))
            {
                connection.ConnectionParameters[param.Key.Substring(11)] = param.Value;
            }
            else if (param.Key.StartsWith("auth."))
            {
                connection.AuthenticationParameters[param.Key.Substring(5)] = param.Value;
            }
            else if (param.Key.StartsWith("metadata."))
            {
                connection.Metadata[param.Key.Substring(9)] = param.Value;
            }
        }

        lock (_lock)
        {
            _connections[id] = connection;
        }

        return Task.FromResult(connection);
    }

    /// <summary>
    /// Closes a connection.
    /// </summary>
    /// <param name="connection">The connection to close.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task CloseConnectionAsync(SourceConnection connection)
    {
        if (connection == null)
        {
            throw new ArgumentNullException(nameof(connection));
        }

        lock (_lock)
        {
            if (_connections.TryGetValue(connection.Id, out var existingConnection))
            {
                existingConnection.Status.State = ConnectionState.Closed;
                existingConnection.Status.LastDisconnectionTime = DateTime.UtcNow;
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the status of a connection.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    /// <returns>The connection status.</returns>
    public Task<Abstractions.Services.ConnectionStatusInfo> GetConnectionStatusAsync(string connectionId)
    {
        lock (_lock)
        {
            if (_connections.TryGetValue(connectionId, out var connection))
            {
                // Convert from Common.ConnectionStatus to Abstractions.Services.ConnectionStatusInfo
                var status = connection.Status.State switch
                {
                    ConnectionState.Open => Abstractions.Services.ConnectionStatus.Connected,
                    ConnectionState.Closed => Abstractions.Services.ConnectionStatus.Disconnected,
                    ConnectionState.Connecting => Abstractions.Services.ConnectionStatus.Connecting,
                    ConnectionState.Disconnecting => Abstractions.Services.ConnectionStatus.Disconnecting,
                    ConnectionState.Error => Abstractions.Services.ConnectionStatus.Failed,
                    _ => Abstractions.Services.ConnectionStatus.Unknown
                };

                return Task.FromResult(new Abstractions.Services.ConnectionStatusInfo
                {
                    IsConnected = connection.Status.State == ConnectionState.Open,
                    LastError = connection.Status.LastErrorMessage,
                    LastUpdated = DateTime.UtcNow,
                    Status = status
                });
            }

            return Task.FromResult(new Abstractions.Services.ConnectionStatusInfo
            {
                IsConnected = false,
                LastError = "Connection not found",
                LastUpdated = DateTime.UtcNow,
                Status = Abstractions.Services.ConnectionStatus.Unknown
            });
        }
    }

    /// <summary>
    /// Validates connection parameters.
    /// </summary>
    /// <param name="parameters">The connection parameters to validate.</param>
    /// <returns>The validation result.</returns>
    public Task<Abstractions.Services.ValidationResult> ValidateConnectionAsync(Dictionary<string, string> parameters)
    {
        var commonResult = new Common.ValidationResult();

        if (parameters == null)
        {
            commonResult.AddError("Parameters cannot be null");
            return Task.FromResult(commonResult.ToAbstractionsValidationResult());
        }

        if (!parameters.ContainsKey("name"))
        {
            commonResult.AddError("Name is required");
        }

        if (!parameters.ContainsKey("sourceId"))
        {
            commonResult.AddError("SourceId is required");
        }

        if (!parameters.ContainsKey("sourceType"))
        {
            commonResult.AddError("SourceType is required");
        }

        if (!parameters.ContainsKey("protocol"))
        {
            commonResult.AddError("Protocol is required");
        }

        return Task.FromResult(commonResult.ToAbstractionsValidationResult());
    }

    /// <summary>
    /// Gets a connection by ID.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    /// <returns>The connection, or default if not found.</returns>
    public Task<SourceConnection?> GetConnectionAsync(string connectionId)
    {
        lock (_lock)
        {
            _connections.TryGetValue(connectionId, out var connection);
            return Task.FromResult(connection);
        }
    }

    /// <summary>
    /// Gets all connections.
    /// </summary>
    /// <returns>A list of all connections.</returns>
    public Task<IEnumerable<SourceConnection>> GetAllConnectionsAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<SourceConnection>>(_connections.Values.ToList());
        }
    }

    /// <summary>
    /// Tests a connection.
    /// </summary>
    /// <param name="parameters">The connection parameters.</param>
    /// <returns>The test result.</returns>
    public Task<ConnectionTestResult> TestConnectionAsync(Dictionary<string, string> parameters)
    {
        // In a real implementation, this would test the connection to the source system
        return Task.FromResult(ConnectionTestResult.Success(parameters, 10));
    }

    /// <summary>
    /// Opens the connection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task OpenAsync(CancellationToken cancellationToken = default)
    {
        // In a real implementation, this would open the connection to the source system
        return Task.CompletedTask;
    }

    /// <summary>
    /// Closes the connection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task CloseAsync(CancellationToken cancellationToken = default)
    {
        // In a real implementation, this would close the connection to the source system
        return Task.CompletedTask;
    }

    /// <summary>
    /// Tests the connection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        // In a real implementation, this would test the connection to the source system
        return Task.CompletedTask;
    }
}
