namespace DF.Functions.Clients
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class that performs a health check and returns a 200/ok message. used for chcking the service is up.
    /// </summary>
    public static class HealthCheckClientFunction
    {
        /// <summary>
        /// Method that performs a health check and returns a 200/ok message. used for chcking the service is up.
        /// </summary>
        /// <param name="req">The HttpRequestMessage.</param>
        /// <param name="log">The ILogger.</param>
        /// <returns>HttpResponseMessage.</returns>
        [FunctionName(nameof(HealthCheckClientFunction))]
        public static HttpResponseMessage RunHealthCheck(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            ILogger log)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
