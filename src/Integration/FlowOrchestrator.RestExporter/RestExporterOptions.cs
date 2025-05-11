namespace FlowOrchestrator.RestExporter;

/// <summary>
/// Options for the REST exporter.
/// </summary>
public class RestExporterOptions
{
    /// <summary>
    /// Gets or sets the base URL for REST API operations.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the default HTTP method to use.
    /// </summary>
    public string DefaultMethod { get; set; } = "POST";
    
    /// <summary>
    /// Gets or sets the default headers to include in all requests.
    /// </summary>
    public Dictionary<string, string> DefaultHeaders { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the timeout for HTTP requests in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use SSL/TLS.
    /// </summary>
    public bool UseSsl { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate SSL certificates.
    /// </summary>
    public bool ValidateSslCertificate { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use compression.
    /// </summary>
    public bool UseCompression { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to follow redirects.
    /// </summary>
    public bool FollowRedirects { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the maximum number of redirects to follow.
    /// </summary>
    public int MaxRedirects { get; set; } = 10;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use authentication.
    /// </summary>
    public bool UseAuthentication { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the authentication method to use.
    /// </summary>
    public string AuthenticationMethod { get; set; } = "none";
    
    /// <summary>
    /// Gets or sets the username for basic authentication.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password for basic authentication.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets the bearer token for bearer authentication.
    /// </summary>
    public string? BearerToken { get; set; }
    
    /// <summary>
    /// Gets or sets the API key for API key authentication.
    /// </summary>
    public string? ApiKey { get; set; }
    
    /// <summary>
    /// Gets or sets the API key header name for API key authentication.
    /// </summary>
    public string ApiKeyHeaderName { get; set; } = "X-API-Key";
    
    /// <summary>
    /// Gets or sets a value indicating whether to use a proxy.
    /// </summary>
    public bool UseProxy { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the proxy URL.
    /// </summary>
    public string? ProxyUrl { get; set; }
    
    /// <summary>
    /// Gets or sets the proxy username.
    /// </summary>
    public string? ProxyUsername { get; set; }
    
    /// <summary>
    /// Gets or sets the proxy password.
    /// </summary>
    public string? ProxyPassword { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to retry failed requests.
    /// </summary>
    public bool RetryOnFailure { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the delay between retry attempts in milliseconds.
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;
}
