using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

public class DeepSeekService
{
    private readonly string _apiKey;
    private readonly RestClient _client;

    public DeepSeekService(IConfiguration configuration)
    {
        _apiKey = configuration["DeepSeek:ApiKey"];
        _client = new RestClient("https://api.deepseek.com/v1/chat/completions");
    }

    public async Task<string> SendMessageAsync(string prompt)
    {
        var requestBody = new
        {
            model = "deepseek-chat",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            },
            temperature = 0.7
        };

        var request = new RestRequest("", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", $"Bearer {_apiKey}");
        request.AddJsonBody(requestBody);

        var response = await _client.ExecuteAsync(request);
        return response.Content;
    }
}
