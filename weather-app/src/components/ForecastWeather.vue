<template>
  <div>
    <h4 class="weatherH">Weather forecast</h4>
    <div class="row">
      <div class="col-md-2 col-sm-4 col-lg-2 col-xs-12" v-for="item in foreCastWeatherArr">
        <b-card-group deck>
          <b-card v-bind:header="item.dayOfTheWeek" class="text-center">
            <b-card-text>
              <div>
                <h3>
                  <img v-bind:src="item.actual_icon_url" />
                  {{item.temperature}} °C
                </h3>
              </div>
              {{item.description}}
              <b-badge variant="secondary">Wind {{item.wind}} m/s</b-badge>
              <b-badge variant="secondary">Min Temp {{item.min_temp}} °C</b-badge>
              <b-badge variant="secondary">Max Temp {{item.max_temp}} °C</b-badge>
              <b-badge variant="secondary">Humidity {{item.humidity}} %</b-badge>
            </b-card-text>
          </b-card>
        </b-card-group>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["foreCastWeatherArr"],
  data() {
    return {
      child: ""
    };
  },
  mounted() {
      
  }
};
</script>

<style scoped>
.weatherH{
    margin-top: 20px;
}
</style>