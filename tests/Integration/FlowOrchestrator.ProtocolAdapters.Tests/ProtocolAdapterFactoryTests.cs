using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlowOrchestrator.ProtocolAdapters.Tests;

public class ProtocolAdapterFactoryTests
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<ILogger<ProtocolAdapterFactory>> _loggerMock;
    private readonly Mock<IProtocolAdapter> _httpAdapterMock;
    private readonly Mock<IProtocolAdapter> _mqttAdapterMock;

    public ProtocolAdapterFactoryTests()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _loggerMock = new Mock<ILogger<ProtocolAdapterFactory>>();
        _httpAdapterMock = new Mock<IProtocolAdapter>();
        _mqttAdapterMock = new Mock<IProtocolAdapter>();

        _httpAdapterMock.Setup(a => a.ProtocolName).Returns("HTTP");
        _mqttAdapterMock.Setup(a => a.ProtocolName).Returns("MQTT");

        var adapters = new List<IProtocolAdapter> { _httpAdapterMock.Object, _mqttAdapterMock.Object };
        var serviceScope = new Mock<IServiceScope>();
        var serviceScopeFactory = new Mock<IServiceScopeFactory>();

        serviceScope.Setup(s => s.ServiceProvider).Returns(_serviceProviderMock.Object);
        serviceScopeFactory.Setup(f => f.CreateScope()).Returns(serviceScope.Object);
        _serviceProviderMock.Setup(s => s.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);
        _serviceProviderMock.Setup(s => s.GetService(typeof(IEnumerable<IProtocolAdapter>))).Returns(adapters);
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var factory = new ProtocolAdapterFactory(_serviceProviderMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(factory);
    }

    [Fact]
    public void Constructor_WithNullParameters_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ProtocolAdapterFactory(null!, _loggerMock.Object));
        Assert.Throws<ArgumentNullException>(() => new ProtocolAdapterFactory(_serviceProviderMock.Object, null!));
    }

    [Fact]
    public void GetAdapter_WithValidProtocolName_ReturnsAdapter()
    {
        // Arrange
        var factory = new ProtocolAdapterFactory(_serviceProviderMock.Object, _loggerMock.Object);
        var adapters = new List<IProtocolAdapter> { _httpAdapterMock.Object, _mqttAdapterMock.Object };
        _serviceProviderMock.Setup(s => s.GetService(typeof(IEnumerable<IProtocolAdapter>))).Returns(adapters);

        // Act
        var adapter = factory.GetAdapter("HTTP");

        // Assert
        Assert.NotNull(adapter);
        Assert.Equal("HTTP", adapter.ProtocolName);
    }

    [Fact]
    public void GetAdapter_WithInvalidProtocolName_ThrowsInvalidOperationException()
    {
        // Arrange
        var factory = new ProtocolAdapterFactory(_serviceProviderMock.Object, _loggerMock.Object);
        var adapters = new List<IProtocolAdapter> { _httpAdapterMock.Object, _mqttAdapterMock.Object };
        _serviceProviderMock.Setup(s => s.GetService(typeof(IEnumerable<IProtocolAdapter>))).Returns(adapters);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => factory.GetAdapter("INVALID"));
    }

    [Fact]
    public void GetAllAdapters_ReturnsAllAdapters()
    {
        // Arrange
        var factory = new ProtocolAdapterFactory(_serviceProviderMock.Object, _loggerMock.Object);
        var adapters = new List<IProtocolAdapter> { _httpAdapterMock.Object, _mqttAdapterMock.Object };
        _serviceProviderMock.Setup(s => s.GetService(typeof(IEnumerable<IProtocolAdapter>))).Returns(adapters);

        // Act
        var result = factory.GetAllAdapters();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, a => a.ProtocolName == "HTTP");
        Assert.Contains(result, a => a.ProtocolName == "MQTT");
    }

    [Fact]
    public void HasAdapter_WithValidProtocolName_ReturnsTrue()
    {
        // Arrange
        var factory = new ProtocolAdapterFactory(_serviceProviderMock.Object, _loggerMock.Object);
        var adapters = new List<IProtocolAdapter> { _httpAdapterMock.Object, _mqttAdapterMock.Object };
        _serviceProviderMock.Setup(s => s.GetService(typeof(IEnumerable<IProtocolAdapter>))).Returns(adapters);

        // Act
        var result = factory.HasAdapter("HTTP");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasAdapter_WithInvalidProtocolName_ReturnsFalse()
    {
        // Arrange
        var factory = new ProtocolAdapterFactory(_serviceProviderMock.Object, _loggerMock.Object);
        var adapters = new List<IProtocolAdapter> { _httpAdapterMock.Object, _mqttAdapterMock.Object };
        _serviceProviderMock.Setup(s => s.GetService(typeof(IEnumerable<IProtocolAdapter>))).Returns(adapters);

        // Act
        var result = factory.HasAdapter("INVALID");

        // Assert
        Assert.False(result);
    }
}
