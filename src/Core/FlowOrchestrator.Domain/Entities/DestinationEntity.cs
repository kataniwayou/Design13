using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a data destination in the system.
/// </summary>
public class DestinationEntity : VersionedEntity
{
    /// <summary>
    /// Gets or sets the destination type.
    /// </summary>
    public string DestinationType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the protocol used to connect to the destination.
    /// </summary>
    public string Protocol { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the connection string or configuration for the destination.
    /// </summary>
    public string ConnectionConfiguration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the authentication configuration for the destination.
    /// </summary>
    public string AuthenticationConfiguration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the schema of the data expected by this destination.
    /// </summary>
    public string Schema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data format expected by this destination.
    /// </summary>
    public string DataFormat { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the exporter service ID that can export to this destination.
    /// </summary>
    public string ExporterServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the metadata associated with this destination.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Creates a new instance of the DestinationEntity class.
    /// </summary>
    public DestinationEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DestinationEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the destination.</param>
    /// <param name="name">The name of the destination.</param>
    /// <param name="description">The description of the destination.</param>
    public DestinationEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DestinationEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the destination.</param>
    /// <param name="name">The name of the destination.</param>
    /// <param name="description">The description of the destination.</param>
    /// <param name="version">The semantic version number.</param>
    /// <param name="versionDescription">The human-readable description of this version.</param>
    /// <param name="previousVersionId">The reference to the previous version (if applicable).</param>
    /// <param name="versionStatus">The status of this version.</param>
    public DestinationEntity(
        string id,
        string name,
        string description,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus)
        : base(id, name, description, version, versionDescription, previousVersionId, versionStatus)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DestinationEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the destination.</param>
    /// <param name="name">The name of the destination.</param>
    /// <param name="description">The description of the destination.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="protocol">The protocol used to connect to the destination.</param>
    /// <param name="connectionConfiguration">The connection string or configuration for the destination.</param>
    /// <param name="authenticationConfiguration">The authentication configuration for the destination.</param>
    /// <param name="schema">The schema of the data expected by this destination.</param>
    /// <param name="dataFormat">The data format expected by this destination.</param>
    /// <param name="exporterServiceId">The exporter service ID that can export to this destination.</param>
    public DestinationEntity(
        string id,
        string name,
        string description,
        string destinationType,
        string protocol,
        string connectionConfiguration,
        string authenticationConfiguration,
        string schema,
        string dataFormat,
        string exporterServiceId)
        : base(id, name, description)
    {
        DestinationType = destinationType;
        Protocol = protocol;
        ConnectionConfiguration = connectionConfiguration;
        AuthenticationConfiguration = authenticationConfiguration;
        Schema = schema;
        DataFormat = dataFormat;
        ExporterServiceId = exporterServiceId;
    }
}
