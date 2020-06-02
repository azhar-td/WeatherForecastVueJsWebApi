<template>
  <div>
    <b-row>
      <b-col class="col-md-6 col-sm-6 col-xs-12 col-lg-6">
        <form v-on:submit.prevent="getCurrentWeather">
          <div class="input-group">
            <input
              type="text"
              class="form-control"
              v-model="searchTxt"
              name="searchTxt"
              id="searchTxt"
              placeholder="Search weather in DE by city & zip"
            />
            <div class="input-group-append">
              <button v-on:click="getCurrentWeather" class="btn btn-secondary" type="button">Search</button>
            </div>
          </div>
        </form>
        <b-card-group deck>
          <b-card
            v-bind:header="currentWeather.headerString+' ('+currentWeather.description+')'"
            class="text-center"
          >
            <b-card-text>
              <div>
                <h3>
                  <img v-bind:src="currentWeather.actual_icon_url" />
                  {{currentWeather.temperature}} °C
                </h3>
              </div>

              <table>
                <tbody>
                  <tr>
                    <td>
                      <strong>Wind</strong>
                    </td>
                    <td>{{currentWeather.wind_speed}}m/s</td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Min Temperature</strong>
                    </td>
                    <td>{{currentWeather.min_temp}}°C</td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Max Temperature</strong>
                    </td>
                    <td>{{currentWeather.max_temp}}°C</td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Humidity</strong>
                    </td>
                    <td>{{currentWeather.humidity}}%</td>
                  </tr>
                </tbody>
              </table>
            </b-card-text>
          </b-card>
        </b-card-group>
      </b-col>
      <b-col class="col-md-6 col-sm-6 col-xs-12 col-lg-6">
        <TempChart v-bind:highChartObj="highChartObj"/>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <ForecastWeather v-bind:foreCastWeatherArr="foreCastWeatherArr" />
      </b-col>
    </b-row>
  </div>
</template>

