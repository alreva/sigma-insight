using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SigmaInsight.Api.Ai;

public partial class OpenAiService(OpenAiClient openAi)
{
    public async Task<string> QueryWithNoRefinement(string prompt)
    {
        const string context = """
                               You are a chatbot that can answer questions
                               about Sigma Software
                               """;
        var response = await openAi.ExecuteQuery(context, prompt);
        return response.Choices?[^1].Message?.Content ?? "";
    }
}