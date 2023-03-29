<template>
  <div id="app">
    <main>
      <router-view></router-view>
      <Popup />
    </main>
  </div>
</template>

<script>
import Popup from "./components/Popup.vue";
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
