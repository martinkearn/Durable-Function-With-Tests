namespace DF.Tests.Clients
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;
    using DF.Functions.Clients;
    using DF.Functions.Orchestrators;
    using DF.Tests.Models;

    /// <summary>
    /// Tests for HttpStartClient.
    /// </summary>
    public class HttpStartClientFunctionTests
    {
        /// <summary>
        /// Asserts that when HttpStartClient is called it is started and returns a retry header and 202/Accepted status code.
        /// Guidance taken from https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-unit-testing
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact(DisplayName = "Asserts that when HttpStartClient is called it is started and returns a retry header and 202 / Accepted status code")]
        public async Task HttpStart_GivenAnyRequest_ReturnsExpectedResult()
        {
            // Arrange
            string functionName = nameof(MainOrchestratorFunction);
            string instanceId = Guid.NewGuid().ToString();
            Uri locationHeader = new Uri($"http://localhost:7071/runtime/webhooks/durabletask/instances/{instanceId}?taskhub");
            var loggerMock = new Mock<ILogger>();
            var clientMock = new Mock<IDurableClient>();
            var responseBody = new DurableClientResponseBody()
            {
                Id = instanceId,
            };

            clientMock.
                Setup(x => x.StartNewAsync(functionName, It.IsAny<string>(), It.IsAny<object>())).
                ReturnsAsync(instanceId);

            clientMock
                .Setup(x => x.CreateCheckStatusResponse(It.IsAny<HttpRequestMessage>(), instanceId, It.IsAny<Boolean>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Accepted,
                    Content = new StringContent(JsonConvert.SerializeObject(responseBody, Formatting.Indented)),
                    Headers =
                    {
                        RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(10)),
                        Location = locationHeader
                    }
                });

            // Act
            var result = await HttpStartClientFunction.RunHttpStart(
                new HttpRequestMessage()
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json"),
                    RequestUri = new Uri("http://localhost:7071/api/HttpStartClient"),
                },
                clientMock.Object,
                loggerMock.Object);
            var resultBody = JsonConvert.DeserializeObject<DurableClientResponseBody>(await result.Content.ReadAsStringAsync());

            // Assert
            Assert.NotNull(result.Headers.RetryAfter);
            Assert.Equal(TimeSpan.FromSeconds(10), result.Headers.RetryAfter.Delta);
            Assert.Equal(locationHeader, result.Headers.Location);
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
            Assert.Equal(instanceId, resultBody.Id);
        }
    }
}
