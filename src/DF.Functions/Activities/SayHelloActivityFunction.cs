namespace DF.Functions.Activities
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class that returns a hello string.
    /// </summary>
    public static class SayHelloActivityFunction
    {
        /// <summary>
        /// Method that returns a hello string.
        /// </summary>
        /// <param name="name">The string to say hello to.</param>
        /// <param name="log">ILOgger.</param>
        /// <returns>A string saying hello to the {name} parameter.</returns>
        [FunctionName(nameof(SayHelloActivityFunction))]
        public static string RunSayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}
