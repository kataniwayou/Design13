using FlowOrchestrator.Domain.Models;
using FluentAssertions;

namespace FlowOrchestrator.UnitTests.Domain;

public class TimeRangeTests
{
    [Fact]
    public void TimeRange_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var timeRange = new TimeRange();

        // Assert
        timeRange.Start.Should().BeCloseTo(DateTime.MinValue, TimeSpan.FromSeconds(1));
        timeRange.End.Should().BeCloseTo(DateTime.MinValue, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void TimeRange_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var startTime = DateTime.UtcNow.AddDays(-1);
        var endTime = DateTime.UtcNow;

        // Act
        var timeRange = new TimeRange
        {
            Start = startTime,
            End = endTime
        };

        // Assert
        timeRange.Start.Should().Be(startTime);
        timeRange.End.Should().Be(endTime);
    }

    [Fact]
    public void TimeRange_LastHours_ShouldCreateCorrectTimeRange()
    {
        // Arrange
        int hours = 24;

        // Act
        var timeRange = TimeRange.LastHours(hours);

        // Assert
        timeRange.End.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        timeRange.Start.Should().BeCloseTo(DateTime.UtcNow.AddHours(-hours), TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void TimeRange_LastDays_ShouldCreateCorrectTimeRange()
    {
        // Arrange
        int days = 7;

        // Act
        var timeRange = TimeRange.LastDays(days);

        // Assert
        timeRange.End.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        timeRange.Start.Should().BeCloseTo(DateTime.UtcNow.AddDays(-days), TimeSpan.FromSeconds(1));
    }
}
