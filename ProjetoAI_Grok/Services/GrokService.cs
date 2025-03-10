using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

public class GrokService
{
    private readonly string _apiKey;
    private readonly RestClient _client;

    public GrokService(IConfiguration configuration)
    {
        _apiKey = configuration["Grok:ApiKey"];
        _client = new RestClient("https://api.x.ai/v1/chat/completions");
    }

    public async Task<string> SendMessageAsync(string prompt)
    {
        var requestBody = new
        {
            messages = new[]
            {
                new { role = "system", content = "You are Grok, a chatbot inspired by the Hitchhikers Guide to the Galaxy." },
                new { role = "user", content = prompt }
            },
            model = "grok-2-latest",
            stream = false,
            temperature = 0
        };

        var request = new RestRequest("", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", $"Bearer {_apiKey}");
        request.AddJsonBody(requestBody);

        var response = await _client.ExecuteAsync(request);
        return response.Content;
    }
}
