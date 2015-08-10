using System.Collections.Generic;
using System.Web.Http;

namespace SuperPowersWeatherAPI.Controllers
{
    public class WeatherController : ApiController
    {
        private static List<WeatherData> weatherData = new List<WeatherData>();


        public List<WeatherData> Get()
        {
            return weatherData;
        }

        public void Post([FromBody]WeatherData value)
        {
            weatherData.Add(value);
        }

        public void Delete()
        {
            weatherData = new List<WeatherData>();
        }
    }

    public class WeatherData
    {
        public string DeviceID { get; set; }
        public float Humidity { get; set; }
        public float Temperature { get; set; }
    }
}
