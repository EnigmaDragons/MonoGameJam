using System;
using System.Net.Http;
using System.Text;
using MonoDragons.Core.Errors;
using Newtonsoft.Json;

namespace MonoDragons.Core.Common
{
    public static class Metric
    {
        private static HttpClient _client = new HttpClient();
        private static string _runId = Guid.NewGuid().ToString();
        public static MetaAppDetails AppDetails { get; set; }

        public static void Record(string metricName, string value)
        {
            _client.PostAsync(
                "https://hk86vytqs1.execute-api.us-west-2.amazonaws.com/GameMetrics/RecordMetric",
                new StringContent(JsonConvert.SerializeObject(new MetricDetail
                {
                    ApplicationName = AppDetails.Name,
                    ApplicationVersion = AppDetails.Version,
                    MetricName = metricName,
                    Value = value,
                    PlayerID = _runId
                }), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
        }

        private class MetricDetail
        {
            public string ApplicationName { get; set; }
            public string ApplicationVersion { get; set; }
            public string MetricName { get; set; }
            public string Value { get; set; }
            public string PlayerID { get; set; }
        }
    }
}
