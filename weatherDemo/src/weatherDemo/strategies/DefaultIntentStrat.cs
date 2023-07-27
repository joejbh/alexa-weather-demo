using Alexa.NET.Request.Type;

class DefaultIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => true;

  public bool ShouldEndSession => false;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("I'm sorry.  I didn't understand your request.  Please provide a 5 digit zip code");
}
