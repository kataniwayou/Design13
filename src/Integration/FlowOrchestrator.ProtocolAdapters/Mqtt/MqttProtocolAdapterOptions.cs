namespace FlowOrchestrator.ProtocolAdapters.Mqtt;

/// <summary>
/// Options for the MQTT protocol adapter.
/// </summary>
public class MqttProtocolAdapterOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to use TLS.
    /// </summary>
    public bool UseTls { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the MQTT version.
    /// </summary>
    public string MqttVersion { get; set; } = "MQTT 5.0";
    
    /// <summary>
    /// Gets or sets the default topic.
    /// </summary>
    public string DefaultTopic { get; set; } = "default/topic";
    
    /// <summary>
    /// Gets or sets the default quality of service.
    /// </summary>
    public int DefaultQualityOfService { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets a value indicating whether to retain messages by default.
    /// </summary>
    public bool DefaultRetain { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public string ClientId { get; set; } = $"mqtt-client-{Guid.NewGuid()}";
    
    /// <summary>
    /// Gets or sets a value indicating whether to use clean session.
    /// </summary>
    public bool CleanSession { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the keep alive interval in seconds.
    /// </summary>
    public int KeepAliveSeconds { get; set; } = 60;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use authentication.
    /// </summary>
    public bool UseAuthentication { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password for authentication.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to use client certificate authentication.
    /// </summary>
    public bool UseClientCertificate { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the client certificate path.
    /// </summary>
    public string? ClientCertificatePath { get; set; }
    
    /// <summary>
    /// Gets or sets the client certificate password.
    /// </summary>
    public string? ClientCertificatePassword { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to verify server certificate.
    /// </summary>
    public bool VerifyServerCertificate { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the connection timeout in seconds.
    /// </summary>
    public int ConnectionTimeoutSeconds { get; set; } = 30;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use reconnection.
    /// </summary>
    public bool UseReconnection { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the reconnection interval in seconds.
    /// </summary>
    public int ReconnectionIntervalSeconds { get; set; } = 5;
    
    /// <summary>
    /// Gets or sets the maximum reconnection attempts.
    /// </summary>
    public int MaxReconnectionAttempts { get; set; } = 10;
    
    /// <summary>
    /// Gets or sets the will topic.
    /// </summary>
    public string? WillTopic { get; set; }
    
    /// <summary>
    /// Gets or sets the will message.
    /// </summary>
    public string? WillMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the will quality of service.
    /// </summary>
    public int WillQualityOfService { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets a value indicating whether to retain the will message.
    /// </summary>
    public bool WillRetain { get; set; } = false;
}
