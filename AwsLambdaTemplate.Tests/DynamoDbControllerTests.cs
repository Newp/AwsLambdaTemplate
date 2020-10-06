using AwsLambdaTemplate.Services;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AwsLambdaTemplate.Tests
{
    public class DynamoDbTests : ServerFixture
    {

        [Fact(Timeout = 300000)]
        public async Task ConnectionTest()
        {
            var dynamodb = base.GetService<DynamoDbService>();

            var tables = await dynamodb.Client.ListTablesAsync();

            base.GetService<JsonLogger>().Debug(tables);
        }
    }

    public class ElasticsearchTests : ServerFixture
    {
        HttpClient client;

        public ElasticsearchTests()
        {
            this.client = new HttpClient();
        }


        [Fact]
        public async Task ConnectOk()
        {
            try
            {
                var res = await this.client.GetAsync("http://localhost:9200/");

                Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            }
            catch (Exception ex)
            {
                var swap = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("<<==========================>>");
                Console.ForegroundColor = swap;
                AllException(ex);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("<<==========================>>");
                Console.ForegroundColor = swap;
                throw;
            }
        }

        void AllException(Exception exception)
        {
            Console.WriteLine(exception.ToString());

            if (exception.InnerException != null)
                AllException(exception.InnerException);
        }
    }
}
