using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weatherAPI.Model
{
    public class ForecastWeather
    {
        public decimal[] Temperature { get; set; }
        public decimal[] Min_Temperature { get; set; }
        public decimal[] Max_Temperature { get; set; }
        public decimal[] Humidity { get; set; }
        public decimal[] Pressure { get; set; }
        public decimal[] WindSpeed { get; set; }
        public string[] Description { get; set; }
        public string[] Icon { get; set; }
        public DateTime[] DateStamp { get; set; }
        public string[] DayOfTheWeek { get; set; }
    }
}
