using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/grok")]
public class GrokController : ControllerBase
{
    private readonly GrokService _grokService;

    public GrokController(GrokService grokService)
    {
        _grokService = grokService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var response = await _grokService.SendMessageAsync(request.Prompt);
        return Ok(response);
    }
}

public class ChatRequest
{
    public string Prompt { get; set; }
}
