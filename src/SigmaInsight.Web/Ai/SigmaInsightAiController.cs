using Microsoft.AspNetCore.Mvc;

namespace SigmaInsight.Web.Ai;

[ApiController]
[Route("api/ai")]
public class SigmaInsightAiController(ILogger<SigmaInsightAiController> logger) : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        logger.LogInformation("OpenAI controller");
        return DateTimeOffset.UtcNow.ToString("u");
    }
    
    [HttpPost, Route("no-refinement")]
    public async Task<ActionResult<string>> NoRefinement(
        [FromServices] SigmaInsightAiService sigmaInsightAi,
        [FromBody] OpenAiRequestModel model)
    {
        var result = await sigmaInsightAi.QueryWithNoRefinement(model.Prompt);
        return Ok(new { result });
    }

    [HttpPost, Route("prompt-engineering")]
    public async Task<ActionResult<string>> WithPromptEngineering(
        [FromServices] SigmaInsightAiService sigmaInsightAi,
        [FromBody] OpenAiRequestModel model)
    {
        var result = await sigmaInsightAi.QueryWithPromptEngineering(model.Prompt);
        return Ok(new { result });
    }

    [HttpPost, Route("fine-tuned")]
    public async Task<ActionResult<string>> FineTuned(
        [FromServices] SigmaInsightAiService sigmaInsightAi,
        [FromBody] OpenAiRequestModel model)
    {
        var result = await sigmaInsightAi.QueryFineTuned(model.Prompt);
        return Ok(new { result });
    }
    
    public class OpenAiRequestModel
    {
        public required string Prompt { get; set; }
    }
}