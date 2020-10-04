using AwsLambdaTemplate.Services;
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
        readonly DynamoDbService dynamoDb;

        public DynamoDbController(DynamoDbService dynamoDb)
        {
            this.dynamoDb = dynamoDb;
        }

        public async Task<dynamic> Get()
        {
            var dd = await dynamoDb.Client.ListTablesAsync();
            return dd;
        }
    }
}
