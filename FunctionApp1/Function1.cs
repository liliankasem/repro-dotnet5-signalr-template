using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public static class Function1
    {
        [Function("negotiate")]
        public static HttpResponseData Negotiate(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            [SignalRConnectionInfoInput(HubName = "")] MyConnectionInfo connectionInfo, FunctionContext context)
        {
            var logger = context.GetLogger("negotiate");
            logger.LogInformation($"SignalR Connection URL = '{connectionInfo.Url}'");

            var response = new HttpResponseData(HttpStatusCode.OK);
            var headers = new Dictionary<string, string>();
            headers.Add("Content", "Content - Type: text / html; charset = utf - 8");

            response.Headers = headers;
            response.Body = $"Connection URL = '{connectionInfo.Url}'";

            return response;
        }
    }

    public class MyConnectionInfo
    {
        public string Url { get; set; }

        public string AccessToken { get; set; }
    }
}