namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents the schema of a data source or destination.
/// </summary>
public class DataSchema
{
    /// <summary>
    /// Gets or sets the name of the schema.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the schema.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the version of the schema.
    /// </summary>
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Gets or sets the tables in the schema.
    /// </summary>
    public List<DataTable> Tables { get; set; } = new List<DataTable>();
    
    /// <summary>
    /// Gets or sets the relationships in the schema.
    /// </summary>
    public List<DataRelationship> Relationships { get; set; } = new List<DataRelationship>();
    
    /// <summary>
    /// Gets or sets the additional properties of the schema.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a table in a data schema.
/// </summary>
public class DataTable
{
    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the table.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the columns in the table.
    /// </summary>
    public List<DataColumn> Columns { get; set; } = new List<DataColumn>();
    
    /// <summary>
    /// Gets or sets the primary key columns of the table.
    /// </summary>
    public List<string> PrimaryKey { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the indexes of the table.
    /// </summary>
    public List<DataIndex> Indexes { get; set; } = new List<DataIndex>();
    
    /// <summary>
    /// Gets or sets the additional properties of the table.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a column in a data table.
/// </summary>
public class DataColumn
{
    /// <summary>
    /// Gets or sets the name of the column.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the column.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data type of the column.
    /// </summary>
    public string DataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the maximum length of the column.
    /// </summary>
    public int? MaxLength { get; set; }
    
    /// <summary>
    /// Gets or sets the precision of the column.
    /// </summary>
    public int? Precision { get; set; }
    
    /// <summary>
    /// Gets or sets the scale of the column.
    /// </summary>
    public int? Scale { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the column is nullable.
    /// </summary>
    public bool IsNullable { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the column is an identity column.
    /// </summary>
    public bool IsIdentity { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the column is computed.
    /// </summary>
    public bool IsComputed { get; set; }
    
    /// <summary>
    /// Gets or sets the default value of the column.
    /// </summary>
    public string? DefaultValue { get; set; }
    
    /// <summary>
    /// Gets or sets the additional properties of the column.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents an index in a data table.
/// </summary>
public class DataIndex
{
    /// <summary>
    /// Gets or sets the name of the index.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the index.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the columns in the index.
    /// </summary>
    public List<string> Columns { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets a value indicating whether the index is unique.
    /// </summary>
    public bool IsUnique { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the index is clustered.
    /// </summary>
    public bool IsClustered { get; set; }
    
    /// <summary>
    /// Gets or sets the additional properties of the index.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a relationship between tables in a data schema.
/// </summary>
public class DataRelationship
{
    /// <summary>
    /// Gets or sets the name of the relationship.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the relationship.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the parent table of the relationship.
    /// </summary>
    public string ParentTable { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the parent columns of the relationship.
    /// </summary>
    public List<string> ParentColumns { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the child table of the relationship.
    /// </summary>
    public string ChildTable { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the child columns of the relationship.
    /// </summary>
    public List<string> ChildColumns { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the update rule of the relationship.
    /// </summary>
    public string UpdateRule { get; set; } = "NoAction";
    
    /// <summary>
    /// Gets or sets the delete rule of the relationship.
    /// </summary>
    public string DeleteRule { get; set; } = "NoAction";
    
    /// <summary>
    /// Gets or sets the additional properties of the relationship.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}
