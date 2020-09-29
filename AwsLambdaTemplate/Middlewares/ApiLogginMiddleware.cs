
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AwsLambdaTemplate.Middlewares
{
    public class ApiLogginMiddleware : IMiddleware
    {
        private readonly Encoding encoding;
        private readonly JsonLogger logger;

        public ApiLogginMiddleware(Encoding encoding, JsonLogger logger)
        {
            this.encoding = encoding;
            this.logger = logger;
        }

        public struct ApiInvokeResult
        {
            public int StatusCode;
            public string RequestBody;
            public string ResponseBody;
            public long ElapsedMilliseconds;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch watch = new Stopwatch();
            watch.Restart();

            context.Request.EnableBuffering();

            //request scope
            var requestBuffer = new MemoryStream();
            await context.Request.BodyReader.CopyToAsync(requestBuffer);
            context.Request.Body.Position = 0;

            //response scope
            var clientResponseStream = context.Response.Body;
            var responseBuffer = new MemoryStream();
            context.Response.Body = responseBuffer;

            //process api action
            await next(context);

            responseBuffer.Position = 0;
            await responseBuffer.CopyToAsync(clientResponseStream);

            //await clientResponseStream.WriteAsync(response, 0, response.Length);

            watch.Stop();
            
            var result = new ApiInvokeResult()
            {
                StatusCode = context.Response.StatusCode,
                ElapsedMilliseconds = watch.ElapsedMilliseconds,
                RequestBody = encoding.GetString( requestBuffer.ToArray()),
                ResponseBody = encoding.GetString(responseBuffer.ToArray()),
            };

            logger.Write(Microsoft.Extensions.Logging.LogLevel.Information, "api log", result);
            
        }
    }
}