using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Tests.Common.Extentions
{
    public static class HttpClientExtentions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content) ?? default;
        }

        public static async Task<T> PostAsync<T>(this HttpClient client, string url, HttpContent? httpContent)
        {
            var response = await client.PostAsync(url, httpContent);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content) ?? default;
        }
    }
}
