import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    todos: [],
  },
  mutations: {
    SET_TODOS(state, todos) {
      state.todos = todos;
    },
    ADD_TODO(state, todo) {
      state.todos.push(todo);
    },
  },
  actions: {
    fetchTasks({commit}) {
      axios
        .get("/api/tasks")
        .then((response) => {
          commit("SET_TODOS", response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    addTask({commit}, todo) {
      axios
        .post("/api/tasks", todo)
        .then((response) => {
          commit("ADD_TODO", response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  getters: {
    todos: (state) => state.todos,
  },
});

export default store;
