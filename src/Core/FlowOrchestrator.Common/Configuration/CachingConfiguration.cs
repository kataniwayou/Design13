namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for caching.
/// </summary>
public class CachingConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether caching is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the cache provider.
    /// </summary>
    public string CacheProvider { get; set; } = "Memory";
    
    /// <summary>
    /// Gets or sets the cache key prefix.
    /// </summary>
    public string CacheKeyPrefix { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the cache expiration in seconds.
    /// </summary>
    public int CacheExpirationSeconds { get; set; } = 3600;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use sliding expiration.
    /// </summary>
    public bool UseSlidingExpiration { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the maximum cache size in megabytes.
    /// </summary>
    public int MaxCacheSizeMB { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets the cache eviction policy.
    /// </summary>
    public string CacheEvictionPolicy { get; set; } = "LRU";
    
    /// <summary>
    /// Gets or sets a value indicating whether to compress cached data.
    /// </summary>
    public bool CompressCachedData { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to encrypt cached data.
    /// </summary>
    public bool EncryptCachedData { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the additional parameters for caching.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
