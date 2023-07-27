using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using weatherDemo;

interface IIntentStrategy
{
  bool IsMatch(IntentRequest intentRequest);
  bool ShouldEndSession { get; }
  Task<string> Run(IntentRequest intentRequest);
}

class GetTodayWeatherIntentStrat : IIntentStrategy
{
  private readonly IWeatherService weatherService;

  public GetTodayWeatherIntentStrat(IWeatherService _weatherService)
  {
    weatherService = _weatherService;
  }

  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "GetTodayWeatherIntent";

  public bool ShouldEndSession => false;

  /// <summary>
  /// Provides today's high and low temperature for a given zip code
  /// </summary>
  /// <param name="input"></param>
  /// <param name="context"></param>
  /// <returns></returns>
  public async Task<string> Run(IntentRequest intentRequest)
  {
    var zip = intentRequest.Intent.Slots["Zip"].Value;

    if (zip == null)
    {
      return "I'm sorry, but I didn't understand.  Please provide a USA zip code.";
    }

    var weather = await weatherService.GetWeatherByZipAsync(zip);

    return $"For the zip code {weather.Zip}, today's high temperature is {weather.HighTemp} degrees and today's low temperature is {weather.LowTemp} degrees fahrenheit.";
  }
}

class CancelIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "AMAZON.CancelIntent";

  public bool ShouldEndSession => true;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("Ok. Shutting Down.");
}

class StopIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "AMAZON.StopIntent";

  public bool ShouldEndSession => true;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("Ok. Shutting Down.");
}

class HelpIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "AMAZON.HelpIntent";

  public bool ShouldEndSession => false;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("It sounds like you want help.  I can tell you the high and low temperatures for a given zip code in the USA.  Ask away!");
}

class DefaultIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => true;

  public bool ShouldEndSession => false;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("I'm sorry.  I didn't understand your request.  Please provide a 5 digit zip code");
}
