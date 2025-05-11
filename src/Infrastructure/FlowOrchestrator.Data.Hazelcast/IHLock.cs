using System;
using System.Threading.Tasks;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Interface for a distributed lock in Hazelcast.
    /// </summary>
    public interface IHLock
    {
        /// <summary>
        /// Gets the name of the lock.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Acquires the lock.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task LockAsync();
        
        /// <summary>
        /// Tries to acquire the lock within the specified timeout.
        /// </summary>
        /// <param name="timeoutMilliseconds">The timeout in milliseconds.</param>
        /// <returns>True if the lock was acquired, false otherwise.</returns>
        Task<bool> TryLockAsync(int timeoutMilliseconds);
        
        /// <summary>
        /// Releases the lock.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UnlockAsync();
        
        /// <summary>
        /// Checks if the lock is locked.
        /// </summary>
        /// <returns>True if the lock is locked, false otherwise.</returns>
        Task<bool> IsLockedAsync();
    }
}
