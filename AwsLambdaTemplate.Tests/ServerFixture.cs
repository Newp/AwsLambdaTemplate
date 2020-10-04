using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
                {
                    
                }));

        public HttpClient CreateClient() => this.testServer.CreateClient();
    }
}
