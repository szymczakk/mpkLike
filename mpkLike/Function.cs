using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace mpkLike
{
    public class Function
    {
        private readonly List<BussInfo> _answers = new List<BussInfo>
        {
            new BussInfo()
            {
                BusCount = 0,
                NextBusDates = null
            },
            new BussInfo()
            {
                BusCount = 2,
                NextBusDates = new DateTime[2]
                {
                    DateTime.Now.Add(TimeSpan.FromMinutes(15)),
                    DateTime.Now.Add(TimeSpan.FromMinutes(3))
                }
            }
        };


        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var result = GetBusdata();

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(result, Formatting.None),
                IsBase64Encoded = false
            };
        }

        private BussInfo GetBusdata()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return _answers[random.Next(0, _answers.Count)];
        }
    }

    internal class BussInfo
    {
        public int BusCount { get; set; }
        public DateTime[] NextBusDates { get; set; }
    }
}
