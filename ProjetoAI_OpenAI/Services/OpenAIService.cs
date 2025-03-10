using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class OpenAIService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public OpenAIService(IConfiguration configuration)
    {
        _apiKey = configuration["OpenAI:ApiKey"];
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> SendMessageAsync(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-4o-mini",  
            messages = new[] { new { role = "user", content = prompt } }
        };

        var requestJson = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");       

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        var responseString = await response.Content.ReadAsStringAsync();

        return responseString;
    }
}
