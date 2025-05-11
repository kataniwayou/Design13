using System;
using System.Threading.Tasks;
using Hazelcast.CP;
using Hazelcast.Core;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Implementation of IHLock that wraps a Hazelcast FencedLock.
    /// </summary>
    public class HazelcastLock : IHLock
    {
        private readonly IFencedLock _fencedLock;

        /// <summary>
        /// Gets the name of the lock.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelcastLock"/> class.
        /// </summary>
        /// <param name="fencedLock">The fenced lock to wrap.</param>
        /// <param name="name">The name of the lock.</param>
        public HazelcastLock(IFencedLock fencedLock, string name)
        {
            _fencedLock = fencedLock ?? throw new ArgumentNullException(nameof(fencedLock));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Acquires the lock.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LockAsync()
        {
            await _fencedLock.LockAsync(new LockContext());
        }

        /// <summary>
        /// Tries to acquire the lock within the specified timeout.
        /// </summary>
        /// <param name="timeoutMilliseconds">The timeout in milliseconds.</param>
        /// <returns>True if the lock was acquired, false otherwise.</returns>
        public async Task<bool> TryLockAsync(int timeoutMilliseconds)
        {
            var lockContext = new LockContext();
            // Use the timeout directly in the method call instead of setting it on the context
            return await _fencedLock.TryLockAsync(lockContext, TimeSpan.FromMilliseconds(timeoutMilliseconds));
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UnlockAsync()
        {
            await _fencedLock.UnlockAsync(new LockContext());
        }

        /// <summary>
        /// Checks if the lock is locked.
        /// </summary>
        /// <returns>True if the lock is locked, false otherwise.</returns>
        public async Task<bool> IsLockedAsync()
        {
            return await _fencedLock.IsLockedAsync(new LockContext());
        }
    }
}
