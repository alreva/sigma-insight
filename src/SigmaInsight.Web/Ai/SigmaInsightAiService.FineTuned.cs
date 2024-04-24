using SigmaInsight.Web.Ai.OpenAi;

namespace SigmaInsight.Web.Ai;

partial class SigmaInsightAiService
{
    public async Task<string> QueryFineTuned(string prompt)
    {
        var response = await openAi.ExecuteQuery(new OpenAiRequest {
            Model = settings.FineTunedModelName,
            Context = "You are a chat bot that answers questions about Sigma Software",
            Prompt = prompt
        });
        return response.Choices?[^1].Message?.Content ?? "";
    }
}