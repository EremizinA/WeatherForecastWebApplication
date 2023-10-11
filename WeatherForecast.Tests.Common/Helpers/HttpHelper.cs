using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Tests.Common.Helpers
{
    public static class HttpHelper
    {
        public static HttpContent? ConvertToStringHttpContent<T>(IEnumerable<T> objectsList)
        {
            var serializedObject = JsonConvert.SerializeObject(objectsList, Formatting.Indented);
            return new StringContent(serializedObject, Encoding.UTF8, "application/json");
        }
    }
}
