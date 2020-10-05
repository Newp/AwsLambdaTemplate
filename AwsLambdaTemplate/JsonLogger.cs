using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsLambdaTemplate
{
    public class JsonLogger
    {
        public void Write(LogLevel level, string subject, object value)
        {
            var time = DateTime.Now;
            var json = JsonConvert.SerializeObject(new { level, time, subject, value });

            Console.WriteLine( json.Replace('\n','\r') );
        }

        protected virtual void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
