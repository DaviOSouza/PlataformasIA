using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

public class GeminiService
{
    private readonly string _apiKey;
    private readonly RestClient _client;

    public GeminiService(IConfiguration configuration)
    {
        _apiKey = configuration["Gemini:ApiKey"];
        _client = new RestClient("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent");
    }

    public async Task<string> SendMessageAsync(string prompt)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[] { new { text = prompt } }
                }
            }
        };

        var request = new RestRequest($"?key={_apiKey}", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(requestBody);

        var response = await _client.ExecuteAsync(request);
        return response.Content;
    }
}
