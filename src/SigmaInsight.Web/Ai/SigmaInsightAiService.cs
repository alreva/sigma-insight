using SigmaInsight.Web.Ai.OpenAi;

namespace SigmaInsight.Web.Ai;

public partial class SigmaInsightAiService(OpenAiClient openAi, SigmaInsightAiSettings settings)
{
    public async Task<string> QueryWithNoRefinement(string prompt)
    {
        const string context = """
                               You are a chatbot that can answer questions
                               about Sigma Software
                               """;
        var response = await openAi.ExecuteQuery(new OpenAiRequest{ Context = context, Prompt = prompt });
        return response.Choices?[^1].Message?.Content ?? "";
    }
}