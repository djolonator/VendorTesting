

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace VendorTesting.Models
{
    public class TestJsonResponseModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        [JsonProperty("errors")]
        public JObject Errors { get; set; }
    }
}
