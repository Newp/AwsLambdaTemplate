using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwsLambdaTemplate.Tests
{
    public class ServerFixture
    {
        public TestServer testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(collection =>
                    collection
                        .AddSingleton<JsonLogger, JsonUnitTestLogger>()
                ));

        public HttpClient CreateClient() => this.testServer.CreateClient();
    }
}
