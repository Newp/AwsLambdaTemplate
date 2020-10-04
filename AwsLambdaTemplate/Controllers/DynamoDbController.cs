using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsLambdaTemplate.Controllers
{
    [Route("api/[controller]")]
    public class DynamoDbController
    {

        public string Get()
        {
            return "dynamodb ok";
        }
    }
}
