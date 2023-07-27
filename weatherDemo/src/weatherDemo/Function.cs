using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace weatherDemo;

public class Function
{
    // Using Strategy Pattern lowers cyclomatic complexity and allows testing of individual strategies with less test buildup.
    private List<IIntentStrategy>? Strategies;

    /// <summary>
    /// Provides today's high and low temperature for a given zip code
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
    {
        Bootstrap(context);

        var log = context.Logger;
        log.LogLine($"Skill Request Object:");
        log.LogLine(JsonConvert.SerializeObject(input));

        var response = new SkillResponse
        {
            Response = new ResponseBody
            {
                ShouldEndSession = false
            },
            Version = "1.0"
        };

        var responseText = "";

        if (input.GetRequestType() == typeof(LaunchRequest))
        {
            log.LogLine($"Default LaunchRequest made: 'Alexa, open weather demo'");
            responseText = "I can tell you the high and low temperatures for a given zip code in the USA.  Ask away!";
        }
        else if (input.GetRequestType() == typeof(IntentRequest))
        {
            var intentRequest = (IntentRequest)input.Request;

            log.LogLine($"Skill Request Object:");
            log.LogLine(JsonConvert.SerializeObject(input));

            var strategy = DetermineIntentStrategy(intentRequest);

            responseText = await strategy.Run(intentRequest);
            response.Response.ShouldEndSession = strategy.ShouldEndSession;
        }

        response.Response.OutputSpeech = new PlainTextOutputSpeech
        {
            Text = responseText
        };

        log.LogLine("Skill Response Object:");
        log.LogLine(JsonConvert.SerializeObject(response));

        return response;
    }

    private IIntentStrategy DetermineIntentStrategy(IntentRequest intentRequest)
    {
        return Strategies.FirstOrDefault(
            s => s.IsMatch(intentRequest),
            new DefaultIntentStrat()
        );
    }

    private void Bootstrap(ILambdaContext context)
    {
        Strategies = new()
        {
            new GetTodayWeatherIntentStrat(new WeatherService(context.Logger)),
            new CancelIntentStrat(),
            new StopIntentStrat(),
            new HelpIntentStrat(),
        };
    }
}
