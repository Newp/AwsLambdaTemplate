using AwsLambdaTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            var token = new CancellationTokenSource();

            token.CancelAfter(5000);

            try
            {
                var dd = await dynamoDb.Client.ListTablesAsync(token.Token);
                return dd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
