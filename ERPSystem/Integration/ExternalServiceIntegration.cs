using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ERPSystem.Integration
{
    public class ExternalServiceIntegration
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExternalServiceIntegration(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response to specified type
                return JsonConvert.DeserializeObject<T>(responseBody)!;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");

                throw;
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response to specified type
                return JsonConvert.DeserializeObject<T>(responseBody)!;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
        }

    }
}
