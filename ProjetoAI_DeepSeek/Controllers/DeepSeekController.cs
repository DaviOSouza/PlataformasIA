using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/deepseek")]
public class DeepSeekController : ControllerBase
{
    private readonly DeepSeekService _deepSeekService;

    public DeepSeekController(DeepSeekService deepSeekService)
    {
        _deepSeekService = deepSeekService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var response = await _deepSeekService.SendMessageAsync(request.Prompt);
        return Ok(response);
    }
}

public class ChatRequest
{
    public string Prompt { get; set; }
}
