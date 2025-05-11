using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Configuration;
using Hazelcast.DistributedObjects;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Provides isolation for branches in the distributed memory system.
    /// </summary>
    public class BranchIsolationProvider
    {
        private readonly HazelcastDataStore _dataStore;
        private readonly IHMap<string, BranchIsolationInfo> _isolationMap;
        private readonly IHMap<string, List<string>> _branchLockMap;
        private readonly string _isolationMapName;
        private readonly string _branchLockMapName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BranchIsolationProvider"/> class.
        /// </summary>
        /// <param name="dataStore">The Hazelcast data store.</param>
        /// <param name="isolationMapName">The name of the isolation map.</param>
        /// <param name="branchLockMapName">The name of the branch lock map.</param>
        public BranchIsolationProvider(
            HazelcastDataStore dataStore,
            string isolationMapName = "branchIsolation",
            string branchLockMapName = "branchLocks")
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

            if (string.IsNullOrEmpty(isolationMapName))
                throw new ArgumentException("Isolation map name cannot be null or empty.", nameof(isolationMapName));

            if (string.IsNullOrEmpty(branchLockMapName))
                throw new ArgumentException("Branch lock map name cannot be null or empty.", nameof(branchLockMapName));

            _isolationMapName = isolationMapName;
            _branchLockMapName = branchLockMapName;

            _isolationMap = _dataStore.GetMapAsync<string, BranchIsolationInfo>(_isolationMapName).GetAwaiter().GetResult();
            _branchLockMap = _dataStore.GetMapAsync<string, List<string>>(_branchLockMapName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Creates an isolation context for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateIsolationContextAsync(string flowId, string branchId, BranchIsolationLevel isolationLevel)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            var branchKey = $"{flowId}:{branchId}";

            // Check if isolation context already exists
            if (await _isolationMap.ContainsKeyAsync(branchKey))
                throw new InvalidOperationException($"Isolation context already exists for branch {branchKey}.");

            // Create isolation context
            var isolationInfo = new BranchIsolationInfo
            {
                FlowId = flowId,
                BranchId = branchId,
                IsolationLevel = isolationLevel,
                CreationTime = DateTime.UtcNow,
                LastAccessTime = DateTime.UtcNow
            };

            await _isolationMap.PutAsync(branchKey, isolationInfo);
        }

        /// <summary>
        /// Destroys the isolation context for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DestroyIsolationContextAsync(string flowId, string branchId)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            var branchKey = $"{flowId}:{branchId}";

            // Check if isolation context exists
            if (!await _isolationMap.ContainsKeyAsync(branchKey))
                return;

            // Release all locks
            await ReleaseAllLocksAsync(flowId, branchId);

            // Remove isolation context
            await _isolationMap.RemoveAsync(branchKey);
        }

        /// <summary>
        /// Gets the isolation level for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The isolation level, or null if no isolation context exists.</returns>
        public async Task<BranchIsolationLevel?> GetIsolationLevelAsync(string flowId, string branchId)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            var branchKey = $"{flowId}:{branchId}";

            var isolationInfo = await _isolationMap.GetAsync(branchKey);
            if (isolationInfo == null)
                return null;

            // Update last access time
            isolationInfo.LastAccessTime = DateTime.UtcNow;
            await _isolationMap.PutAsync(branchKey, isolationInfo);

            return isolationInfo.IsolationLevel;
        }

        /// <summary>
        /// Acquires a lock on a memory address for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <param name="address">The memory address to lock.</param>
        /// <param name="lockMode">The lock mode.</param>
        /// <param name="timeoutMilliseconds">The timeout in milliseconds.</param>
        /// <returns>True if the lock was acquired, false otherwise.</returns>
        public async Task<bool> AcquireLockAsync(string flowId, string branchId, string address, BranchLockMode lockMode, int timeoutMilliseconds = 5000)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            var branchKey = $"{flowId}:{branchId}";

            // Check if isolation context exists
            if (!await _isolationMap.ContainsKeyAsync(branchKey))
                throw new InvalidOperationException($"No isolation context exists for branch {branchKey}.");

            // Get a lock on the address
            var lockObj = await _dataStore.GetLockAsync($"lock:{address}");

            // Try to acquire the lock
            bool acquired = await lockObj.TryLockAsync(timeoutMilliseconds);
            if (!acquired)
                return false;

            try
            {
                // Add to branch lock map
                var locks = await _branchLockMap.GetAsync(branchKey) ?? new List<string>();
                if (!locks.Contains(address))
                {
                    locks.Add(address);
                    await _branchLockMap.PutAsync(branchKey, locks);
                }

                return true;
            }
            catch
            {
                // Release the lock if an error occurs
                await lockObj.UnlockAsync();
                throw;
            }
        }

        /// <summary>
        /// Releases a lock on a memory address for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <param name="address">The memory address to unlock.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ReleaseLockAsync(string flowId, string branchId, string address)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            var branchKey = $"{flowId}:{branchId}";

            // Get a lock on the address
            var lockObj = await _dataStore.GetLockAsync($"lock:{address}");

            // Release the lock
            await lockObj.UnlockAsync();

            // Remove from branch lock map
            var locks = await _branchLockMap.GetAsync(branchKey);
            if (locks != null)
            {
                locks.Remove(address);
                if (locks.Count > 0)
                    await _branchLockMap.PutAsync(branchKey, locks);
                else
                    await _branchLockMap.RemoveAsync(branchKey);
            }
        }

        /// <summary>
        /// Releases all locks for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ReleaseAllLocksAsync(string flowId, string branchId)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            var branchKey = $"{flowId}:{branchId}";

            // Get all locks for the branch
            var locks = await _branchLockMap.GetAsync(branchKey);
            if (locks == null || locks.Count == 0)
                return;

            // Release each lock
            foreach (var address in locks)
            {
                var lockObj = await _dataStore.GetLockAsync($"lock:{address}");
                await lockObj.UnlockAsync();
            }

            // Clear branch lock map
            await _branchLockMap.RemoveAsync(branchKey);
        }

        /// <summary>
        /// Gets all locked addresses for a branch.
        /// </summary>
        /// <param name="flowId">The ID of the flow.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>A list of locked addresses.</returns>
        public async Task<List<string>> GetLockedAddressesAsync(string flowId, string branchId)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            var branchKey = $"{flowId}:{branchId}";

            return await _branchLockMap.GetAsync(branchKey) ?? new List<string>();
        }
    }

    /// <summary>
    /// Information about branch isolation.
    /// </summary>
    public class BranchIsolationInfo
    {
        /// <summary>
        /// Gets or sets the ID of the flow.
        /// </summary>
        public string FlowId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the branch.
        /// </summary>
        public string BranchId { get; set; }

        /// <summary>
        /// Gets or sets the isolation level.
        /// </summary>
        public BranchIsolationLevel IsolationLevel { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the isolation context.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the last access time of the isolation context.
        /// </summary>
        public DateTime LastAccessTime { get; set; }
    }

    /// <summary>
    /// Isolation level for branches.
    /// </summary>
    public enum BranchIsolationLevel
    {
        /// <summary>
        /// No isolation.
        /// </summary>
        None,

        /// <summary>
        /// Read committed isolation.
        /// </summary>
        ReadCommitted,

        /// <summary>
        /// Repeatable read isolation.
        /// </summary>
        RepeatableRead,

        /// <summary>
        /// Serializable isolation.
        /// </summary>
        Serializable
    }

    /// <summary>
    /// Lock mode for branches.
    /// </summary>
    public enum BranchLockMode
    {
        /// <summary>
        /// Shared lock.
        /// </summary>
        Shared,

        /// <summary>
        /// Exclusive lock.
        /// </summary>
        Exclusive
    }
}
