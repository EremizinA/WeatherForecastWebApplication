using Newtonsoft.Json;
using WeatherForecast.Tests.Common.Services;

namespace WeatherForecast.Tests.Common.Services
{
    public class RestClient<T> : IRestClient<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public RestClient(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content) ?? default;
        }
    }
}
