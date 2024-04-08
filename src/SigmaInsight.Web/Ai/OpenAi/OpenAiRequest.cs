using System.Text.Json.Serialization;

namespace SigmaInsight.Web.Ai.OpenAi;

public class OpenAiRequest
{
    public required string Model { get; init; }
    public required Message[] Messages { get; init; }
    public required double Temperature { get; init; }
    
    [JsonPropertyName("max_tokens")]
    public required int MaxTokens { get; init; }
}