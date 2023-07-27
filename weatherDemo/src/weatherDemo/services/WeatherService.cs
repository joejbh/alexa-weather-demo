using System.Net.Http.Headers;
using Amazon.Lambda.Core;

namespace weatherDemo;

public interface IWeatherService
{
  Task<Weather> GetWeatherByZipAsync(string zip);
}

public class WeatherService : IWeatherService
{

  private readonly HttpClient client;
  private readonly ILambdaLogger log;

  public WeatherService(ILambdaLogger _log)
  {
    log = _log;

    client = new HttpClient
    {
      BaseAddress = new Uri("https://api.weatherbit.io/v2.0"),
    };

    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
  }


  public async Task<Weather> GetWeatherByZipAsync(string zip)
  {
    log.LogLine("Fetching weather data from external service");
    var result = zip == "11111" ?
      new Weather
      {
        Zip = zip,
        HighTemp = 98.3,
        LowTemp = 70.2,
      } :
      new Weather
      {
        Zip = zip,
        HighTemp = 83.7,
        LowTemp = 63.2,
      };

    return await Task.FromResult(result);

    // var key = Environment.GetEnvironmentVariable("WEATHER_BIT_KEY");
    // var w = await client.GetFromJsonAsync<WeatherResponse>($"/forecast/daily?postal_code={zip}&units=I&key={key}");

    // var todayWeather = (w?.data?.FirstOrDefault()) ?? throw new Exception("There was an error fetching the weather data");

    // return new Weather
    // {
    //   Zip = zip,
    //   HighTemp = Convert.ToDouble(todayWeather.high_temp),
    //   LowTemp = Convert.ToDouble(todayWeather.low_temp),
    // };
  }
}

public class Weather
{
  public string? Zip { get; set; }
  public double HighTemp { get; set; }
  public double LowTemp { get; set; }
}

public class WeatherResponse
{
  public WeatherResponseData[]? data { get; set; }

}

public class WeatherResponseData
{
  public string? high_temp { get; set; }
  public string? low_temp { get; set; }
}