using AMDTServerModule.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AMDTServerModule.MiddleWares
{
    public class MetricMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly MetricReporter _metricReporter;

        public MetricMiddleWare(RequestDelegate next, MetricReporter metricReporter)
        {
            _next = next;
            _metricReporter = metricReporter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture request size (content length)
            var requestSize = context.Request.ContentLength ?? 0;
            _metricReporter.RegisterRequestSize(requestSize);

            // Capture the response size by writing to a new memory stream
            var originalResponseBody = context.Response.Body;
            using (var newResponseBody = new MemoryStream())
            {
                context.Response.Body = newResponseBody;

                await _next(context);

                // After the response is written, measure the response size
                var responseSize = newResponseBody.Length;
                _metricReporter.RegisterResponseSize(responseSize);

                // Copy the response body back to the original response stream
                await newResponseBody.CopyToAsync(originalResponseBody);
            }
        }
    }
}
