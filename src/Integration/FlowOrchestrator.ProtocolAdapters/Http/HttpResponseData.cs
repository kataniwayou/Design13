namespace FlowOrchestrator.ProtocolAdapters.Http;

/// <summary>
/// Represents HTTP response data.
/// </summary>
public class HttpResponseData
{
    /// <summary>
    /// Gets or sets the status code.
    /// </summary>
    public int StatusCode { get; set; } = 200;
    
    /// <summary>
    /// Gets or sets the status message.
    /// </summary>
    public string StatusMessage { get; set; } = "OK";
    
    /// <summary>
    /// Gets or sets the headers.
    /// </summary>
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the body.
    /// </summary>
    public string Body { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the cookies.
    /// </summary>
    public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the HTTP version.
    /// </summary>
    public string HttpVersion { get; set; } = "HTTP/1.1";
    
    /// <summary>
    /// Gets or sets the timestamp of the response.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the duration of the request in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
}
