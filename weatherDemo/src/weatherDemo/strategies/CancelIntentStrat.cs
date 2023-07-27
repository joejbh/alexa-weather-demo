using Alexa.NET.Request.Type;

class CancelIntentStrat : IIntentStrategy
{
  public bool IsMatch(IntentRequest intentRequest) => intentRequest.Intent.Name == "AMAZON.CancelIntent";

  public bool ShouldEndSession => true;

  public Task<string> Run(IntentRequest intentRequest) => Task.FromResult("Ok. Shutting Down.");
}
