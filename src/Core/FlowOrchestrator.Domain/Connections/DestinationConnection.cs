using System;
using System.Collections.Generic;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Domain.Connections;

/// <summary>
/// Represents a connection to a destination system.
/// </summary>
public class DestinationConnection
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
    /// Gets or sets the destination ID.
    /// </summary>
    public string DestinationId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the destination type.
    /// </summary>
    public string DestinationType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the protocol used to connect to the destination.
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
    /// Creates a new instance of the DestinationConnection class.
    /// </summary>
    public DestinationConnection()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DestinationConnection class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the connection.</param>
    /// <param name="name">The name of the connection.</param>
    /// <param name="description">The description of the connection.</param>
    /// <param name="destinationId">The destination ID.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="protocol">The protocol used to connect to the destination.</param>
    public DestinationConnection(string id, string name, string description, string destinationId, string destinationType, string protocol)
    {
        Id = id;
        Name = name;
        Description = description;
        DestinationId = destinationId;
        DestinationType = destinationType;
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
        
        if (string.IsNullOrEmpty(DestinationId))
        {
            result.AddError("DestinationId is required.");
        }
        
        if (string.IsNullOrEmpty(DestinationType))
        {
            result.AddError("DestinationType is required.");
        }
        
        if (string.IsNullOrEmpty(Protocol))
        {
            result.AddError("Protocol is required.");
        }
        
        return result;
    }
}
