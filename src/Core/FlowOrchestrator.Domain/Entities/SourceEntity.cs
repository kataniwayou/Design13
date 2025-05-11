using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a data source in the system.
/// </summary>
public class SourceEntity : VersionedEntity
{
    /// <summary>
    /// Gets or sets the source type.
    /// </summary>
    public string SourceType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the protocol used to connect to the source.
    /// </summary>
    public string Protocol { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the connection string or configuration for the source.
    /// </summary>
    public string ConnectionConfiguration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the authentication configuration for the source.
    /// </summary>
    public string AuthenticationConfiguration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the schema of the data provided by this source.
    /// </summary>
    public string Schema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data format provided by this source.
    /// </summary>
    public string DataFormat { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the importer service ID that can import from this source.
    /// </summary>
    public string ImporterServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the metadata associated with this source.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Creates a new instance of the SourceEntity class.
    /// </summary>
    public SourceEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the SourceEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the source.</param>
    /// <param name="name">The name of the source.</param>
    /// <param name="description">The description of the source.</param>
    public SourceEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the SourceEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the source.</param>
    /// <param name="name">The name of the source.</param>
    /// <param name="description">The description of the source.</param>
    /// <param name="version">The semantic version number.</param>
    /// <param name="versionDescription">The human-readable description of this version.</param>
    /// <param name="previousVersionId">The reference to the previous version (if applicable).</param>
    /// <param name="versionStatus">The status of this version.</param>
    public SourceEntity(
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
    /// Creates a new instance of the SourceEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the source.</param>
    /// <param name="name">The name of the source.</param>
    /// <param name="description">The description of the source.</param>
    /// <param name="sourceType">The source type.</param>
    /// <param name="protocol">The protocol used to connect to the source.</param>
    /// <param name="connectionConfiguration">The connection string or configuration for the source.</param>
    /// <param name="authenticationConfiguration">The authentication configuration for the source.</param>
    /// <param name="schema">The schema of the data provided by this source.</param>
    /// <param name="dataFormat">The data format provided by this source.</param>
    /// <param name="importerServiceId">The importer service ID that can import from this source.</param>
    public SourceEntity(
        string id,
        string name,
        string description,
        string sourceType,
        string protocol,
        string connectionConfiguration,
        string authenticationConfiguration,
        string schema,
        string dataFormat,
        string importerServiceId)
        : base(id, name, description)
    {
        SourceType = sourceType;
        Protocol = protocol;
        ConnectionConfiguration = connectionConfiguration;
        AuthenticationConfiguration = authenticationConfiguration;
        Schema = schema;
        DataFormat = dataFormat;
        ImporterServiceId = importerServiceId;
    }
}
