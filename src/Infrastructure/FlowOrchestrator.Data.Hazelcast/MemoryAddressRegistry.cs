using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Memory;
using Hazelcast.DistributedObjects;

namespace FlowOrchestrator.Data.Hazelcast
{
    /// <summary>
    /// Registry for memory addresses in the distributed memory system.
    /// </summary>
    public class MemoryAddressRegistry
    {
        private readonly HazelcastDataStore _dataStore;
        private readonly IHMap<string, string> _addressRegistry;
        private readonly IHMap<string, List<string>> _flowAddressMap;
        private readonly IHMap<string, List<string>> _branchAddressMap;
        private readonly string _addressRegistryName;
        private readonly string _flowAddressMapName;
        private readonly string _branchAddressMapName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryAddressRegistry"/> class.
        /// </summary>
        /// <param name="dataStore">The Hazelcast data store.</param>
        /// <param name="addressRegistryName">The name of the address registry map.</param>
        /// <param name="flowAddressMapName">The name of the flow address map.</param>
        /// <param name="branchAddressMapName">The name of the branch address map.</param>
        public MemoryAddressRegistry(
            HazelcastDataStore dataStore,
            string addressRegistryName = "addressRegistry",
            string flowAddressMapName = "flowAddresses",
            string branchAddressMapName = "branchAddresses")
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

            if (string.IsNullOrEmpty(addressRegistryName))
                throw new ArgumentException("Address registry name cannot be null or empty.", nameof(addressRegistryName));

            if (string.IsNullOrEmpty(flowAddressMapName))
                throw new ArgumentException("Flow address map name cannot be null or empty.", nameof(flowAddressMapName));

            if (string.IsNullOrEmpty(branchAddressMapName))
                throw new ArgumentException("Branch address map name cannot be null or empty.", nameof(branchAddressMapName));

            _addressRegistryName = addressRegistryName;
            _flowAddressMapName = flowAddressMapName;
            _branchAddressMapName = branchAddressMapName;

            _addressRegistry = _dataStore.GetMapAsync<string, string>(_addressRegistryName).GetAwaiter().GetResult();
            _flowAddressMap = _dataStore.GetMapAsync<string, List<string>>(_flowAddressMapName).GetAwaiter().GetResult();
            _branchAddressMap = _dataStore.GetMapAsync<string, List<string>>(_branchAddressMapName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Registers a memory address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <param name="flowId">The ID of the flow that owns the memory.</param>
        /// <param name="branchId">The ID of the branch that owns the memory, if applicable.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RegisterAddressAsync(string address, string flowId, string branchId = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            // Register address
            await _addressRegistry.PutAsync(address, flowId + (branchId != null ? $":{branchId}" : ""));

            // Add to flow address map
            var flowAddresses = await _flowAddressMap.GetAsync(flowId);
            if (flowAddresses == null)
                flowAddresses = new List<string>();
            if (!flowAddresses.Contains(address))
            {
                flowAddresses.Add(address);
                await _flowAddressMap.PutAsync(flowId, flowAddresses);
            }

            // Add to branch address map if applicable
            if (!string.IsNullOrEmpty(branchId))
            {
                var branchKey = $"{flowId}:{branchId}";
                var branchAddresses = await _branchAddressMap.GetAsync(branchKey);
                if (branchAddresses == null)
                    branchAddresses = new List<string>();
                if (!branchAddresses.Contains(address))
                {
                    branchAddresses.Add(address);
                    await _branchAddressMap.PutAsync(branchKey, branchAddresses);
                }
            }
        }

        /// <summary>
        /// Unregisters a memory address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UnregisterAddressAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            // Get flow and branch IDs
            var ownership = await _addressRegistry.GetAsync(address);
            if (ownership == null)
                return; // Address not registered

            string flowId;
            string branchId = null;

            if (ownership.Contains(':'))
            {
                var parts = ownership.Split(':');
                flowId = parts[0];
                branchId = parts[1];
            }
            else
            {
                flowId = ownership;
            }

            // Remove from address registry
            await _addressRegistry.RemoveAsync(address);

            // Remove from flow address map
            var flowAddresses = await _flowAddressMap.GetAsync(flowId);
            if (flowAddresses != null)
            {
                flowAddresses.Remove(address);
                if (flowAddresses.Count > 0)
                    await _flowAddressMap.PutAsync(flowId, flowAddresses);
                else
                    await _flowAddressMap.RemoveAsync(flowId);
            }

            // Remove from branch address map if applicable
            if (!string.IsNullOrEmpty(branchId))
            {
                var branchKey = $"{flowId}:{branchId}";
                var branchAddresses = await _branchAddressMap.GetAsync(branchKey);
                if (branchAddresses != null)
                {
                    branchAddresses.Remove(address);
                    if (branchAddresses.Count > 0)
                        await _branchAddressMap.PutAsync(branchKey, branchAddresses);
                    else
                        await _branchAddressMap.RemoveAsync(branchKey);
                }
            }
        }

        /// <summary>
        /// Gets the flow ID that owns the specified memory address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <returns>The flow ID, or null if the address is not registered.</returns>
        public async Task<string> GetFlowIdAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            var ownership = await _addressRegistry.GetAsync(address);
            if (ownership == null)
                return null;

            if (ownership.Contains(':'))
                return ownership.Split(':')[0];

            return ownership;
        }

        /// <summary>
        /// Gets the branch ID that owns the specified memory address.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <returns>The branch ID, or null if the address is not owned by a branch.</returns>
        public async Task<string> GetBranchIdAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));

            var ownership = await _addressRegistry.GetAsync(address);
            if (ownership == null || !ownership.Contains(':'))
                return null;

            return ownership.Split(':')[1];
        }

        /// <summary>
        /// Gets all memory addresses owned by the specified flow.
        /// </summary>
        /// <param name="flowId">The flow ID.</param>
        /// <returns>A list of memory addresses.</returns>
        public async Task<List<string>> GetFlowAddressesAsync(string flowId)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            return await _flowAddressMap.GetAsync(flowId) ?? new List<string>();
        }

        /// <summary>
        /// Gets all memory addresses owned by the specified branch.
        /// </summary>
        /// <param name="flowId">The flow ID.</param>
        /// <param name="branchId">The branch ID.</param>
        /// <returns>A list of memory addresses.</returns>
        public async Task<List<string>> GetBranchAddressesAsync(string flowId, string branchId)
        {
            if (string.IsNullOrEmpty(flowId))
                throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));

            if (string.IsNullOrEmpty(branchId))
                throw new ArgumentException("Branch ID cannot be null or empty.", nameof(branchId));

            var branchKey = $"{flowId}:{branchId}";
            return await _branchAddressMap.GetAsync(branchKey) ?? new List<string>();
        }
    }
}
