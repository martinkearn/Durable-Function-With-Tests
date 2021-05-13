namespace DF.Tests.Clients
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;
    using DF.Functions.Clients;

    /// <summary>
    /// Tests for HealthCheckClient.
    /// </summary>
    public class HealthCheckClientFunctionTests
    {
        /// <summary>
        /// Asserts that when HealthCheckClient is called a 200/OK response is returned.
        /// </summary>
        [Fact(DisplayName = "Asserts that when HealthCheckClient is called a 200/OK response is returned.")]
        public void HealthCheckClientHealthCheck_GivenAnyRequest_ReturnsOK()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var requestMock = new Mock<HttpRequestMessage>();

            // Act
            var result = HealthCheckClientFunction.RunHealthCheck(requestMock.Object, loggerMock.Object);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
