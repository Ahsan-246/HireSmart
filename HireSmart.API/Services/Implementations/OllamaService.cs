using System.Net.Http.Json;
using HireSmart.API.Services.Interfaces;

namespace HireSmart.API.Services.Implementation
{
    public class OllamaService : IOllamaService
    {
        private readonly HttpClient httpClient;

        public OllamaService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var request = new
            {
                model = "qwen2.5:3b",
                prompt = prompt,
                stream = false
            };

            var response = await httpClient.PostAsJsonAsync(
                "api/generate",
                request);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content.ReadFromJsonAsync<OllamaResponse>();

            return result?.Response ?? string.Empty;
        }

        private class OllamaResponse
        {
            public string Response { get; set; } = string.Empty;
        }
    }
}