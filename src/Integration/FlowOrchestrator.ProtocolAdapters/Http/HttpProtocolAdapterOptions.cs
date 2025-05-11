namespace FlowOrchestrator.ProtocolAdapters.Http;

/// <summary>
/// Options for the HTTP protocol adapter.
/// </summary>
public class HttpProtocolAdapterOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to use HTTPS.
    /// </summary>
    public bool UseHttps { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the HTTP version.
    /// </summary>
    public string HttpVersion { get; set; } = "HTTP/1.1";
    
    /// <summary>
    /// Gets or sets the default content type.
    /// </summary>
    public string DefaultContentType { get; set; } = "application/json";
    
    /// <summary>
    /// Gets or sets the default character set.
    /// </summary>
    public string DefaultCharacterSet { get; set; } = "utf-8";
    
    /// <summary>
    /// Gets or sets a value indicating whether to use compression.
    /// </summary>
    public bool UseCompression { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the default compression method.
    /// </summary>
    public string DefaultCompressionMethod { get; set; } = "gzip";
    
    /// <summary>
    /// Gets or sets a value indicating whether to use authentication.
    /// </summary>
    public bool UseAuthentication { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the default authentication method.
    /// </summary>
    public string DefaultAuthenticationMethod { get; set; } = "Bearer";
    
    /// <summary>
    /// Gets or sets the authentication token.
    /// </summary>
    public string? AuthenticationToken { get; set; }
    
    /// <summary>
    /// Gets or sets the username for basic authentication.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password for basic authentication.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string? ApiKey { get; set; }
    
    /// <summary>
    /// Gets or sets the API key header name.
    /// </summary>
    public string ApiKeyHeaderName { get; set; } = "X-API-Key";
    
    /// <summary>
    /// Gets or sets a value indicating whether to follow redirects.
    /// </summary>
    public bool FollowRedirects { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the maximum number of redirects to follow.
    /// </summary>
    public int MaxRedirects { get; set; } = 10;
    
    /// <summary>
    /// Gets or sets the timeout in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use keep-alive.
    /// </summary>
    public bool UseKeepAlive { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the default headers.
    /// </summary>
    public Dictionary<string, string> DefaultHeaders { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the default query parameters.
    /// </summary>
    public Dictionary<string, string> DefaultQueryParameters { get; set; } = new Dictionary<string, string>();
}
