using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AwsLambdaTemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        Stopwatch watch = new Stopwatch();
        public void ConfigureServices(IServiceCollection services)
        {
            watch.Start();
            services.AddControllers();

            Console.WriteLine($"ConfigureServices end=>{watch.ElapsedMilliseconds}ms");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine($"configure start=>{watch.ElapsedMilliseconds}ms");
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            Console.WriteLine($"UseRouting end=>{watch.ElapsedMilliseconds}ms");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            Console.WriteLine($"UseEndpoints end=>{watch.ElapsedMilliseconds}ms");

            Console.WriteLine($"configure end=>{watch.ElapsedMilliseconds}ms");
            watch.Stop();
        }
    }
}
