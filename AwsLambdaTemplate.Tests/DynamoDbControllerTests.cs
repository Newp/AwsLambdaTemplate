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
    public class DynamoDbControllerTests : ServerFixture
    {
        HttpClient client;

        public DynamoDbControllerTests()
        {
            this.client = base.CreateClient();
        }

        [Fact]
        public async Task ConnectionTest()
        {
            var result = await client.GetAsync("/api/dynamodb");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
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
            var res = await this.client.GetAsync("http://localhost:9200/");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
