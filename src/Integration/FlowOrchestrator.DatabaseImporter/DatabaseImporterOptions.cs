namespace FlowOrchestrator.DatabaseImporter;

/// <summary>
/// Options for the database importer.
/// </summary>
public class DatabaseImporterOptions
{
    /// <summary>
    /// Gets or sets the connection string for the database.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the provider name for the database.
    /// </summary>
    public string ProviderName { get; set; } = "Microsoft.Data.SqlClient";
    
    /// <summary>
    /// Gets or sets the command timeout in seconds.
    /// </summary>
    public int CommandTimeoutSeconds { get; set; } = 30;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use transactions.
    /// </summary>
    public bool UseTransactions { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the isolation level for transactions.
    /// </summary>
    public string IsolationLevel { get; set; } = "ReadCommitted";
    
    /// <summary>
    /// Gets or sets a value indicating whether to use connection pooling.
    /// </summary>
    public bool UseConnectionPooling { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the minimum pool size.
    /// </summary>
    public int MinPoolSize { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets the maximum pool size.
    /// </summary>
    public int MaxPoolSize { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use encryption.
    /// </summary>
    public bool UseEncryption { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to trust the server certificate.
    /// </summary>
    public bool TrustServerCertificate { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use integrated security.
    /// </summary>
    public bool UseIntegratedSecurity { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the username for SQL authentication.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password for SQL authentication.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets the batch size for bulk operations.
    /// </summary>
    public int BatchSize { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use schema discovery.
    /// </summary>
    public bool UseSchemaDiscovery { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use parameter discovery.
    /// </summary>
    public bool UseParameterDiscovery { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use prepared commands.
    /// </summary>
    public bool UsePreparedCommands { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use multiple active result sets.
    /// </summary>
    public bool UseMultipleActiveResultSets { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use column encryption.
    /// </summary>
    public bool UseColumnEncryption { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use retry logic.
    /// </summary>
    public bool UseRetryLogic { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the delay between retry attempts in milliseconds.
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;
}
