namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Represents the security requirements of a protocol.
/// </summary>
public class SecurityRequirements
{
    /// <summary>
    /// Gets or sets whether authentication is required.
    /// </summary>
    public bool RequiresAuthentication { get; set; }
    
    /// <summary>
    /// Gets or sets the supported authentication methods.
    /// </summary>
    public IEnumerable<string> SupportedAuthenticationMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets whether encryption is required.
    /// </summary>
    public bool RequiresEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets the supported encryption methods.
    /// </summary>
    public IEnumerable<string> SupportedEncryptionMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets whether a secure connection is required.
    /// </summary>
    public bool RequiresSecureConnection { get; set; }
    
    /// <summary>
    /// Gets or sets whether certificate validation is required.
    /// </summary>
    public bool RequiresCertificateValidation { get; set; }
    
    /// <summary>
    /// Gets or sets whether client certificates are required.
    /// </summary>
    public bool RequiresClientCertificate { get; set; }
    
    /// <summary>
    /// Gets or sets whether IP restrictions are supported.
    /// </summary>
    public bool SupportsIpRestrictions { get; set; }
    
    /// <summary>
    /// Gets or sets whether rate limiting is supported.
    /// </summary>
    public bool SupportsRateLimiting { get; set; }
    
    /// <summary>
    /// Gets or sets the security level of the protocol.
    /// </summary>
    public SecurityLevel SecurityLevel { get; set; }
}
