using FlowOrchestrator.FlowManager;
using FluentAssertions;

namespace FlowOrchestrator.UnitTests.Management;

public class FlowConnectionTests
{
    [Fact]
    public void FlowConnection_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var flowConnection = new FlowConnection();

        // Assert
        flowConnection.ConnectionId.Should().BeEmpty();
        flowConnection.SourceComponentId.Should().BeEmpty();
        flowConnection.TargetComponentId.Should().BeEmpty();
        flowConnection.SourcePort.Should().BeEmpty();
        flowConnection.TargetPort.Should().BeEmpty();
        flowConnection.Condition.Should().BeNull();
        flowConnection.Priority.Should().Be(0);
        flowConnection.Metadata.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void FlowConnection_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var connectionId = "conn-123";
        var sourceComponentId = "comp1";
        var targetComponentId = "comp2";
        var sourcePort = "output";
        var targetPort = "input";
        var condition = "data.value > 10";
        var priority = 1;
        var metadata = new Dictionary<string, string>
        {
            { "color", "#00FF00" },
            { "label", "Test Connection" }
        };

        // Act
        var flowConnection = new FlowConnection
        {
            ConnectionId = connectionId,
            SourceComponentId = sourceComponentId,
            TargetComponentId = targetComponentId,
            SourcePort = sourcePort,
            TargetPort = targetPort,
            Condition = condition,
            Priority = priority,
            Metadata = metadata
        };

        // Assert
        flowConnection.ConnectionId.Should().Be(connectionId);
        flowConnection.SourceComponentId.Should().Be(sourceComponentId);
        flowConnection.TargetComponentId.Should().Be(targetComponentId);
        flowConnection.SourcePort.Should().Be(sourcePort);
        flowConnection.TargetPort.Should().Be(targetPort);
        flowConnection.Condition.Should().Be(condition);
        flowConnection.Priority.Should().Be(priority);
        flowConnection.Metadata.Should().BeEquivalentTo(metadata);
    }
}
