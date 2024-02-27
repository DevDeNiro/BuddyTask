<template>
  <div id="app">
    <main>
      <router-view></router-view>
    </main>
  </div>
</template>

<script>
import {defineComponent} from "vue";
import Home from "@/views/Home.vue";

export default defineComponent({
  components: {Home}
})

</script>

<style>
html,
body {
  margin: 0;
  padding: 0;
  background-color: #f5f6fa;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  transition: all 0.3s ease;
}
</style>
