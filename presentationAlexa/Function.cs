using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace presentationAlexa
{
    public class Function
    {
        private ILambdaLogger _logger;

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            _logger = context.Logger;

            switch (input.Request)
            {
                case LaunchRequest launchRequest:
                    return LaunchRequestIntent(launchRequest, context);

                case IntentRequest intentRequest:
                    switch (intentRequest.Intent.Name)
                    {
                        case "IntroduceYourself":
                            return IntroduceYourselfIntent(intentRequest, context);

                        case "IntroduceMe":
                            return IntroduceMeIntent(intentRequest, context);
                    }
                    break;
            }

            throw new NotImplementedException("Unknown request type.");
        }

        private SkillResponse IntroduceMeIntent(IntentRequest intentRequest, ILambdaContext context)
        {
            var text = "Of course. This is Cris... .NET developer working here in PGS. He chose .NET but fall in love with JavaScript. As far as I know, the easiest way to contact him is through twitter. He seems to be spending almost all his free time there. Poor boy.";

            return ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = text
            });
        }

        private SkillResponse LaunchRequestIntent(LaunchRequest launchRequest, ILambdaContext context)
        {
            var text = "Hi everyone, nice to hear you! Welcome in PGS, in our new office. Hope you will enjoy your stay. Let us start the presentation about Alexa, that means me!";

            var response = ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = text
            });

            return response;
        }

        private SkillResponse IntroduceYourselfIntent(IntentRequest intentRequest, ILambdaContext context)
        {
            return ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = "I'm Alexa, your small CIA spy right in your home. No... I'm joking... hahaha. I'm just small device at your service. I have 7 microphones, WiFi, funny led on top and few buttons. But what is the most important is AWS cloud right behind me. Please Cris continue"
            });
        }
    }
}
