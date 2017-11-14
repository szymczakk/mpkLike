using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace mpkLikeAlexa
{
    public class Function
    {
        private string APIURL = "https://3yrnvbq3nc.execute-api.eu-central-1.amazonaws.com/dev";
        private ILambdaLogger _logger;

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            _logger = context.Logger;

            switch (input.Request)
            {
                case LaunchRequest launchRequest:
                    _logger.LogLine("Launch request");

                    return HandleLaunch(launchRequest, context);
                case IntentRequest intentRequest:
                    _logger.LogLine("Intent request");

                    switch (intentRequest.Intent.Name)
                    {
                        case "BusTimeIntent":
                            _logger.LogLine("BussTimeIntent started");

                            return HandleBussTimeIntent(intentRequest, context);
                    }
                    break;
            }

            throw new NotImplementedException("Unknown request type.");
        }

        private SkillResponse HandleBussTimeIntent(IntentRequest intentRequest, ILambdaContext context)
        {
            var busSlot = intentRequest.Intent.Slots["BUSNO"];
            _logger.LogLine("BusNo: " + busSlot.Value);
            var sb = new StringBuilder();

            var client = new HttpClient();

            var bussesResponse = client.GetAsync(this.APIURL).Result.Content.ReadAsStringAsync().Result;
            _logger.LogLine("Response:" + bussesResponse);

            var bussesData = GetBusResponseModel(bussesResponse);

            _logger.LogLine("after: " + JsonConvert.SerializeObject(bussesData));

            if (bussesData.BusCount == 0)
            {
                sb.AppendLine("Sorry, but you missed your last bus. You have to sleep at work");
            }
            else
            {
                sb.Append("You have ");
                sb.Append(bussesData.BusCount);
                sb.Append(" left. The next one is on ");
                sb.Append(bussesData.NextBusDates.First());
            }

            var response = ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = sb.ToString()
            });

            return response;
        }

        private static BusResponseModel GetBusResponseModel(string bussesApiResponse)
        {
            var bussesResponseModel = JsonConvert.DeserializeObject<ApiResponseModel>(bussesApiResponse);
            var bussesData =
                JsonConvert.DeserializeObject<BusResponseModel>(
                    bussesResponseModel.body);
            return bussesData;
        }

        private SkillResponse HandleLaunch(LaunchRequest request, ILambdaContext context)
        {
            var response = ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = "Hi! What line do you want to ask for?"
            });

            return response;
        }
    }
}
