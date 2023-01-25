
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
import LoaderSVG from './components/LoaderSVG.vue'
import router from './router';

export default {
  name: 'App',
  components: {
    router,
    HomeView, LoaderSVG
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
