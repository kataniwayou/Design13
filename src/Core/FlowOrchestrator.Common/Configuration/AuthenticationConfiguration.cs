namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the authentication configuration.
/// </summary>
public class AuthenticationConfiguration
{
    /// <summary>
    /// Gets or sets the authentication method.
    /// </summary>
    public string Method { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string? ApiKey { get; set; }
    
    /// <summary>
    /// Gets or sets the API secret.
    /// </summary>
    public string? ApiSecret { get; set; }
    
    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    public string? Token { get; set; }
    
    /// <summary>
    /// Gets or sets the token expiration.
    /// </summary>
    public DateTime? TokenExpiration { get; set; }
    
    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public string? ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the client secret.
    /// </summary>
    public string? ClientSecret { get; set; }
    
    /// <summary>
    /// Gets or sets the tenant ID.
    /// </summary>
    public string? TenantId { get; set; }
    
    /// <summary>
    /// Gets or sets the certificate path.
    /// </summary>
    public string? CertificatePath { get; set; }
    
    /// <summary>
    /// Gets or sets the certificate password.
    /// </summary>
    public string? CertificatePassword { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
