using System;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Represents memory addressing information in the distributed memory system.
    /// </summary>
    public class MemoryAddressingInfo
    {
        /// <summary>
        /// Gets or sets the memory address.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the size of the memory allocation.
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// Gets or sets the time when the memory was allocated.
        /// </summary>
        public DateTime AllocationTime { get; set; }
        
        /// <summary>
        /// Gets or sets the time when the memory was last accessed.
        /// </summary>
        public DateTime LastAccessTime { get; set; }
    }
}
