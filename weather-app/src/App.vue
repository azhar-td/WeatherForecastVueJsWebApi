<template>
  <div id="app">
    <AppHeader/>
    <b-container>
      <Dashboard/>
    </b-container>
  </div>
</template>

<script>
 import AppHeader from './components/AppHeader.vue';
import Dashboard from './components/Dashboard.vue';

export default {
  name: 'app',
  components: {
    AppHeader,
    Dashboard
  }
}
</script>

<style>
body{
  background: url(./images/weather-6.png) repeat 0 0;
  animation:slide 20s linear infinite;
}
@keyframes slide{
  from{background-position: 0 0;}
  to{background-position: -400px 0;}
}
</style>
