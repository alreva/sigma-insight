namespace SigmaInsight.Web.Ai.OpenAi;

public class OpenAiRequest
{
    public required string Context { get; init; }
    public required string Prompt { get; init; }
    public string Model { get; init; } = "gpt-3.5-turbo";
}