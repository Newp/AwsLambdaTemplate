using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsLambdaTemplate.Tests
{
    public class JsonUnitTestLogger : JsonLogger
    {
        protected override void Write(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }
    }
}
