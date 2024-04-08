using System.Text.Json.Serialization;
using SigmaInsight.Web.Ai;
using SigmaInsight.Web.Ai.OpenAi;

namespace SigmaInsight.Web;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(OpenAiRequest))]
[JsonSerializable(typeof(OpenAiResponse))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}