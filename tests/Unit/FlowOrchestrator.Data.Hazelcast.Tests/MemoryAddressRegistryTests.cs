using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Data.Hazelcast;
using Moq;
using Xunit;

namespace FlowOrchestrator.Data.Hazelcast.Tests
{
    public class MemoryAddressRegistryTests
    {
        private readonly Mock<HazelcastDataStore> _mockDataStore;
        private readonly Mock<IHMap<string, List<string>>> _mockFlowAddressMap;
        private readonly Mock<IHMap<string, List<string>>> _mockBranchAddressMap;
        private readonly Mock<IHMap<string, string>> _mockAddressOwnerMap;
        private readonly MemoryAddressRegistry _registry;

        public MemoryAddressRegistryTests()
        {
            _mockDataStore = new Mock<HazelcastDataStore>();
            _mockFlowAddressMap = new Mock<IHMap<string, List<string>>>();
            _mockBranchAddressMap = new Mock<IHMap<string, List<string>>>();
            _mockAddressOwnerMap = new Mock<IHMap<string, string>>();

            _mockDataStore.Setup(ds => ds.GetMapAsync<string, List<string>>("FlowAddressMap"))
                .ReturnsAsync(_mockFlowAddressMap.Object);
            _mockDataStore.Setup(ds => ds.GetMapAsync<string, List<string>>("BranchAddressMap"))
                .ReturnsAsync(_mockBranchAddressMap.Object);
            _mockDataStore.Setup(ds => ds.GetMapAsync<string, string>("AddressOwnerMap"))
                .ReturnsAsync(_mockAddressOwnerMap.Object);

            _registry = new MemoryAddressRegistry(_mockDataStore.Object);
        }

        [Fact]
        public async Task RegisterAddressAsync_WithValidParameters_RegistersAddress()
        {
            // Arrange
            var address = "memory:0x12345";
            var flowId = "flow-123";
            var branchId = "branch-456";

            _mockFlowAddressMap.Setup(m => m.GetAsync(flowId))
                .ReturnsAsync(new List<string>());
            _mockBranchAddressMap.Setup(m => m.GetAsync($"{flowId}:{branchId}"))
                .ReturnsAsync(new List<string>());

            // Act
            await _registry.RegisterAddressAsync(address, flowId, branchId);

            // Assert
            _mockFlowAddressMap.Verify(m => m.PutAsync(flowId, It.IsAny<List<string>>()), Times.Once);
            _mockBranchAddressMap.Verify(m => m.PutAsync($"{flowId}:{branchId}", It.IsAny<List<string>>()), Times.Once);
            _mockAddressOwnerMap.Verify(m => m.PutAsync(address, $"{flowId}:{branchId}"), Times.Once);
        }

        [Fact]
        public async Task GetAddressOwnerAsync_WithRegisteredAddress_ReturnsOwner()
        {
            // Arrange
            var address = "memory:0x12345";
            var flowId = "flow-123";
            var branchId = "branch-456";
            var owner = $"{flowId}:{branchId}";

            _mockAddressOwnerMap.Setup(m => m.GetAsync(address))
                .ReturnsAsync(owner);

            // Act
            var result = await _registry.GetAddressOwnerAsync(address);

            // Assert
            Assert.Equal(owner, result);
            _mockAddressOwnerMap.Verify(m => m.GetAsync(address), Times.Once);
        }

        [Fact]
        public async Task GetFlowAddressesAsync_WithValidFlowId_ReturnsAddresses()
        {
            // Arrange
            var flowId = "flow-123";
            var addresses = new List<string> { "memory:0x12345", "memory:0x67890" };

            _mockFlowAddressMap.Setup(m => m.GetAsync(flowId))
                .ReturnsAsync(addresses);

            // Act
            var result = await _registry.GetFlowAddressesAsync(flowId);

            // Assert
            Assert.Equal(addresses, result);
            _mockFlowAddressMap.Verify(m => m.GetAsync(flowId), Times.Once);
        }

        [Fact]
        public async Task GetBranchAddressesAsync_WithValidIds_ReturnsAddresses()
        {
            // Arrange
            var flowId = "flow-123";
            var branchId = "branch-456";
            var addresses = new List<string> { "memory:0x12345", "memory:0x67890" };

            _mockBranchAddressMap.Setup(m => m.GetAsync($"{flowId}:{branchId}"))
                .ReturnsAsync(addresses);

            // Act
            var result = await _registry.GetBranchAddressesAsync(flowId, branchId);

            // Assert
            Assert.Equal(addresses, result);
            _mockBranchAddressMap.Verify(m => m.GetAsync($"{flowId}:{branchId}"), Times.Once);
        }
    }
}
