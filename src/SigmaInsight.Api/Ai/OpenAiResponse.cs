using System.Text.Json.Serialization;

namespace SigmaInsight.Api.Ai;

public record OpenAiRequest(string Model, Message[] Messages, double Temperature, int MaxTokens);
public record OpenAiResponse(Choice[]? Choices);

public record Choice(Message? Message);

public record Message(string Role, string? Content);