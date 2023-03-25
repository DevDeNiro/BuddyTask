<style></style>

<template>
  <div class="self-center">
    <div>
      <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
        completed
      </h3>
      <TaskDetail :completedTasks="completedTasks"></TaskDetail>
    </div>
    <div>
      <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
        uncompleted
      </h3>
      <!-- <TaskDetail :tasks="incompleteTasks"></TaskDetail> -->
    </div>
  </div>
</template>

<script export>
import TaskDetail from "./TaskDetail.vue";
import {mapActions, mapState} from "vuex";

export default {
  components: {
    TaskDetail,
  },
  computed: {
    ...mapState(["tasks"]),
    completedTasks() {
      return this.tasks.filter((task) => task.completed === true);
    },
  },
  methods: {
    ...mapActions(["fetchTasks"]),
  },
  created() {
    this.fetchTasks();
  },
};
</script>
