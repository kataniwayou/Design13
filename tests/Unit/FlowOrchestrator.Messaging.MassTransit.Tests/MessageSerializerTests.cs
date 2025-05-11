using System;
using System.Text;
using FlowOrchestrator.Messaging.MassTransit;
using Xunit;

namespace FlowOrchestrator.Messaging.MassTransit.Tests
{
    public class MessageSerializerTests
    {
        private readonly MessageSerializer _serializer;

        public MessageSerializerTests()
        {
            _serializer = new MessageSerializer();
        }

        [Fact]
        public void SerializeToJson_WithValidObject_ReturnsJsonString()
        {
            // Arrange
            var testMessage = new TestMessage { Id = 1, Name = "Test Message" };

            // Act
            var json = _serializer.SerializeToJson(testMessage);

            // Assert
            Assert.NotNull(json);
            Assert.Contains("\"Id\":1", json);
            Assert.Contains("\"Name\":\"Test Message\"", json);
        }

        [Fact]
        public void DeserializeFromJson_WithValidJson_ReturnsObject()
        {
            // Arrange
            var json = "{\"Id\":1,\"Name\":\"Test Message\"}";

            // Act
            var result = _serializer.DeserializeFromJson<TestMessage>(json);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Message", result.Name);
        }

        [Fact]
        public void SerializeToBytes_WithValidObject_ReturnsByteArray()
        {
            // Arrange
            var testMessage = new TestMessage { Id = 1, Name = "Test Message" };

            // Act
            var bytes = _serializer.SerializeToBytes(testMessage);

            // Assert
            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 0);
        }

        [Fact]
        public void DeserializeFromBytes_WithValidBytes_ReturnsObject()
        {
            // Arrange
            var json = "{\"Id\":1,\"Name\":\"Test Message\"}";
            var bytes = Encoding.UTF8.GetBytes(json);

            // Act
            var result = _serializer.DeserializeFromBytes<TestMessage>(bytes);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Message", result.Name);
        }

        private class TestMessage
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
