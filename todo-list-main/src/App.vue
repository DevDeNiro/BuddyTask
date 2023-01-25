
<template>
  <div id="app">
  
    <router-view></router-view>
  </div>

</template>

<script>

import addForm from './components/addForm.vue'
import HomeView from './views/HomeView.vue'
import router from './router';

export default {
  name: 'App',
  components: {
    router,
    HomeView,
  }
}
</script>

<style>

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

</style>
