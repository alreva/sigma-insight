using System.Text.Json;

namespace SigmaInsight.Api.Ai;

public class OpenAiClient(HttpClient httpClient, ILogger<OpenAiClient> logger)
{
    public async Task<OpenAiResponse> ExecuteQuery(string context, string prompt)
    {
        logger.LogInformation("Base address: {BaseAddress}", httpClient.BaseAddress);
        
        OpenAiRequest requestData = new (
            "gpt-3.5-turbo", // or any other model
            new[] {
                new Message("system", context),
                new Message("user", prompt),
            },
            0.7,
            800
        );
        
        logger.LogInformation(
            "Request data: {@Request}",
            JsonSerializer.Serialize(requestData, AppJsonSerializerContext.Default.OpenAiRequest));
        using var httpResponse = await httpClient.PostAsJsonAsync(
            "",
            requestData,
            AppJsonSerializerContext.Default.OpenAiRequest);
        httpResponse.EnsureSuccessStatusCode();
        var response = (await httpResponse.Content.ReadFromJsonAsync(AppJsonSerializerContext.Default.OpenAiResponse))!;
        logger.LogInformation(
            "Response data: {@Response}",
            JsonSerializer.Serialize(response, AppJsonSerializerContext.Default.OpenAiResponse));
        return response;
    }
}