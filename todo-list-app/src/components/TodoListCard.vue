<style></style>

<template>
  <div class="self-center w-10/12 pt-5">
    <p class="uppercase block text-sm py-3 font-medium text-gray-700">
      completed
    </p>
    <slot></slot>
    <div v-for="todo in completeTodos" :key="todo.id"></div>
    <p class="uppercase block text-sm py-3 font-medium text-gray-700">
      incompleted
    </p>
    <slot></slot>
    <div v-for="todo in incompleteTodos" :key="todo.id"></div>
  </div>
</template>

<script>
import axios from "axios";
import TodoCard from "./TodoCard.vue";

export default {
  components: {
    TodoCard,
  },
  data() {
    return {
      todos: [],
      todoStatus: null,
      completeTodos: [],
      incompleteTodos: [],
    };
  },
  computed: {
    filterUsers() {
      if (this.todoStatus === "complete") {
        this.completeTodos = this.filterByTodoStatus(this.todos, true);
        this.incompleteTodos = this.filterByTodoStatus(this.todos, false);
      } else if (this.todoStatus === "incomplete") {
        this.completeTodos = this.filterByTodoStatus(this.todos, true);
        this.incompleteTodos = this.filterByTodoStatus(this.todos, false);
      } else {
        this.completeTodos = this.filterByTodoStatus(this.todos, true);
        this.incompleteTodos = this.filterByTodoStatus(this.todos, false);
      }
    },
    filterByTodoStatus(todos, isComplete) {
      return todos.filter((user) => user.todo_completed === isComplete);
    },
  },
  mounted() {
    // axios
    //   .get("https://localhost:7174/api/Todo")
    //   .then((response) => {
    //     this.todos = response.data;
    //     // Params a ajouter coté API
    //     this.todoStatus = response.headers["profile-status"];
    //     this.filterTodos();
    //   })
    //   .catch((error) => {
    //     console.log(error);
    //   });
  },
};
</script>
