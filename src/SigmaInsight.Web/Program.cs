using SigmaInsight.Web.Ai;
using SigmaInsight.Web.Ai.OpenAi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("appsettings.runtime.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SigmaInsightAiService>();
builder.Services.AddHttpClient<OpenAiClient>();

var openAiSettings = builder
    .Configuration
    .GetSection("OpenAI")
    .Get<OpenAiSettings>()!;
builder.Services.AddTransient(svcs =>
    svcs.GetRequiredService<IConfiguration>()
        .GetSection("OpenAI")
        .Get<OpenAiSettings>()!);

builder.Services.AddHttpClient<OpenAiClient>(client =>
{
    client.BaseAddress = new Uri(openAiSettings.BaseAddress);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiSettings.ApiKey}");
});

builder.Services.AddTransient(svcs =>
    svcs.GetRequiredService<IConfiguration>()
        .GetSection("SigmaInsightAI")
        .Get<SigmaInsightAiSettings>()!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
