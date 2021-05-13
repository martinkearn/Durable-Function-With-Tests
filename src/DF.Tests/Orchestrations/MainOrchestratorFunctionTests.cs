namespace DF.Tests.Orchestrations
{
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Moq;
    using Xunit;
    using DF.Functions.Activities;
    using DF.Functions.Orchestrators;

    /// <summary>
    /// Unit tests for the main orchestrator function.
    /// </summary>
    public class MainOrchestratorFunctionTests
    {
        /// <summary>
        /// Asserts that when the main orchetsrator is started, it returns the expected result.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact(DisplayName = "Asserts that when the main orchetsrator is started, it returns the expected result.")]
        public async Task MainOrchestratorRunOrchestrator_GivenAnyRequest_ReturnsExpectedResult()
        {
            // Arrange
            var mockContext = new Mock<IDurableOrchestrationContext>();
            mockContext.Setup(x => x.CallActivityAsync<string>(nameof(SayHelloActivityFunction), "Tokyo")).ReturnsAsync("Hello Tokyo!");
            mockContext.Setup(x => x.CallActivityAsync<string>(nameof(SayHelloActivityFunction), "Seattle")).ReturnsAsync("Hello Seattle!");
            mockContext.Setup(x => x.CallActivityAsync<string>(nameof(SayHelloActivityFunction), "London")).ReturnsAsync("Hello London!");

            // Act
            var result = await MainOrchestratorFunction.RunOrchestrator(mockContext.Object);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Hello Tokyo!", result[0]);
            Assert.Equal("Hello Seattle!", result[1]);
            Assert.Equal("Hello London!", result[2]);
        }
    }
}
