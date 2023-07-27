using Alexa.NET.Request.Type;

class HelpIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "AMAZON.HelpIntent";

  public bool ShouldEndSession => false;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("It sounds like you want help.  I can tell you the high and low temperatures for a given zip code in the USA.  Ask away!");
}
