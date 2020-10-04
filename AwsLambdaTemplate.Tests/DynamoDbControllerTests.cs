using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);

            var responseBody = await result.Content.ReadAsStringAsync();
            Assert.Equal("dynamodb ok", responseBody);
        }
    }
}
