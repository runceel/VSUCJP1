using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Demo2
{
    public class Files
    {
        [FunctionName("GetFile")]
        public async Task<IActionResult> GetFile([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "file/{fileName}")]HttpRequest req,
            [Blob("files/{fileName}", FileAccess.Read)]Stream file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return new FileContentResult(ms.ToArray(), "text/plain");
            }
        }
        
        [FunctionName("UploadFile")]
        public async Task<IActionResult> UploadFile([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "file/{fileName}")]HttpRequest req,
            [Blob("files/{fileName}", FileAccess.Write)]Stream file)
        {
            await req.Body.CopyToAsync(file);
            return new AcceptedResult();
        }

        [FunctionName("BatchExecute")]
        public async Task<IActionResult> ExecuteBatch([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "batch")]HttpRequest req,
            [Queue("batch")]IAsyncCollector<string> queue)
        {
            var files = JsonConvert.DeserializeObject<string[]>(await req.ReadAsStringAsync());
            foreach (var file in files)
            {
                await queue.AddAsync(file);
            }

            return new AcceptedResult();
        }

        [FunctionName("Execute")]
        public async Task Execute([QueueTrigger("batch")] string fileName,
            [Blob("files/{queueTrigger}", FileAccess.Read)] Stream file,
            ILogger log)
        {
            log.LogInformation($"Execute started: {fileName}, {file is null}");
            await Task.Delay(3000);
            log.LogInformation($"Done: {await new StreamReader(file).ReadToEndAsync()}");
        }
    }
}
