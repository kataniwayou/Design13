namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a query for discovering transformation rules.
/// </summary>
public class RuleDiscoveryQuery
{
    /// <summary>
    /// Gets or sets the rule type to search for.
    /// </summary>
    public string? RuleType { get; set; }
    
    /// <summary>
    /// Gets or sets the input data type to search for.
    /// </summary>
    public string? InputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the output data type to search for.
    /// </summary>
    public string? OutputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the rule language to search for.
    /// </summary>
    public string? RuleLanguage { get; set; }
    
    /// <summary>
    /// Gets or sets the author to search for.
    /// </summary>
    public string? Author { get; set; }
    
    /// <summary>
    /// Gets or sets the version to search for.
    /// </summary>
    public string? Version { get; set; }
    
    /// <summary>
    /// Gets or sets the creation date range to search for.
    /// </summary>
    public TimeRange? CreationDateRange { get; set; }
    
    /// <summary>
    /// Gets or sets the last modified date range to search for.
    /// </summary>
    public TimeRange? LastModifiedDateRange { get; set; }
    
    /// <summary>
    /// Gets or sets the keywords to search for.
    /// </summary>
    public List<string> Keywords { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the maximum number of results to return.
    /// </summary>
    public int MaxResults { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets the sort field.
    /// </summary>
    public string SortField { get; set; } = "LastModifiedDate";
    
    /// <summary>
    /// Gets or sets a value indicating whether to sort in ascending order.
    /// </summary>
    public bool SortAscending { get; set; }
    
    /// <summary>
    /// Gets or sets the additional query parameters.
    /// </summary>
    public Dictionary<string, object> AdditionalParameters { get; set; } = new Dictionary<string, object>();
}
