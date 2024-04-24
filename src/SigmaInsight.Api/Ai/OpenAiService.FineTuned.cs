using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SigmaInsight.Api.Ai;

public partial class OpenAiService
{
    public async Task<string> QueryFineTuned(string prompt)
    {
        var response = await openAi.ExecuteQuery("You are a chat bot that answers questions about Sigma Software", prompt);
        return response.Choices?[^1].Message?.Content ?? "";
    }
}