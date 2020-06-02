<template>
<div>
<div id="temp">

</div>
</div>
</template>

<script>
export default {
    props:['highChartObj'],
      created() {

      },
     data(){
        return{
            child:''
        }
    },
    methods:{
        renderHC(){
            Highcharts.chart('temp', {
    chart: {
        type: 'line'
    },
    title: {
        text: 'Average Temperature'
    },
    subtitle: {
        text: 'Source: OpenWeatherMap'
    },
    xAxis: {
        categories: this.highChartObj.dayOfTheWeekArr
    },
    yAxis: {
        title: {
            text: 'Temperature (°C)'
        }
    },
    plotOptions: {
        line: {
            dataLabels: {
                enabled: true
            },
            enableMouseTracking: false
        }
    },
    series: [{
        name: 'Min',
        data: this.highChartObj.min_tempArr
    }, {
        name: 'Max',
        data: this.highChartObj.max_tempArr
    }]
});
        }
    },
    mounted(){
        this.renderHC();
    this.$watch(
    	() => {
        this.renderHC();
      	return this.highChartObj;
      },
      (newVal, oldVal) => {
      	console.log(newVal);
      },
      {
        deep: true
      }
    )
    }   
}
</script>

<style scoped>

</style>