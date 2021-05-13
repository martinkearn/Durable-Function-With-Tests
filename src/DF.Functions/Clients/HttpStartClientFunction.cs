namespace DF.Functions.Clients
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using DF.Functions.Orchestrators;

    /// <summary>
    /// Class for starting the orchestration via http.
    /// </summary>
    public static class HttpStartClientFunction
    {
        /// <summary>
        /// Method for starting the orchestration via http.
        /// </summary>
        /// <param name="req">HttpRequestMessage.</param>
        /// <param name="starter">IDurableOrchestrationClient.</param>
        /// <param name="log">ILogger.</param>
        /// <returns>Returns a HttpResponseMessage.</returns>
        [FunctionName(nameof(HttpStartClientFunction))]
        public static async Task<HttpResponseMessage> RunHttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            var instanceId = await starter.StartNewAsync(nameof(MainOrchestratorFunction), null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
