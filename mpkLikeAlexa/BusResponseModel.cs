using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mpkLikeAlexa
{
    [JsonObject]
    internal class BusResponseModel
    {
        [JsonProperty]
        public int BusCount { get; set; }

        [JsonProperty]
        public IEnumerable<DateTime> NextBusDates { get; set; }
    }

    [JsonObject]
    internal class ApiResponseModel
    {
        [JsonProperty]
        public int statusCode { get; set; }

        [JsonProperty]
        public string headers { get; set; }

        [JsonProperty]
        public BusResponseModel body { get; set; }

        [JsonProperty]
        public bool isBase64Encoded { get; set; }
    }
}