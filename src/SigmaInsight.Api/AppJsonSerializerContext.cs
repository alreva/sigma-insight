using System.Text.Json.Serialization;

namespace SigmaInsight.Api;

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}