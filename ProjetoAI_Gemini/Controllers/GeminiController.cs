using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/gemini")]
public class GeminiController : ControllerBase
{
    private readonly GeminiService _geminiService;

    public GeminiController(GeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var response = await _geminiService.SendMessageAsync(request.Prompt);
        return Ok(response);
    }
}

public class ChatRequest
{
    public string Prompt { get; set; }
}
