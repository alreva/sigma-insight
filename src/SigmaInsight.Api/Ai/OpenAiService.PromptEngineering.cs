namespace SigmaInsight.Api.Ai;

public partial class OpenAiService
{
    private sealed record Position(string Person, string Title);

    private static readonly string Context =
        "You are a chatbot that can answer questions about Sigma Software. " +
        new Position[]
            {
                new("Valery Krasovsky", "CEO and a co-founder"),
                new("Anatolii Kochetov", "Executive Vice President"),
            }
            .Select(p =>
                $"Based on the structured data about Sigma Software, {p.Person} is The {p.Title}. " +
                $"Generate a brief description that provides this information " +
                $"to a user inquiring about the {p.Title} of Sigma Software")
            .AsConcatenatedString(". ");
    
    public async Task<string> QueryWithPromptEngineering(string prompt)
    {
        var response = await openAi.ExecuteQuery(Context, prompt);
        return response.Choices?[^1].Message?.Content ?? "";
    }
}