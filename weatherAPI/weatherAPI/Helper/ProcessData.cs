using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weatherAPI.Model;

namespace weatherAPI.Helper
{
    public class ProcessData
    {
        public CurrentWeather ProcessCurrent(dynamic apiData)
        {
            //decimal kelvin = 273.15;
            var currentData = new CurrentWeather();
            currentData.Temperature = apiData.main.temp - 273.15;//Convert to C
            currentData.Min_Temperature = apiData.main.temp_min-273.15;//Convert to C
            currentData.Max_Temperature = apiData.main.temp_max-273.15;//Convert to C
            currentData.WindSpeed = apiData.wind.speed;
            currentData.Humidity = apiData.main.humidity;
            currentData.Pressure = apiData.main.pressure;
            currentData.Icon = apiData.weather[0].icon;
            currentData.Description = apiData.weather[0].description;
            currentData.City = apiData.name;
            currentData.Country = apiData.sys.country;
            currentData.SunRise = apiData.sys.sunrise;
            currentData.SunSet = apiData.sys.sunset;
            return currentData;
        }
        public ForecastWeather ProcessForecast(dynamic apiData)
        {
            DateTime currentDT = apiData.list[0].dt_txt;
            DateTime targetDT = currentDT.AddDays(1);
            int counter = 0;
            while (currentDT.Date.Day != targetDT.Date.Day)
            {
                currentDT=currentDT.AddHours(3);
                counter++;
            }
            counter = counter + 4;//Starting Index for forecast
            ForecastWeather forecast = new ForecastWeather();
            forecast.Temperature = new decimal[5];
            forecast.Min_Temperature = new decimal[5];
            forecast.Max_Temperature = new decimal[5];
            forecast.Humidity = new decimal[5];
            forecast.Pressure = new decimal[5];
            forecast.WindSpeed = new decimal[5];
            forecast.Description = new string[5];
            forecast.Icon = new string[5];
            forecast.DateStamp = new DateTime[5];
            forecast.DayOfTheWeek = new string[5];
            for(int i = 0; i < 5; i++)
            {
                if(i==4 && counter>39)
                {
                    //reset counter to take last instance
                    counter = 39;
                }
                forecast.Temperature[i] = apiData.list[counter].main.temp - 273.15;//Convert to C;
                forecast.Min_Temperature[i] = apiData.list[counter].main.temp_min - 273.15;//Convert to C;
                forecast.Max_Temperature[i] = apiData.list[counter].main.temp_max - 273.15;//Convert to C;
                forecast.Humidity[i] = apiData.list[counter].main.humidity;
                forecast.Pressure[i] = apiData.list[counter].main.pressure;
                forecast.WindSpeed[i] = apiData.list[counter].wind.speed;
                forecast.Description[i] = apiData.list[counter].weather[0].description;
                forecast.Icon[i] = apiData.list[counter].weather[0].icon;
                forecast.DateStamp[i] = apiData.list[counter].dt_txt;
                forecast.DayOfTheWeek[i] = forecast.DateStamp[i].DayOfWeek.ToString();
                counter = counter + 8;
            }
            return forecast;
        }
    }
}
