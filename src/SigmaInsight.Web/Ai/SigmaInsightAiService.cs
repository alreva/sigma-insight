using SigmaInsight.Web.Ai.OpenAi;

namespace SigmaInsight.Web.Ai;

public class SigmaInsightAiService(OpenAiClient openAi, SigmaInsightAiSettings settings)
{
    public async Task<string> QueryWithNoRefinement(string prompt)
    {
        const string context = """
                               You are a chatbot that can answer questions
                               about Sigma Software
                               """;
        var response = await openAi.ExecuteQuery(context: context, prompt: prompt);
        return response.Choices?[^1].Message?.Content ?? "";
    }

    public async Task<string> QueryWithPromptEngineering(string prompt)
    {
        var context = settings.PromptEngineeringContext;
        var response = await openAi.ExecuteQuery(context: context, prompt: prompt);
        return response.Choices?[^1].Message?.Content ?? "";
    }
}