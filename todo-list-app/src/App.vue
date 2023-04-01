<template>
  <div id="app">
    <main>
      <router-view></router-view>
      <Home />
    </main>
  </div>
</template>

<script>
import Home from "./views/Home.vue";
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
