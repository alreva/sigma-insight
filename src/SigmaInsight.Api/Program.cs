using Microsoft.AspNetCore.OutputCaching;
using SigmaInsight.Api;
using SigmaInsight.Api.Ai;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOutputCache();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var openAiSettings = builder.Configuration
    .GetSection("OpenAI")
    .Get<OpenAiSettings>()!;
builder.Services.AddHttpClient<OpenAiClient>(client =>
{
    client.BaseAddress = new Uri(openAiSettings.BaseAddress);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiSettings.ApiKey}");
});

var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

app.MapGet("/", () => sampleTodos);
app.MapGet("/{id}",
    [OutputCache(Duration = 5)]
    (int id) =>
    Array.Find(sampleTodos, a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();