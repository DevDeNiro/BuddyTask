import {createStore} from "vuex";
import axios from "axios";

const store = createStore({
  state: {
    tasks: [],
    message: "Hello world!",
  },
  mutations: {
    // define your mutations here
    SET_TODOS(state, tasks) {
      state.tasks = tasks;
    },
    ADD_TODO(state, todo) {
      state.tasks.push(todo);
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
