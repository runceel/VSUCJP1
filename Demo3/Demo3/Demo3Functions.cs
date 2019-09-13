using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3
{
    public class Data
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Demo3Functions
    {
        [FunctionName("Negotiate")]
        public IActionResult Negotiate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "negotiate")]HttpRequest req,
            [SignalRConnectionInfo(HubName = "alert")] SignalRConnectionInfo info)
        {
            return new OkObjectResult(info);
        }

        [FunctionName("AddData")]
        public async Task<IActionResult> AddData([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequest req,
            [CosmosDB("vsucjp1", "data", ConnectionStringSetting = "CosmosDB", CreateIfNotExists = true, PartitionKey = "/type")]IAsyncCollector<Data> cosmosDb)
        {
            var receivedData = JsonConvert.DeserializeObject<int[]>(await req.ReadAsStringAsync());
            foreach (var x in receivedData)
            {
                await cosmosDb.AddAsync(new Data
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = "demodata",
                    Value = x,
                });
            }

            return new AcceptedResult();
        }

        [FunctionName("CheckData")]
        public async Task CheckData([CosmosDBTrigger("vsucjp1", "data", ConnectionStringSetting = "CosmosDB", CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> dataList,
            [SignalR(HubName = "alert")]IAsyncCollector<SignalRMessage> signalR,
            ILogger log)
        {
            log.LogInformation(JsonConvert.SerializeObject(dataList.Select(x => x.GetPropertyValue<int>("value"))));
            var alert = dataList.Select(x => (Data)(dynamic)x).Where(x => x.Value >= 100).ToArray();
            if (!alert.Any())
            {
                return;
            }

            foreach (var x in alert)
            {
                await signalR.AddAsync(new SignalRMessage
                {
                    Target = "onAlert",
                    Arguments = new object[] { x },
                });
            }
        }
    }
}
