namespace FlowOrchestrator.MessageQueueExporter;

/// <summary>
/// Options for the message queue exporter.
/// </summary>
public class MessageQueueExporterOptions
{
    /// <summary>
    /// Gets or sets the connection string for the message queue.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the provider name for the message queue.
    /// </summary>
    public string ProviderName { get; set; } = "RabbitMQ";
    
    /// <summary>
    /// Gets or sets the default queue name.
    /// </summary>
    public string DefaultQueueName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the default exchange.
    /// </summary>
    public string DefaultExchange { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the maximum batch size.
    /// </summary>
    public int MaxBatchSize { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use durable queues.
    /// </summary>
    public bool UseDurableQueues { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use exclusive queues.
    /// </summary>
    public bool UseExclusiveQueues { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use auto-delete queues.
    /// </summary>
    public bool UseAutoDeleteQueues { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use persistent messages.
    /// </summary>
    public bool UsePersistentMessages { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use transactions.
    /// </summary>
    public bool UseTransactions { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use publisher confirms.
    /// </summary>
    public bool UsePublisherConfirms { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use SSL/TLS.
    /// </summary>
    public bool UseSsl { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to verify SSL certificates.
    /// </summary>
    public bool VerifySslCertificate { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the SSL certificate path.
    /// </summary>
    public string? SslCertificatePath { get; set; }
    
    /// <summary>
    /// Gets or sets the SSL certificate password.
    /// </summary>
    public string? SslCertificatePassword { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use authentication.
    /// </summary>
    public bool UseAuthentication { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password for authentication.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use virtual hosts.
    /// </summary>
    public bool UseVirtualHost { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the virtual host.
    /// </summary>
    public string? VirtualHost { get; set; }
    
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
    
    /// <summary>
    /// Gets or sets a value indicating whether to declare queues before publishing.
    /// </summary>
    public bool DeclareQueuesBeforePublishing { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to declare exchanges before publishing.
    /// </summary>
    public bool DeclareExchangesBeforePublishing { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to bind queues to exchanges before publishing.
    /// </summary>
    public bool BindQueuesBeforePublishing { get; set; } = true;
}
