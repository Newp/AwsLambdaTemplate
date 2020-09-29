using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AwsLambdaTemplate.Middlewares
{
    public class TryCatchMiddleware : IMiddleware
    {
        private readonly JsonLogger logger;

        public TryCatchMiddleware(JsonLogger logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HandledException hex)
            {
                context.Response.StatusCode = (int)hex.StatusCode;
            }
            catch (Exception ex)
            {
                logger.Write( LogLevel.Error, "unhandled exception", new { error = ex.Message, stacktrace =  ex.StackTrace });
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }

    public class HandledException : Exception
    {

        public HandledException(HttpStatusCode statusCode) : base($"handled exception=> {statusCode}")
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
