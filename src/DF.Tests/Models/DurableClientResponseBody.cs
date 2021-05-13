namespace DF.Tests.Models
{
    /// <summary>
    /// Class for mocking the response body from a durable client.
    /// </summary>
    public class DurableClientResponseBody
    {
        public string Id { get; set; }
        public string StatusQueryGetUri { get; set; }
        public string SendEventPostUri { get; set; }
        public string TerminatePostUri { get; set; }
        public string PurgeHistoryDeleteUri { get; set; }
    }
}
