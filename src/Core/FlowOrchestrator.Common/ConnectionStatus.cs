using System;

namespace FlowOrchestrator.Common;

/// <summary>
/// Represents the status of a connection.
/// </summary>
public class ConnectionStatus
{
    /// <summary>
    /// Gets or sets the connection ID.
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the connection state.
    /// </summary>
    public ConnectionState State { get; set; } = ConnectionState.Closed;
    
    /// <summary>
    /// Gets or sets the last error message.
    /// </summary>
    public string? LastErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the last connection time.
    /// </summary>
    public DateTime? LastConnectionTime { get; set; }
    
    /// <summary>
    /// Gets or sets the last disconnection time.
    /// </summary>
    public DateTime? LastDisconnectionTime { get; set; }
    
    /// <summary>
    /// Gets or sets the connection statistics.
    /// </summary>
    public ConnectionStatistics Statistics { get; set; } = new ConnectionStatistics();
}

/// <summary>
/// Represents the state of a connection.
/// </summary>
public enum ConnectionState
{
    /// <summary>
    /// The connection is closed.
    /// </summary>
    Closed,
    
    /// <summary>
    /// The connection is open.
    /// </summary>
    Open,
    
    /// <summary>
    /// The connection is connecting.
    /// </summary>
    Connecting,
    
    /// <summary>
    /// The connection is disconnecting.
    /// </summary>
    Disconnecting,
    
    /// <summary>
    /// The connection is in an error state.
    /// </summary>
    Error,
    
    /// <summary>
    /// The connection state is unknown.
    /// </summary>
    Unknown
}

/// <summary>
/// Represents statistics for a connection.
/// </summary>
public class ConnectionStatistics
{
    /// <summary>
    /// Gets or sets the number of successful connections.
    /// </summary>
    public int SuccessfulConnections { get; set; }
    
    /// <summary>
    /// Gets or sets the number of failed connections.
    /// </summary>
    public int FailedConnections { get; set; }
    
    /// <summary>
    /// Gets or sets the number of bytes sent.
    /// </summary>
    public long BytesSent { get; set; }
    
    /// <summary>
    /// Gets or sets the number of bytes received.
    /// </summary>
    public long BytesReceived { get; set; }
    
    /// <summary>
    /// Gets or sets the total connection time in seconds.
    /// </summary>
    public double TotalConnectionTimeSeconds { get; set; }
}
