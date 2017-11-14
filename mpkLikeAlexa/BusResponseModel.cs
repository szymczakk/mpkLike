using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mpkLikeAlexa
{
    internal class BusResponseModel
    {
        public int BusCount { get; set; }
        public IEnumerable<DateTime> NextBusDates { get; set; }
    }

    internal class ApiResponseModel
    {
        public int statusCode { get; set; }
        public string headers { get; set; }
        public string body { get; set; }
        public bool isBase64Encoded { get; set; }
    }
}