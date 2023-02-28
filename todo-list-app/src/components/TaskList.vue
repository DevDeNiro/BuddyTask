<style></style>

<template>
  <div class="self-center">
    <div>
      <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
        completed
      </h3>
      <TaskDetail :tasks="completeTasks"></TaskDetail>
    </div>
    <div>
      <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
        uncompleted
      </h3>
      <TaskDetail :tasks="incompleteTasks"></TaskDetail>
    </div>
  </div>
</template>

<script export>
import axios from "axios";
import TaskDetail from "./TaskDetail.vue";

export default {
  components: {
    TaskDetail,
  },

  data() {
    return {
      completeTasks: [],
      incompleteTasks: [],
    };
  },

  mounted() {
    axios
      .get("/api/Todo")
      .then((response) => {
        response.data.forEach((task) => {
          if (task.completed) {
            this.completeTasks.push(task);
          } else {
            this.incompleteTasks.push(task);
          }
        });
        console.log(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
</script>
