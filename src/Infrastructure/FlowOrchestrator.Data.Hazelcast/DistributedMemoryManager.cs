using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Memory;
using Hazelcast.DistributedObjects;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Manages distributed memory using Hazelcast.
    /// </summary>
    public class DistributedMemoryManager
    {
        private readonly HazelcastDataStore _dataStore;
        private readonly IHMap<string, byte[]> _memoryMap;
        private readonly IHMap<string, MemoryAddressingInfo> _addressingMap;
        private readonly IHMap<string, string> _metadataMap;
        private readonly string _memoryMapName;
        private readonly string _addressingMapName;
        private readonly string _metadataMapName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedMemoryManager"/> class.
        /// </summary>
        /// <param name="dataStore">The Hazelcast data store.</param>
        /// <param name="memoryMapName">The name of the memory map.</param>
        /// <param name="addressingMapName">The name of the addressing map.</param>
        /// <param name="metadataMapName">The name of the metadata map.</param>
        public DistributedMemoryManager(
            HazelcastDataStore dataStore,
            string memoryMapName = "memory",
            string addressingMapName = "addressing",
            string metadataMapName = "metadata")
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

            if (string.IsNullOrEmpty(memoryMapName))
                throw new ArgumentException("Memory map name cannot be null or empty.", nameof(memoryMapName));

            if (string.IsNullOrEmpty(addressingMapName))
                throw new ArgumentException("Addressing map name cannot be null or empty.", nameof(addressingMapName));

            if (string.IsNullOrEmpty(metadataMapName))
                throw new ArgumentException("Metadata map name cannot be null or empty.", nameof(metadataMapName));

            _memoryMapName = memoryMapName;
            _addressingMapName = addressingMapName;
            _metadataMapName = metadataMapName;

            _memoryMap = _dataStore.GetMapAsync<string, byte[]>(_memoryMapName).GetAwaiter().GetResult();
            _addressingMap = _dataStore.GetMapAsync<string, MemoryAddressingInfo>(_addressingMapName).GetAwaiter().GetResult();
            _metadataMap = _dataStore.GetMapAsync<string, string>(_metadataMapName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Allocates memory with the specified address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <param name="size">The size of the memory to allocate.</param>
        /// <param name="metadata">Optional metadata for the memory allocation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AllocateMemoryAsync(string address, int size, Dictionary<string, string> metadata = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            if (size <= 0)
                throw new ArgumentException("Size must be greater than zero.", nameof(size));

            // Check if memory already exists
            if (await _memoryMap.ContainsKeyAsync(address))
                throw new InvalidOperationException($"Memory already allocated at address {address}.");

            // Allocate memory
            await _memoryMap.PutAsync(address, new byte[size]);

            // Create addressing information
            var addressing = new MemoryAddressingInfo
            {
                Address = address,
                Size = size,
                AllocationTime = DateTime.UtcNow,
                LastAccessTime = DateTime.UtcNow
            };

            await _addressingMap.PutAsync(address, addressing);

            // Store metadata if provided
            if (metadata != null && metadata.Count > 0)
            {
                foreach (var item in metadata)
                {
                    await _metadataMap.PutAsync($"{address}:{item.Key}", item.Value);
                }
            }
        }

        /// <summary>
        /// Frees memory at the specified address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task FreeMemoryAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            // Check if memory exists
            if (!await _memoryMap.ContainsKeyAsync(address))
                throw new InvalidOperationException($"No memory allocated at address {address}.");

            // Remove memory
            await _memoryMap.RemoveAsync(address);

            // Remove addressing information
            await _addressingMap.RemoveAsync(address);

            // Remove metadata
            var metadataKeys = new List<string>();
            foreach (var entry in await _metadataMap.GetEntriesAsync())
            {
                if (entry.Key.StartsWith($"{address}:"))
                {
                    metadataKeys.Add(entry.Key);
                }
            }

            foreach (var key in metadataKeys)
            {
                await _metadataMap.RemoveAsync(key);
            }
        }

        /// <summary>
        /// Writes data to memory at the specified address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="offset">The offset within the memory to start writing.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task WriteMemoryAsync(string address, byte[] data, int offset = 0)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (offset < 0)
                throw new ArgumentException("Offset cannot be negative.", nameof(offset));

            // Check if memory exists
            if (!await _memoryMap.ContainsKeyAsync(address))
                throw new InvalidOperationException($"No memory allocated at address {address}.");

            // Get current memory
            var memory = await _memoryMap.GetAsync(address);

            // Check if data fits
            if (offset + data.Length > memory.Length)
                throw new InvalidOperationException($"Data exceeds allocated memory size at address {address}.");

            // Write data
            Array.Copy(data, 0, memory, offset, data.Length);
            await _memoryMap.PutAsync(address, memory);

            // Update last access time
            var addressing = await _addressingMap.GetAsync(address);
            addressing.LastAccessTime = DateTime.UtcNow;
            await _addressingMap.PutAsync(address, addressing);
        }

        /// <summary>
        /// Reads data from memory at the specified address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <param name="offset">The offset within the memory to start reading.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The data read from memory.</returns>
        public async Task<byte[]> ReadMemoryAsync(string address, int offset = 0, int? length = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            if (offset < 0)
                throw new ArgumentException("Offset cannot be negative.", nameof(offset));

            // Check if memory exists
            if (!await _memoryMap.ContainsKeyAsync(address))
                throw new InvalidOperationException($"No memory allocated at address {address}.");

            // Get memory
            var memory = await _memoryMap.GetAsync(address);

            // Check if offset is valid
            if (offset >= memory.Length)
                throw new ArgumentException($"Offset {offset} is beyond the end of memory at address {address}.", nameof(offset));

            // Determine length to read
            int readLength = length ?? (memory.Length - offset);

            // Check if length is valid
            if (offset + readLength > memory.Length)
                throw new ArgumentException($"Cannot read {readLength} bytes from offset {offset} in memory at address {address}.", nameof(length));

            // Read data
            var result = new byte[readLength];
            Array.Copy(memory, offset, result, 0, readLength);

            // Update last access time
            var addressing = await _addressingMap.GetAsync(address);
            addressing.LastAccessTime = DateTime.UtcNow;
            await _addressingMap.PutAsync(address, addressing);

            return result;
        }

        /// <summary>
        /// Gets addressing information for memory at the specified address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <returns>The addressing information.</returns>
        public async Task<MemoryAddressingInfo> GetAddressingAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            // Check if memory exists
            if (!await _addressingMap.ContainsKeyAsync(address))
                throw new InvalidOperationException($"No memory allocated at address {address}.");

            return await _addressingMap.GetAsync(address);
        }

        /// <summary>
        /// Gets metadata for memory at the specified address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <returns>The metadata.</returns>
        public async Task<Dictionary<string, string>> GetMetadataAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            // Check if memory exists
            if (!await _memoryMap.ContainsKeyAsync(address))
                throw new InvalidOperationException($"No memory allocated at address {address}.");

            var metadata = new Dictionary<string, string>();

            foreach (var entry in await _metadataMap.GetEntriesAsync())
            {
                if (entry.Key.StartsWith($"{address}:"))
                {
                    string key = entry.Key.Substring(address.Length + 1);
                    metadata[key] = entry.Value;
                }
            }

            return metadata;
        }
    }
}
