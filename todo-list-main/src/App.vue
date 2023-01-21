
<template>
  <div id="app">
    <nav>
        
    </nav>

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
    HomeView
}
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
