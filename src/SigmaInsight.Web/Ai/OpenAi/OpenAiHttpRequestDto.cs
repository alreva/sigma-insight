using System.Text.Json.Serialization;

namespace SigmaInsight.Web.Ai.OpenAi;

public class OpenAiHttpRequestDto
{
    public required string Model { get; init; }
    public required Message[] Messages { get; init; }
    public required decimal Temperature { get; init; }
    
    [JsonPropertyName("max_tokens")]
    public required int MaxTokens { get; init; }
}