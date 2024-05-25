using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;
using Newtonsoft.Json.Linq; // Add this line
using RestSharp;


namespace Task2.Services
{
    public class WeatherService
    {
        private readonly string _apiKey = "6a3359c4ea44e03f3c585d14ca8f4227";
        private readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<WeatherModel> GetWeatherAsync(string cityName)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest() { Method = Method.Get };
            request.AddParameter("q", cityName);
            request.AddParameter("appid", _apiKey);
            request.AddParameter("units", "metric");

            var response = await client.ExecuteAsync(request);
            var content = JObject.Parse(response.Content);

            try
            {
                return new WeatherModel
                {
                    Description = content["weather"][0]["description"].ToString(),
                    Temperature = float.Parse(content["main"]["temp"].ToString()),
                    Humidity = int.Parse(content["main"]["humidity"].ToString())
                };

            }
            catch (Exception ex)
            {
                return new WeatherModel
                {
                    Description = "",
                    Temperature = 0,
                    Humidity = 0
                };
            }

        }
    }
}
