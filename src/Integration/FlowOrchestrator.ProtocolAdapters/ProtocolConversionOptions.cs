namespace FlowOrchestrator.ProtocolAdapters;

/// <summary>
/// Options for protocol conversion.
/// </summary>
public class ProtocolConversionOptions
{
    /// <summary>
    /// Gets or sets the source format.
    /// </summary>
    public string SourceFormat { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the target format.
    /// </summary>
    public string TargetFormat { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether to preserve the original structure.
    /// </summary>
    public bool PreserveStructure { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to include metadata.
    /// </summary>
    public bool IncludeMetadata { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the data.
    /// </summary>
    public bool ValidateData { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use compression.
    /// </summary>
    public bool UseCompression { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the compression method to use.
    /// </summary>
    public string CompressionMethod { get; set; } = "none";
    
    /// <summary>
    /// Gets or sets a value indicating whether to use encryption.
    /// </summary>
    public bool UseEncryption { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the encryption method to use.
    /// </summary>
    public string EncryptionMethod { get; set; } = "none";
    
    /// <summary>
    /// Gets or sets the encryption key.
    /// </summary>
    public string? EncryptionKey { get; set; }
    
    /// <summary>
    /// Gets or sets the character encoding to use.
    /// </summary>
    public string CharacterEncoding { get; set; } = "utf-8";
    
    /// <summary>
    /// Gets or sets the date format to use.
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-ddTHH:mm:ss.fffZ";
    
    /// <summary>
    /// Gets or sets the number format to use.
    /// </summary>
    public string NumberFormat { get; set; } = "0.####";
    
    /// <summary>
    /// Gets or sets the culture to use.
    /// </summary>
    public string Culture { get; set; } = "en-US";
    
    /// <summary>
    /// Gets or sets the additional options.
    /// </summary>
    public Dictionary<string, object> AdditionalOptions { get; set; } = new Dictionary<string, object>();
}
