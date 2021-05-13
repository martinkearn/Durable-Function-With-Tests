namespace DF.Functions.Orchestrators
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using DF.Functions.Activities;

    /// <summary>
    /// Class for main orchestrator function.
    /// </summary>
    public static class MainOrchestratorFunction
    {
        /// <summary>
        /// Method for Class for main orchestrator function.
        /// </summary>
        /// <param name="context">IDurableOrchestrationContext.</param>
        /// <returns>Returns a list of strings.</returns>
        [FunctionName(nameof(MainOrchestratorFunction))]
        public static async Task<List<string>> RunOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>
            {
                // Replace "hello" with the name of your Durable Activity Function.
                await context.CallActivityAsync<string>(nameof(SayHelloActivityFunction), "Tokyo"),
                await context.CallActivityAsync<string>(nameof(SayHelloActivityFunction), "Seattle"),
                await context.CallActivityAsync<string>(nameof(SayHelloActivityFunction), "London"),
            };

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}
