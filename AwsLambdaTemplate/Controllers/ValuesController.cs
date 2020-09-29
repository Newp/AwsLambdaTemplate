using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwsLambdaTemplate.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReferenceTest;

namespace AwsLambdaTemplate.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        public ValuesController(JsonLogger logger)
        {
            logger.Write( LogLevel.Debug, "values controller created", new { });
        }

        [HttpGet("error")]
        public IEnumerable<string> Error()
        {

            throw new Exception("error test");
        }


        [HttpGet("error2")]
        public IEnumerable<string> Error2()
        {
            throw new HandledException( System.Net.HttpStatusCode.AlreadyReported);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", Sample.Message };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
