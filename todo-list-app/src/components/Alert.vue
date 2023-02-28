<template>
  <div v-if="show" :class="[type]">
    <p>{{ message }}</p>
    <button @click="close">x</button>
  </div>
</template>

<script>
export default {
  props: {
    message: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      default: "error",
    },
  },
  data() {
    return {
      show: true,
    };
  },
  methods: {
    close() {
      this.show = false;
    },
  },
};
</script>

<style>
.error {
  background-color: #f44336;
  color: white;
  padding: 10px;
  margin-bottom: 10px;
}
</style>
