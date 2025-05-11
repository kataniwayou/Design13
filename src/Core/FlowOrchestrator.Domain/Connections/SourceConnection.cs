using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Domain.Connections;

/// <summary>
/// Represents a connection to a source system.
/// </summary>
public class SourceConnection
{
    /// <summary>
    /// Gets or sets the unique identifier for this connection.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the name of this connection.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of this connection.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the source ID.
    /// </summary>
    public string SourceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the source type.
    /// </summary>
    public string SourceType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the protocol used to connect to the source.
    /// </summary>
    public string Protocol { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the connection parameters.
    /// </summary>
    public Dictionary<string, string> ConnectionParameters { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the authentication parameters.
    /// </summary>
    public Dictionary<string, string> AuthenticationParameters { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the connection status.
    /// </summary>
    public ConnectionStatus Status { get; set; } = new ConnectionStatus();
    
    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the last modified timestamp.
    /// </summary>
    public DateTime LastModifiedTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the metadata associated with this connection.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Creates a new instance of the SourceConnection class.
    /// </summary>
    public SourceConnection()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the SourceConnection class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the connection.</param>
    /// <param name="name">The name of the connection.</param>
    /// <param name="description">The description of the connection.</param>
    /// <param name="sourceId">The source ID.</param>
    /// <param name="sourceType">The source type.</param>
    /// <param name="protocol">The protocol used to connect to the source.</param>
    public SourceConnection(string id, string name, string description, string sourceId, string sourceType, string protocol)
    {
        Id = id;
        Name = name;
        Description = description;
        SourceId = sourceId;
        SourceType = sourceType;
        Protocol = protocol;
        Status.ConnectionId = id;
    }
    
    /// <summary>
    /// Validates the connection.
    /// </summary>
    /// <returns>The validation result.</returns>
    public ValidationResult Validate()
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrEmpty(Id))
        {
            result.AddError("Id is required.");
        }
        
        if (string.IsNullOrEmpty(Name))
        {
            result.AddError("Name is required.");
        }
        
        if (string.IsNullOrEmpty(SourceId))
        {
            result.AddError("SourceId is required.");
        }
        
        if (string.IsNullOrEmpty(SourceType))
        {
            result.AddError("SourceType is required.");
        }
        
        if (string.IsNullOrEmpty(Protocol))
        {
            result.AddError("Protocol is required.");
        }
        
        return result;
    }
}
