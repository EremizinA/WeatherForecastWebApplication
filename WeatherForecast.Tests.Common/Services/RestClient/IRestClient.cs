using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Tests.Common.Services
{
    public interface IRestClient<T>
    {
        Task<T> GetAsync(string url);
    }
}
