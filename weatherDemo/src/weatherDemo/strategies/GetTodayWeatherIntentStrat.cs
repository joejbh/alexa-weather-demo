using Alexa.NET.Request.Type;
using weatherDemo;

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
