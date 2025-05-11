using System;
using System.Collections.Generic;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Telemetry.OpenTelemetry;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FlowOrchestrator.Telemetry.OpenTelemetry.Tests
{
    public class MetricsCollectorTests
    {
        private readonly Mock<ILogger<MetricsCollector>> _mockLogger;
        private readonly Mock<ConfigurationParameters> _mockConfiguration;
        private readonly MetricsCollector _metricsCollector;

        public MetricsCollectorTests()
        {
            _mockLogger = new Mock<ILogger<MetricsCollector>>();
            _mockConfiguration = new Mock<ConfigurationParameters>();
            _metricsCollector = new MetricsCollector(_mockConfiguration.Object, _mockLogger.Object);
        }

        [Fact]
        public void RecordCounter_WithValidName_LogsMetric()
        {
            // Arrange
            var counterName = "test_counter";
            var value = 1;
            var tags = new Dictionary<string, object> { { "tag1", "value1" } };

            // Act
            _metricsCollector.RecordCounter(counterName, value, tags);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Debug,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(counterName) && v.ToString().Contains(value.ToString())),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public void RecordGauge_WithValidName_LogsMetric()
        {
            // Arrange
            var gaugeName = "test_gauge";
            var value = 42.0;
            var tags = new Dictionary<string, object> { { "tag1", "value1" } };

            // Act
            _metricsCollector.RecordGauge(gaugeName, value, tags);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Debug,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(gaugeName) && v.ToString().Contains(value.ToString())),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public void RecordHistogram_WithValidName_LogsMetric()
        {
            // Arrange
            var histogramName = "test_histogram";
            var value = 42.0;
            var tags = new Dictionary<string, object> { { "tag1", "value1" } };

            // Act
            _metricsCollector.RecordHistogram(histogramName, value, tags);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Debug,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(histogramName) && v.ToString().Contains(value.ToString())),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
