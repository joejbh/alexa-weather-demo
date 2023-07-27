using Alexa.NET.Request.Type;

interface IIntentStrategy
{
  bool IsMatch(IntentRequest intentRequest);
  bool ShouldEndSession { get; }
  Task<string> Run(IntentRequest intentRequest);
}
