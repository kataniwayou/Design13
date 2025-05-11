namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the encryption configuration.
/// </summary>
public class EncryptionConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether encryption is enabled.
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption method.
    /// </summary>
    public string Method { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the encryption key.
    /// </summary>
    public string? Key { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption IV.
    /// </summary>
    public string? IV { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption certificate path.
    /// </summary>
    public string? CertificatePath { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption certificate password.
    /// </summary>
    public string? CertificatePassword { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
