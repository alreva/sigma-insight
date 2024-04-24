using SigmaInsight.Web.Ai.OpenAi;

namespace SigmaInsight.Web.Ai;

partial class SigmaInsightAiService
{
    public async Task<string> QueryWithPromptEngineering(string prompt)
    {
        var context = settings.PromptEngineeringContext;
        var response = await openAi.ExecuteQuery(new OpenAiRequest{ Context = context, Prompt = prompt });
        return response.Choices?[^1].Message?.Content ?? "";
    }
}