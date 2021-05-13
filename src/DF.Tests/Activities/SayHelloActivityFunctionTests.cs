namespace DF.Tests.Activities
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;
    using DF.Functions.Activities;

    /// <summary>
    /// Tests for SayHelloActivity.
    /// </summary>
    public class SayHelloActivityFunctionTests
    {
        /// <summary>
        /// Asserts that when SayHelloActivity is given a name, it returns "hello name"
        /// </summary>
        [Fact(DisplayName = "Asserts that when SayHelloActivity is given a name, it returns hello name")]
        public void SayHelloActivitySayHello_GivenName_ReturnsHelloName()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var name = "foo";

            // Act
            var result = SayHelloActivityFunction.RunSayHello(name, loggerMock.Object);

            // Assert
            Assert.Equal($"Hello {name}!", result);
        }
    }
}
