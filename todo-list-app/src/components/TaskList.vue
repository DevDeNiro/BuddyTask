<style></style>

<template>
  <div class="self-center w-10/12 pt-5">
    <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
      completed
    </h3>
    <TodoCard>
      <div v-for="todo in completeTodos" :key="todo.id">
        {{ todo.completed }}
      </div>
    </TodoCard>
    <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
      uncompleted
    </h3>
    <TodoCard>
      <div v-for="todo in uncompleteTodos" :key="todo.id">
        {{ todo.completed }}
      </div>
    </TodoCard>
  </div>
</template>

<script>
import axios from "axios";
import TodoCard from "./TaskDetail.vue";

export default {
  components: {
    TodoCard,
  },

  data() {
    return {
      completeTodos: [],
      uncompleteTodos: [],
    };
  },

  mounted() {
    axios
      .get("/api/Todo")
      .then((response) => {
        const todos = response.data;
        const completeTodos = todos.filter((todo) => todo.completed === true);
        const uncompleteTodos = todos.filter(
          (todo) => todo.completed === false
        );
        this.completeTodos = completeTodos;
        this.uncompleteTodos = uncompleteTodos;
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
</script>
