using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace VSUCJP1
{
    public class HelloWorld
    {
        [FunctionName("SayHello")]
        public IActionResult SayHello(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req) => 
            new OkObjectResult($"Hello world!! {req.Query["name"]}!!");
    }
}