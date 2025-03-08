using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.IServices;

namespace WebApi.Services
{
    public class RandomStringGeneratorService : IRandomStringGeneratorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiUrl = "https://codito.io/free-random-code-generator/api/generate";
       // private const string ApiUrl = "https://invalid-url/api/generate";

        public RandomStringGeneratorService
            (
            IHttpClientFactory httpClientFactory
            )
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetRandomProjectCode()
        {
            var requestBody = new
            {
                codesToGenerate = 1,
                onlyUniques = true,
                charactersSets = new string[] { "ABCD", "12345", "ABCD", "12345" },
                prefix = "DV-"
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync(ApiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("External API call failed.");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

              
                var codes = JsonSerializer.Deserialize<string[]>(jsonResponse);

                if (codes != null && codes.Length > 0)
                {
                    return codes[0]; 
                }

                throw new Exception("No project codes returned by external API.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating project code.");
            }
        }
    }
}
