namespace FlowOrchestrator.ProtocolAdapters.Http;

/// <summary>
/// Represents HTTP request data.
/// </summary>
public class HttpRequestData
{
    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
    public string Method { get; set; } = "GET";
    
    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the headers.
    /// </summary>
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the query parameters.
    /// </summary>
    public Dictionary<string, string> QueryParameters { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the body.
    /// </summary>
    public string Body { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the cookies.
    /// </summary>
    public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the form data.
    /// </summary>
    public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the multipart form data.
    /// </summary>
    public Dictionary<string, object> MultipartFormData { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Gets or sets the HTTP version.
    /// </summary>
    public string HttpVersion { get; set; } = "HTTP/1.1";
    
    /// <summary>
    /// Gets or sets the client IP address.
    /// </summary>
    public string ClientIpAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the client port.
    /// </summary>
    public int ClientPort { get; set; }
    
    /// <summary>
    /// Gets or sets the server IP address.
    /// </summary>
    public string ServerIpAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the server port.
    /// </summary>
    public int ServerPort { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the request is secure.
    /// </summary>
    public bool IsSecure { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp of the request.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
