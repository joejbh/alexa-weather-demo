using Alexa.NET.Request.Type;

class StopIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "AMAZON.StopIntent";

  public bool ShouldEndSession => true;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("Ok. Shutting Down.");
}
