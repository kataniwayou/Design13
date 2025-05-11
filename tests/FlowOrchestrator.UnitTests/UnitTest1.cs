using FlowOrchestrator.FlowManager;
using FluentAssertions;

namespace FlowOrchestrator.UnitTests.Management;

public class FlowDefinitionTests
{
    [Fact]
    public void FlowDefinition_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var flowDefinition = new FlowDefinition();

        // Assert
        flowDefinition.FlowId.Should().BeEmpty();
        flowDefinition.Name.Should().BeEmpty();
        flowDefinition.Description.Should().BeEmpty();
        flowDefinition.Version.Should().BeEmpty();
        flowDefinition.Components.Should().NotBeNull().And.BeEmpty();
        flowDefinition.Connections.Should().NotBeNull().And.BeEmpty();
        flowDefinition.Parameters.Should().NotBeNull().And.BeEmpty();
        flowDefinition.Metadata.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void FlowDefinition_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var flowId = "flow-123";
        var name = "Test Flow";
        var description = "A test flow";
        var version = "1.0.0";
        var components = new List<FlowComponent>
        {
            new FlowComponent { ComponentId = "comp1", Name = "Component 1" },
            new FlowComponent { ComponentId = "comp2", Name = "Component 2" }
        };
        var connections = new List<FlowConnection>
        {
            new FlowConnection { ConnectionId = "conn1", SourceComponentId = "comp1", TargetComponentId = "comp2" }
        };
        var parameters = new List<FlowParameter>
        {
            new FlowParameter { Name = "param1", DefaultValue = "value1" },
            new FlowParameter { Name = "param2", DefaultValue = 42 }
        };
        var metadata = new Dictionary<string, string>
        {
            { "createdBy", "user1" },
            { "createdAt", DateTime.UtcNow.ToString() }
        };

        // Act
        var flowDefinition = new FlowDefinition
        {
            FlowId = flowId,
            Name = name,
            Description = description,
            Version = version,
            Components = components,
            Connections = connections,
            Parameters = parameters,
            Metadata = metadata
        };

        // Assert
        flowDefinition.FlowId.Should().Be(flowId);
        flowDefinition.Name.Should().Be(name);
        flowDefinition.Description.Should().Be(description);
        flowDefinition.Version.Should().Be(version);
        flowDefinition.Components.Should().BeEquivalentTo(components);
        flowDefinition.Connections.Should().BeEquivalentTo(connections);
        flowDefinition.Parameters.Should().BeEquivalentTo(parameters);
        flowDefinition.Metadata.Should().BeEquivalentTo(metadata);
    }
}

public class FlowComponentTests
{
    [Fact]
    public void FlowComponent_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var flowComponent = new FlowComponent();

        // Assert
        flowComponent.ComponentId.Should().BeEmpty();
        flowComponent.Name.Should().BeEmpty();
        flowComponent.Description.Should().BeEmpty();
        flowComponent.ComponentType.Should().BeEmpty();
        flowComponent.Configuration.Should().NotBeNull().And.BeEmpty();
        flowComponent.Position.Should().NotBeNull();
    }

    [Fact]
    public void FlowComponent_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var componentId = "comp-123";
        var name = "Test Component";
        var description = "A test component";
        var componentType = "Processor";
        var configuration = new Dictionary<string, object>
        {
            { "setting1", "value1" },
            { "setting2", 42 }
        };
        var position = new FlowPosition { X = 100, Y = 200 };

        // Act
        var flowComponent = new FlowComponent
        {
            ComponentId = componentId,
            Name = name,
            Description = description,
            ComponentType = componentType,
            Configuration = configuration,
            Position = position
        };

        // Assert
        flowComponent.ComponentId.Should().Be(componentId);
        flowComponent.Name.Should().Be(name);
        flowComponent.Description.Should().Be(description);
        flowComponent.ComponentType.Should().Be(componentType);
        flowComponent.Configuration.Should().BeEquivalentTo(configuration);
        flowComponent.Position.Should().BeEquivalentTo(position);
    }
}
