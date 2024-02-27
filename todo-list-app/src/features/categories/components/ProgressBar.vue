<template>
  <div class="progress">
    <div
        :style="{ width: progress + '%' }"
        class="progress-bar bg-purple-600"
    ></div>
  </div>
</template>

<script>
export default {
  props: {
    completed: Number,
    total: Number,
  },
  computed: {
    progress() {
      return this.total > 0 ? (this.completed / this.total) * 100 : 0;
    },
  },
};
</script>

<style scoped>

.progress {
  width: 100%;
  background-color: #eee;
  border-radius: 4px;
  overflow: hidden;
}

.progress-bar {
  height: 10px;
  background-color: orange;
  transition: width 0.5s ease-in-out;
}

</style>
