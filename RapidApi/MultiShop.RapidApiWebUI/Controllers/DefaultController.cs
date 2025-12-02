using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class DefaultController : Controller
    {
        public async Task<IActionResult> WeatherDetail()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://the-weather-api.p.rapidapi.com/api/weather/konya"),
                Headers =
                {
                    { "x-rapidapi-key", "d07417e024mshbc08186d6bb4080p107969jsnb7f7cb15e5f3" },
                    { "x-rapidapi-host", "the-weather-api.p.rapidapi.com" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<WeatherViewModel.Rootobject>(body);
                ViewBag.temp = values.data.temp;
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> Exchange()
        {
            var client1 = new HttpClient();
            var request1 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=USD&to_symbol=TRY&language=en"),
                Headers =
                {
                    { "x-rapidapi-key", "d07417e024mshbc08186d6bb4080p107969jsnb7f7cb15e5f3" },
                    { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
                },
            };

            using (var response = await client1.SendAsync(request1))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ExchangeViewModel.Rootobject>(body);
                ViewBag.exchangeRateUSD = values.data.exchange_rate;
            }

            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=EUR&to_symbol=TRY&language=en"),
                Headers =
                {
                    { "x-rapidapi-key", "d07417e024mshbc08186d6bb4080p107969jsnb7f7cb15e5f3" },
                    { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
                },
            };

            using (var response = await client2.SendAsync(request2))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ExchangeViewModel.Rootobject>(body);
                ViewBag.exchangeRateEUR = values.data.exchange_rate;
                return View();
            }


            return View();
        }
    }
}
