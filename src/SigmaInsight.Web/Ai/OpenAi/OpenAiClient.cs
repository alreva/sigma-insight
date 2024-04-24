using System.Text.Json;

namespace SigmaInsight.Web.Ai.OpenAi;

public class OpenAiClient(
    HttpClient httpClient,
    OpenAiSettings settings,
    ILogger<OpenAiClient> logger)
{
    public async Task<OpenAiResponse> ExecuteQuery(OpenAiRequest openAiRequest)
    {
        logger.LogInformation("Base address: {BaseAddress}", httpClient.BaseAddress);

        OpenAiHttpRequestDto httpRequestDtoData = new()
        {
            Model = openAiRequest.Model ?? "gpt-3.5-turbo", // or any other model
            Messages = new[]
            {
                new Message("system", openAiRequest.Context),
                new Message("user", openAiRequest.Prompt),
            },
            Temperature = settings.Temperature,
            MaxTokens = 800
        };
        
        logger.LogInformation(
            "Request data: {@Request}",
            JsonSerializer.Serialize(httpRequestDtoData, AppJsonSerializerContext.Default.OpenAiHttpRequestDto));
        using var httpResponse = await httpClient.PostAsJsonAsync(
            "",
            httpRequestDtoData,
            AppJsonSerializerContext.Default.OpenAiHttpRequestDto);
        httpResponse.EnsureSuccessStatusCode();
        var response = (await httpResponse.Content.ReadFromJsonAsync(AppJsonSerializerContext.Default.OpenAiResponse))!;
        logger.LogInformation(
            "Response data: {@Response}",
            JsonSerializer.Serialize(response, AppJsonSerializerContext.Default.OpenAiResponse));
        return response;
    }
}