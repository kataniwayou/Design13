using System;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions.Entities;
using FlowOrchestrator.Data.MongoDB;
using Moq;
using Xunit;

namespace FlowOrchestrator.Data.MongoDB.Tests
{
    public class EntityRepositoryTests
    {
        private readonly Mock<MongoDbDataStore> _mockDataStore;
        private readonly EntityRepository _repository;

        public EntityRepositoryTests()
        {
            _mockDataStore = new Mock<MongoDbDataStore>();
            _repository = new EntityRepository(_mockDataStore.Object);
        }

        [Fact]
        public async Task SaveEntityAsync_WithValidEntity_CallsDataStore()
        {
            // Arrange
            var entity = new TestEntity { Id = "test-id", Name = "Test Entity" };

            // Act
            await _repository.SaveEntityAsync(entity);

            // Assert
            _mockDataStore.Verify(ds => ds.GetCollection<TestEntity>(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetEntityAsync_WithValidId_ReturnsEntity()
        {
            // Arrange
            var entityId = "test-id";
            var entity = new TestEntity { Id = entityId, Name = "Test Entity" };

            // Act
            var result = await _repository.GetEntityAsync<TestEntity>(entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entityId, result.Id);
        }

        [Fact]
        public async Task DeleteEntityAsync_WithValidId_CallsDataStore()
        {
            // Arrange
            var entityId = "test-id";

            // Act
            await _repository.DeleteEntityAsync<TestEntity>(entityId);

            // Assert
            _mockDataStore.Verify(ds => ds.GetCollection<TestEntity>(It.IsAny<string>()), Times.Once);
        }

        private class TestEntity : IEntity
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
