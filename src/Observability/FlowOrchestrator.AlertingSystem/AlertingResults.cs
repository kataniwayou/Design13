namespace FlowOrchestrator.AlertingSystem;

/// <summary>
/// Represents an alert creation result
/// </summary>
public class AlertCreationResult
{
    /// <summary>
    /// Whether the creation was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Alert ID
    /// </summary>
    public string AlertId { get; set; } = string.Empty;
    
    /// <summary>
    /// Creation timestamp
    /// </summary>
    public DateTime CreationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Created alert
    /// </summary>
    public Alert Alert { get; set; } = new Alert();
    
    /// <summary>
    /// Error message if creation failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents an alert update result
/// </summary>
public class AlertUpdateResult
{
    /// <summary>
    /// Whether the update was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Alert ID
    /// </summary>
    public string AlertId { get; set; } = string.Empty;
    
    /// <summary>
    /// Update timestamp
    /// </summary>
    public DateTime UpdateTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Applied update
    /// </summary>
    public AlertUpdate Update { get; set; } = new AlertUpdate();
    
    /// <summary>
    /// Error message if update failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents an alert acknowledgement result
/// </summary>
public class AlertAcknowledgementResult
{
    /// <summary>
    /// Whether the acknowledgement was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Alert ID
    /// </summary>
    public string AlertId { get; set; } = string.Empty;
    
    /// <summary>
    /// Acknowledgement timestamp
    /// </summary>
    public DateTime AcknowledgementTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Applied acknowledgement
    /// </summary>
    public AlertAcknowledgement Acknowledgement { get; set; } = new AlertAcknowledgement();
    
    /// <summary>
    /// Error message if acknowledgement failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents an alert resolution result
/// </summary>
public class AlertResolutionResult
{
    /// <summary>
    /// Whether the resolution was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Alert ID
    /// </summary>
    public string AlertId { get; set; } = string.Empty;
    
    /// <summary>
    /// Resolution timestamp
    /// </summary>
    public DateTime ResolutionTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Applied resolution
    /// </summary>
    public AlertResolution Resolution { get; set; } = new AlertResolution();
    
    /// <summary>
    /// Error message if resolution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents an alert query result
/// </summary>
public class AlertQueryResult
{
    /// <summary>
    /// Whether the query was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Query timestamp
    /// </summary>
    public DateTime QueryTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Total count of alerts matching the query
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Alerts matching the query
    /// </summary>
    public List<Alert> Alerts { get; set; } = new List<Alert>();
    
    /// <summary>
    /// Error message if query failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents an alerting configuration result
/// </summary>
public class AlertingConfigurationResult
{
    /// <summary>
    /// Whether the configuration was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Configuration timestamp
    /// </summary>
    public DateTime ConfigurationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Applied configuration
    /// </summary>
    public AlertingConfiguration AppliedConfiguration { get; set; } = new AlertingConfiguration();
    
    /// <summary>
    /// Error message if configuration failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}
