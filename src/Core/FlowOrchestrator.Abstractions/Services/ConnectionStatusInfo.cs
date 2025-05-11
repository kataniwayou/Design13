using System;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents detailed information about a connection status.
/// </summary>
public class ConnectionStatusInfo
{
    /// <summary>
    /// Gets or sets whether the connection is connected.
    /// </summary>
    public bool IsConnected { get; set; }
    
    /// <summary>
    /// Gets or sets the last error message.
    /// </summary>
    public string? LastError { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the status was last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    /// Gets or sets the connection status.
    /// </summary>
    public ConnectionStatus Status { get; set; } = ConnectionStatus.Unknown;
    
    /// <summary>
    /// Gets or sets additional details about the connection.
    /// </summary>
    public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();
}
