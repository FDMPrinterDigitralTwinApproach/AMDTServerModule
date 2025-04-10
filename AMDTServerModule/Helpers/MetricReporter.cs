using Prometheus;
using Microsoft.Extensions.Logging;
using System;

namespace AMDTServerModule.Helpers
{
    public class MetricReporter
    {
        private readonly ILogger<MetricReporter> _logger;
        private readonly Counter _requestCounter;
        private readonly Histogram _responseTimeHistogram;
        private readonly Counter _requestSizeCounter;  // Counter to track request size
        private readonly Counter _responseSizeCounter;  // Counter to track response size

        public MetricReporter(ILogger<MetricReporter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Initialize counters and histograms
            _requestCounter = Metrics.CreateCounter("total_requests", "The total number of requests serviced by this API.");
            _responseTimeHistogram = Metrics.CreateHistogram("request_duration_seconds", "The duration in seconds between the response to a request.",
                new HistogramConfiguration
                {
                    Buckets = Histogram.ExponentialBuckets(0.01, 2, 10),
                    LabelNames = new[] { "status_code", "method" }
                });

            // Metrics for tracking request and response sizes
            _requestSizeCounter = Metrics.CreateCounter("request_size_bytes", "The total size of incoming requests in bytes.");
            _responseSizeCounter = Metrics.CreateCounter("response_size_bytes", "The total size of responses sent by the server in bytes.");
        }

        public void RegisterRequest()
        {
            _requestCounter.Inc();
        }

        public void RegisterResponseTime(int statusCode, string method, TimeSpan elapsed)
        {
            _responseTimeHistogram.Labels(statusCode.ToString(), method).Observe(elapsed.TotalSeconds);
        }

        // Register the size of the incoming request
        public void RegisterRequestSize(long requestSize)
        {
            _requestSizeCounter.Inc(requestSize);
        }

        // Register the size of the outgoing response
        public void RegisterResponseSize(long responseSize)
        {
            _responseSizeCounter.Inc(responseSize);
        }
    }
}
