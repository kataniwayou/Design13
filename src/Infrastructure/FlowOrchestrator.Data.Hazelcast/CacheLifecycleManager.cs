using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Configuration;
using Hazelcast.DistributedObjects;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Manages the lifecycle of caches in the Hazelcast data store.
    /// </summary>
    public class CacheLifecycleManager
    {
        private readonly HazelcastDataStore _dataStore;
        private readonly IHMap<string, CacheMetadata> _cacheMetadataMap;
        private readonly ConfigurationParameters _configuration;
        private readonly string _cacheMetadataMapName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheLifecycleManager"/> class.
        /// </summary>
        /// <param name="dataStore">The Hazelcast data store.</param>
        /// <param name="configuration">The configuration parameters.</param>
        /// <param name="cacheMetadataMapName">The name of the cache metadata map.</param>
        public CacheLifecycleManager(
            HazelcastDataStore dataStore,
            ConfigurationParameters configuration,
            string cacheMetadataMapName = "cacheMetadata")
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrEmpty(cacheMetadataMapName))
                throw new ArgumentException("Cache metadata map name cannot be null or empty.", nameof(cacheMetadataMapName));

            _cacheMetadataMapName = cacheMetadataMapName;
            _cacheMetadataMap = _dataStore.GetMapAsync<string, CacheMetadata>(_cacheMetadataMapName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Creates a cache with the specified name.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the cache.</typeparam>
        /// <typeparam name="TValue">The type of values in the cache.</typeparam>
        /// <param name="cacheName">The name of the cache.</param>
        /// <param name="timeToLiveSeconds">The time-to-live for cache entries in seconds.</param>
        /// <param name="maxIdleSeconds">The maximum idle time for cache entries in seconds.</param>
        /// <param name="evictionPolicy">The eviction policy for the cache.</param>
        /// <returns>The created cache.</returns>
        public async Task<IHMap<TKey, TValue>> CreateCacheAsync<TKey, TValue>(
            string cacheName,
            int? timeToLiveSeconds = null,
            int? maxIdleSeconds = null,
            string evictionPolicy = "LRU")
        {
            if (string.IsNullOrEmpty(cacheName))
                throw new ArgumentException("Cache name cannot be null or empty.", nameof(cacheName));

            // Get default values from configuration if not provided
            if (timeToLiveSeconds == null)
            {
                timeToLiveSeconds = _configuration.GetParameter<int>("DefaultCacheTTLSeconds");
                if (timeToLiveSeconds == 0) timeToLiveSeconds = 300;
            }

            if (maxIdleSeconds == null)
            {
                maxIdleSeconds = _configuration.GetParameter<int>("DefaultCacheMaxIdleSeconds");
                if (maxIdleSeconds == 0) maxIdleSeconds = 60;
            }

            // Create cache
            var cache = await _dataStore.GetMapAsync<TKey, TValue>(cacheName);

            // Store cache metadata
            var metadata = new CacheMetadata
            {
                CacheName = cacheName,
                CreationTime = DateTime.UtcNow,
                LastAccessTime = DateTime.UtcNow,
                TimeToLiveSeconds = timeToLiveSeconds.Value,
                MaxIdleSeconds = maxIdleSeconds.Value,
                EvictionPolicy = evictionPolicy,
                KeyType = typeof(TKey).FullName,
                ValueType = typeof(TValue).FullName
            };

            await _cacheMetadataMap.PutAsync(cacheName, metadata);

            return cache;
        }

        /// <summary>
        /// Gets a cache with the specified name.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the cache.</typeparam>
        /// <typeparam name="TValue">The type of values in the cache.</typeparam>
        /// <param name="cacheName">The name of the cache.</param>
        /// <returns>The cache, or null if not found.</returns>
        public async Task<IHMap<TKey, TValue>> GetCacheAsync<TKey, TValue>(string cacheName)
        {
            if (string.IsNullOrEmpty(cacheName))
                throw new ArgumentException("Cache name cannot be null or empty.", nameof(cacheName));

            // Check if cache exists
            if (!await _cacheMetadataMap.ContainsKeyAsync(cacheName))
                return null;

            // Get cache metadata
            var metadata = await _cacheMetadataMap.GetAsync(cacheName);

            // Verify types
            if (metadata.KeyType != typeof(TKey).FullName || metadata.ValueType != typeof(TValue).FullName)
                throw new InvalidOperationException($"Cache {cacheName} has incompatible types. Expected {typeof(TKey).FullName}/{typeof(TValue).FullName}, found {metadata.KeyType}/{metadata.ValueType}.");

            // Update last access time
            metadata.LastAccessTime = DateTime.UtcNow;
            await _cacheMetadataMap.PutAsync(cacheName, metadata);

            // Get cache
            return await _dataStore.GetMapAsync<TKey, TValue>(cacheName);
        }

        /// <summary>
        /// Destroys a cache with the specified name.
        /// </summary>
        /// <param name="cacheName">The name of the cache.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DestroyCacheAsync(string cacheName)
        {
            if (string.IsNullOrEmpty(cacheName))
                throw new ArgumentException("Cache name cannot be null or empty.", nameof(cacheName));

            // Check if cache exists
            if (!await _cacheMetadataMap.ContainsKeyAsync(cacheName))
                return;

            // Get cache
            var cache = await _dataStore.GetClient().GetDistributedObjectsAsync();

            // Destroy cache
            foreach (var obj in cache)
            {
                if (obj.Name == cacheName && obj.ServiceName == "hz:impl:mapService")
                {
                    // Get the map and destroy it
                    var map = await _dataStore.GetMapAsync<object, object>(cacheName);
                    await map.DestroyAsync();
                    break;
                }
            }

            // Remove metadata
            await _cacheMetadataMap.RemoveAsync(cacheName);
        }

        /// <summary>
        /// Gets metadata for a cache with the specified name.
        /// </summary>
        /// <param name="cacheName">The name of the cache.</param>
        /// <returns>The cache metadata, or null if not found.</returns>
        public async Task<CacheMetadata> GetCacheMetadataAsync(string cacheName)
        {
            if (string.IsNullOrEmpty(cacheName))
                throw new ArgumentException("Cache name cannot be null or empty.", nameof(cacheName));

            return await _cacheMetadataMap.GetAsync(cacheName);
        }

        /// <summary>
        /// Gets all cache names.
        /// </summary>
        /// <returns>A list of cache names.</returns>
        public async Task<List<string>> GetAllCacheNamesAsync()
        {
            var cacheNames = new List<string>();

            foreach (var entry in await _cacheMetadataMap.GetEntriesAsync())
            {
                cacheNames.Add(entry.Key);
            }

            return cacheNames;
        }

        /// <summary>
        /// Cleans up expired caches.
        /// </summary>
        /// <param name="maxIdleTimeSeconds">The maximum idle time for caches in seconds.</param>
        /// <returns>The number of caches cleaned up.</returns>
        public async Task<int> CleanupExpiredCachesAsync(int maxIdleTimeSeconds = 3600)
        {
            var now = DateTime.UtcNow;
            var cachesToCleanup = new List<string>();

            foreach (var entry in await _cacheMetadataMap.GetEntriesAsync())
            {
                var metadata = entry.Value;
                var idleTime = (now - metadata.LastAccessTime).TotalSeconds;

                if (idleTime > maxIdleTimeSeconds)
                {
                    cachesToCleanup.Add(entry.Key);
                }
            }

            foreach (var cacheName in cachesToCleanup)
            {
                await DestroyCacheAsync(cacheName);
            }

            return cachesToCleanup.Count;
        }
    }

    /// <summary>
    /// Metadata for a cache.
    /// </summary>
    public class CacheMetadata
    {
        /// <summary>
        /// Gets or sets the name of the cache.
        /// </summary>
        public string CacheName { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the cache.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the last access time of the cache.
        /// </summary>
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// Gets or sets the time-to-live for cache entries in seconds.
        /// </summary>
        public int TimeToLiveSeconds { get; set; }

        /// <summary>
        /// Gets or sets the maximum idle time for cache entries in seconds.
        /// </summary>
        public int MaxIdleSeconds { get; set; }

        /// <summary>
        /// Gets or sets the eviction policy for the cache.
        /// </summary>
        public string EvictionPolicy { get; set; }

        /// <summary>
        /// Gets or sets the type of keys in the cache.
        /// </summary>
        public string KeyType { get; set; }

        /// <summary>
        /// Gets or sets the type of values in the cache.
        /// </summary>
        public string ValueType { get; set; }
    }
}
