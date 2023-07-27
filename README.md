# alexa-weather-demo

Returns today's high and low temperatures for a given zip code

Details
* I used a modified strategy pattern for lower cyclomatic complexity, better testability, and extensibility.  Switch statements (which you see in the tutorials) can often turn into a terrible mess over time.
* I encapsulated the http requests to weatherbit in a WeatherService.  This allows me to swap out the implementation, but keep the contracts later on if I want to.  Good encapsulation also makes tests easier to write.
* The key for weatherbit comes from environment variables.