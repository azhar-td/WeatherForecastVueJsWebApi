using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using weatherAPI.Helper;

namespace weatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class WeatherController : ControllerBase
    {
        private ProcessData _process;
        private IConfiguration _configuration;
        private string app_key;
        public WeatherController(IConfiguration iConfig)
        {
            _process = new ProcessData();
            _configuration = iConfig;
            app_key = _configuration.GetSection("OpenWeatherMap").GetSection("AppId").Value;
        }
        // GET api/weather
        [HttpGet]
        [Route("GetByCity/{city}")]
        public object GetByCity(string city)
        {
            //get url for current weather api from appsettings.json
            string url = _configuration.GetSection("OpenWeatherMap").GetSection("CurrentWeatherBaseURLByCity").Value;
            url = url.Replace("{city}", city);
            url = url.Replace("{appId}", app_key);
            try
            {
                dynamic liveApiData = LiveApiData(url);
                var current = _process.ProcessCurrent(liveApiData);
                return current;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
                    // GET api/weather
        [HttpGet]
        [Route("GetByZip/{zip}")]
        public object GetByZip(string zip)
        {
            //get url for current weather api from appsettings.json
            string url = _configuration.GetSection("OpenWeatherMap").GetSection("CurrentWeatherBaseURLByZip").Value;
            url = url.Replace("{zip}", zip);
            url = url.Replace("{appId}", app_key);
            try
            {
                dynamic liveApiData = LiveApiData(url);
                var current = _process.ProcessCurrent(liveApiData);
                return current;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        // GET api/weather
        [HttpGet]
        [Route("getForecastByCity/{city}")]
        public object GetForecastByCity(string city)
        {
            //get url for current weather api from appsettings.json
            string url = _configuration.GetSection("OpenWeatherMap").GetSection("ForecastBaseURLByCity").Value;
            url = url.Replace("{city}", city);
            url = url.Replace("{appId}", app_key);
            try
            {
                dynamic liveApiData = LiveApiData(url);
                var forecast = _process.ProcessForecast(liveApiData);
                return forecast;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        // GET api/weather
        [HttpGet]
        [Route("getForecastByZip/{zip}")]
        public object GetForecastByZip(string zip)
        {
            //get url for current weather api from appsettings.json
            string url = _configuration.GetSection("OpenWeatherMap").GetSection("ForecastBaseURLByZip").Value;
            url = url.Replace("{zip}", zip);
            url = url.Replace("{appId}", app_key);
            try
            {
                dynamic liveApiData = LiveApiData(url);
                var forecast = _process.ProcessForecast(liveApiData);
                return forecast;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public dynamic LiveApiData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return JObject.Parse(reader.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    Console.WriteLine(errorText);
                }
                throw;
            }
        }
    }
}