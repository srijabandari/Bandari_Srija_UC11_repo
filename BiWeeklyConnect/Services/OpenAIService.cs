namespace BiWeeklyConnect.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public OpenAIService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }
        public async Task<string> AnalyzeSummaryAsync(string summary) { 
            var payload = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = $"Analyze the following meeting summary: {summary}" }
                },
                //max_tokens = 500
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_configuration["OpenAI:ApiKey"]}");
            request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = System.Text.Json.JsonDocument.Parse(jsonResponse);
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;
        }
    }
}
