using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/openai")]
public class OpenAIController : ControllerBase
{
    private readonly OpenAIService _openAIService;

    public OpenAIController(OpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var response = await _openAIService.SendMessageAsync(request.Prompt);
        return Ok(response);
    }
}

public class ChatRequest
{
    public string Prompt { get; set; }
}
