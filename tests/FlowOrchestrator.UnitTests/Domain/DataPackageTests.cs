using FlowOrchestrator.Domain.Entities;
using FluentAssertions;

namespace FlowOrchestrator.UnitTests.Domain;

public class DataPackageTests
{
    [Fact]
    public void DataPackage_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dataPackage = new DataPackage();

        // Assert
        dataPackage.Id.Should().BeEmpty();
        dataPackage.Name.Should().BeEmpty();
        dataPackage.Description.Should().BeEmpty();
        dataPackage.MemoryAddress.Should().BeEmpty();
        dataPackage.DataFormat.Should().BeEmpty();
        dataPackage.Schema.Should().BeEmpty();
        dataPackage.SizeInBytes.Should().Be(0);
        dataPackage.RecordCount.Should().Be(0);
        dataPackage.SourceStepId.Should().BeNull();
        dataPackage.SourceBranchPath.Should().BeNull();
        dataPackage.ExecutionContextId.Should().BeNull();
        dataPackage.Metadata.Should().NotBeNull().And.BeEmpty();
        dataPackage.Tags.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void DataPackage_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var id = "data-123";
        var name = "Test Data Package";
        var description = "A test data package";
        var memoryAddress = "memory://test/data";
        var dataFormat = "JSON";
        var schema = "schema://test/schema";
        var sizeInBytes = 1024L;
        var recordCount = 10;
        var creationTimestamp = DateTime.UtcNow.AddMinutes(-5);
        var sourceStepId = "step-123";
        var sourceBranchPath = "branch-1";
        var executionContextId = "exec-123";

        // Act
        var dataPackage = new DataPackage(
            id,
            name,
            description,
            memoryAddress,
            dataFormat,
            schema,
            sizeInBytes,
            recordCount,
            creationTimestamp,
            sourceStepId,
            sourceBranchPath,
            executionContextId);

        // Assert
        dataPackage.Id.Should().Be(id);
        dataPackage.Name.Should().Be(name);
        dataPackage.Description.Should().Be(description);
        dataPackage.MemoryAddress.Should().Be(memoryAddress);
        dataPackage.DataFormat.Should().Be(dataFormat);
        dataPackage.Schema.Should().Be(schema);
        dataPackage.SizeInBytes.Should().Be(sizeInBytes);
        dataPackage.RecordCount.Should().Be(recordCount);
        dataPackage.CreationTimestamp.Should().Be(creationTimestamp);
        dataPackage.SourceStepId.Should().Be(sourceStepId);
        dataPackage.SourceBranchPath.Should().Be(sourceBranchPath);
        dataPackage.ExecutionContextId.Should().Be(executionContextId);
    }

    [Fact]
    public void DataPackage_AddMetadata_ShouldAddMetadataCorrectly()
    {
        // Arrange
        var dataPackage = new DataPackage();
        var key = "source";
        var value = "API";

        // Act
        dataPackage.AddMetadata(key, value);

        // Assert
        dataPackage.Metadata.Should().ContainKey(key);
        dataPackage.Metadata[key].Should().Be(value);
    }

    [Fact]
    public void DataPackage_AddTag_ShouldAddTagCorrectly()
    {
        // Arrange
        var dataPackage = new DataPackage();
        var tag = "test-tag";

        // Act
        dataPackage.AddTag(tag);

        // Assert
        dataPackage.Tags.Should().Contain(tag);
    }

    [Fact]
    public void DataPackage_AddTag_ShouldNotAddDuplicateTag()
    {
        // Arrange
        var dataPackage = new DataPackage();
        var tag = "test-tag";

        // Act
        dataPackage.AddTag(tag);
        dataPackage.AddTag(tag);

        // Assert
        dataPackage.Tags.Should().HaveCount(1);
        dataPackage.Tags.Should().Contain(tag);
    }

    [Fact]
    public void DataPackage_SetExpirationTimestamp_ShouldSetExpirationCorrectly()
    {
        // Arrange
        var dataPackage = new DataPackage();
        var expirationTimestamp = DateTime.UtcNow.AddHours(1);

        // Act
        dataPackage.SetExpirationTimestamp(expirationTimestamp);

        // Assert
        dataPackage.ExpirationTimestamp.Should().Be(expirationTimestamp);
    }

    [Fact]
    public void DataPackage_HasExpired_ShouldReturnFalseWhenNotExpired()
    {
        // Arrange
        var dataPackage = new DataPackage();
        var expirationTimestamp = DateTime.UtcNow.AddHours(1);
        dataPackage.SetExpirationTimestamp(expirationTimestamp);

        // Act
        var hasExpired = dataPackage.HasExpired();

        // Assert
        hasExpired.Should().BeFalse();
    }

    [Fact]
    public void DataPackage_HasExpired_ShouldReturnTrueWhenExpired()
    {
        // Arrange
        var dataPackage = new DataPackage();
        var expirationTimestamp = DateTime.UtcNow.AddHours(-1);
        dataPackage.SetExpirationTimestamp(expirationTimestamp);

        // Act
        var hasExpired = dataPackage.HasExpired();

        // Assert
        hasExpired.Should().BeTrue();
    }
}
