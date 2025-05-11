namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a package of data that flows through the system.
/// </summary>
public class DataPackage : BaseEntity
{
    /// <summary>
    /// Gets or sets the memory address where the data is stored.
    /// </summary>
    public string MemoryAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data format.
    /// </summary>
    public string DataFormat { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the schema of the data.
    /// </summary>
    public string Schema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the size of the data in bytes.
    /// </summary>
    public long SizeInBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the number of records in the data.
    /// </summary>
    public int RecordCount { get; set; }
    
    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreationTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the expiration timestamp.
    /// </summary>
    public DateTime? ExpirationTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the source step ID that produced this data.
    /// </summary>
    public string? SourceStepId { get; set; }
    
    /// <summary>
    /// Gets or sets the source branch path that produced this data.
    /// </summary>
    public string? SourceBranchPath { get; set; }
    
    /// <summary>
    /// Gets or sets the execution context ID associated with this data.
    /// </summary>
    public string? ExecutionContextId { get; set; }
    
    /// <summary>
    /// Gets or sets the metadata associated with this data.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the tags associated with this data.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Creates a new instance of the DataPackage class.
    /// </summary>
    public DataPackage()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DataPackage class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the data package.</param>
    /// <param name="name">The name of the data package.</param>
    /// <param name="description">The description of the data package.</param>
    public DataPackage(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the DataPackage class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the data package.</param>
    /// <param name="name">The name of the data package.</param>
    /// <param name="description">The description of the data package.</param>
    /// <param name="memoryAddress">The memory address where the data is stored.</param>
    /// <param name="dataFormat">The data format.</param>
    /// <param name="schema">The schema of the data.</param>
    /// <param name="sizeInBytes">The size of the data in bytes.</param>
    /// <param name="recordCount">The number of records in the data.</param>
    /// <param name="creationTimestamp">The creation timestamp.</param>
    /// <param name="sourceStepId">The source step ID that produced this data.</param>
    /// <param name="sourceBranchPath">The source branch path that produced this data.</param>
    /// <param name="executionContextId">The execution context ID associated with this data.</param>
    public DataPackage(
        string id,
        string name,
        string description,
        string memoryAddress,
        string dataFormat,
        string schema,
        long sizeInBytes,
        int recordCount,
        DateTime creationTimestamp,
        string? sourceStepId,
        string? sourceBranchPath,
        string? executionContextId)
        : base(id, name, description)
    {
        MemoryAddress = memoryAddress;
        DataFormat = dataFormat;
        Schema = schema;
        SizeInBytes = sizeInBytes;
        RecordCount = recordCount;
        CreationTimestamp = creationTimestamp;
        SourceStepId = sourceStepId;
        SourceBranchPath = sourceBranchPath;
        ExecutionContextId = executionContextId;
    }
    
    /// <summary>
    /// Adds a metadata entry to the data package.
    /// </summary>
    /// <param name="key">The metadata key.</param>
    /// <param name="value">The metadata value.</param>
    public void AddMetadata(string key, string value)
    {
        Metadata[key] = value;
    }
    
    /// <summary>
    /// Adds a tag to the data package.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag))
        {
            Tags.Add(tag);
        }
    }
    
    /// <summary>
    /// Sets the expiration timestamp for the data package.
    /// </summary>
    /// <param name="expirationTimestamp">The expiration timestamp.</param>
    public void SetExpirationTimestamp(DateTime expirationTimestamp)
    {
        ExpirationTimestamp = expirationTimestamp;
    }
    
    /// <summary>
    /// Checks if the data package has expired.
    /// </summary>
    /// <returns>True if the data package has expired, false otherwise.</returns>
    public bool HasExpired()
    {
        return ExpirationTimestamp.HasValue && DateTime.UtcNow >= ExpirationTimestamp.Value;
    }
}