<script>
import TempChart from "./TempChart";
import ForecastWeather from "./ForecastWeather";
import axios from "axios";
import { async } from "q";
const icon_url = "http://openweathermap.org/img/wn/$@2x.png";
const currentApiUrl = "http://localhost:61133/api/weather/@/#";
const forecastApiUrl = "http://localhost:61133/api/weather/@/#";
//axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
export default {
  components: {
    ForecastWeather,
    TempChart
  },
  data() {
    return {
      cardFooter: "",
      searchTxt: localStorage.searchTxt||'',
      currentWeather: {
        temperature: "",
        min_temp: "",
        max_temp: "",
        humidity: "",
        pressure: "",
        city: "",
        country: "",
        sun_rise: "",
        sun_set: "",
        wind_speed: "",
        icon: "",
        description: "",
        headerString: "",
        actual_icon_url: ""
      },
      foreCastWeather: {
        temperature: "",
        min_temp: "",
        max_temp: "",
        humidity: "",
        wind: "",
        pressure: "",
        description: "",
        icon: "",
        dayOfTheWeek: "",
        actual_icon_url: ""
      },
      foreCastWeatherArr: [],
      highChartObj:{
          min_tempArr:[],
          max_tempArr:[],
          dayOfTheWeekArr:[]
      }
    };
  },
  methods: {
    async getCurrentWeather() {
      try {
        var apiUrl = "";
        if (isNaN(this.searchTxt)) {
          apiUrl = currentApiUrl.replace("@", "GetByCity");
          apiUrl = apiUrl.replace("#", this.searchTxt);
        } else if (!isNaN(this.searchTxt)) {
          apiUrl = currentApiUrl.replace("@", "GetByZip");
          apiUrl = apiUrl.replace("#", this.searchTxt);
        }
        var result = await axios.get(apiUrl);
        this.currentWeather.temperature = parseFloat(
          result.data.temperature
        ).toFixed(2);
        this.currentWeather.min_temp = parseFloat(
          result.data.min_Temperature
        ).toFixed(2);
        this.currentWeather.max_temp = parseFloat(
          result.data.max_Temperature
        ).toFixed(2);
        this.currentWeather.humidity = result.data.humidity;
        this.currentWeather.pressure = result.data.pressure;
        this.currentWeather.city = result.data.city;
        this.currentWeather.country = result.data.country;
        this.currentWeather.sun_rise = result.data.sunRise;
        this.currentWeather.sun_set = result.data.sunSet;
        this.currentWeather.wind_speed = result.data.windSpeed;
        this.currentWeather.icon = result.data.icon;
        this.currentWeather.description = result.data.description;
        this.currentWeather.actual_icon_url = icon_url.replace(
          "$",
          this.currentWeather.icon
        );
        this.currentWeather.headerString =
          "Weather in " +
          this.currentWeather.city +
          ", " +
          this.currentWeather.country;
        this.getForecastWeather();
        //store in local storage
        localStorage.searchTxt=this.searchTxt;
      } catch (error) {
        console.log("Error", error);
      }
    },
    async getForecastWeather() {
      this.foreCastWeatherArr = [];
      var apiUrl = "";
      if (isNaN(this.searchTxt)) {
        apiUrl = forecastApiUrl.replace("@", "getForecastByCity");
        apiUrl = apiUrl.replace("#", this.searchTxt);
      } else if (!isNaN(this.searchTxt)) {
        apiUrl = currentApiUrl.replace("@", "getForecastByZip");
        apiUrl = apiUrl.replace("#", this.searchTxt);
      }
      var result = await axios.get(apiUrl);
      for (var i = 0; i < 5; i++) {
        this.foreCastWeather = new Object();
        this.foreCastWeather.temperature = parseFloat(
          result.data.temperature[i]
        ).toFixed(2);
        this.foreCastWeather.min_temp = parseFloat(
          result.data.min_Temperature[i]
        ).toFixed(2);
        this.foreCastWeather.max_temp = parseFloat(
          result.data.max_Temperature[i]
        ).toFixed(2);
        this.foreCastWeather.humidity = result.data.humidity[i];
        this.foreCastWeather.wind = result.data.windSpeed[i];
        this.foreCastWeather.pressure = result.data.pressure[i];
        this.foreCastWeather.description = result.data.description[i];
        this.foreCastWeather.icon = result.data.icon[i];
        this.foreCastWeather.actual_icon_url = icon_url.replace(
          "$",
          result.data.icon[i]
        );
        this.foreCastWeather.dayOfTheWeek = result.data.dayOfTheWeek[i];
        this.foreCastWeatherArr.push(this.foreCastWeather);
      }
      //Populate Highcharts data
      this.highChartObj.min_tempArr=result.data.min_Temperature;
      this.highChartObj.max_tempArr=result.data.max_Temperature;
      this.highChartObj.dayOfTheWeekArr=result.data.dayOfTheWeek;
    }
  },
  mounted() {
    //this.getCurrentWeather();
    if(localStorage.searchTxt!='' && localStorage.searchTxt!=undefined){
        this.getCurrentWeather();
    }
    else{
        //App start with Berlin if no input before
        this.searchTxt='Berlin'; 
        this.getCurrentWeather();
    }
  }
};
</script>

<style scoped>
table {
  background: #f5f5f5;
  border-collapse: separate;
  box-shadow: inset 0 1px 0 #fff;
  font-size: 12px;
  line-height: 24px;
  margin: 0px auto;
  text-align: left;
  width: 100%;
}
td {
  border-right: 1px solid #fff;
  border-left: 1px solid #e8e8e8;
  border-top: 1px solid #fff;
  border-bottom: 1px solid #e8e8e8;
  padding: 10px 15px;
  position: relative;
  transition: all 300ms;
  width: 50%;
}

td:first-child {
  box-shadow: inset 1px 0 0 #fff;
}

td:last-child {
  border-right: 1px solid #e8e8e8;
  box-shadow: inset -1px 0 0 #fff;
}

tr {
  background: #f1f1f1;
}

tr:nth-child(odd) td {
  background: #f1f1f1;
}

tr:last-of-type td {
  box-shadow: inset 0 -1px 0 #fff;
}

tr:last-of-type td:first-child {
  box-shadow: inset 1px -1px 0 #fff;
}

tr:last-of-type td:last-child {
  box-shadow: inset -1px -1px 0 #fff;
}
.has-search .form-control {
  padding-left: 2.375rem;
}

.has-search .form-control-feedback {
  position: absolute;
  z-index: 2;
  display: block;
  width: 2.375rem;
  height: 2.375rem;
  line-height: 2.375rem;
  text-align: center;
  pointer-events: none;
  color: #aaa;
}
</style>