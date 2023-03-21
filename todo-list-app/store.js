import {createStore} from "vuex";
import axios from "axios";

const store = createStore({
  state: {
    tasks: [],
  },
  mutations: {
    SET_TODOS(state, tasks) {
      state.tasks = tasks;
    },
    ADD_TODO(state, todo) {
      state.tasks.push(todo);
    },
    DELETE_TODO(state, taskId) {
      state.tasks = state.tasks.filter((task) => task.id !== taskId);
    },
    UPDATE_TODO(state, updatedTask) {
      state.tasks = state.tasks.map((task) => {
        if (task.id === updatedTask.id) {
          return {...task, ...updatedTask};
        }
        return task;
      });
    },
  },
  actions: {
    fetchTasks({commit}) {
      axios
        .get("/api/tasks")
        .then((response) => {
          commit("SET_TODOS", response.data);
          console.log(response.data);
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
    deleteTask({commit}, taskId) {
      axios
        .delete(`/api/tasks/${taskId}`)
        .then((response) => {
          commit("DELETE_TODO", taskId);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateTask({commit}, updatedTask) {
      axios
        .put(`/api/tasks/${updatedTask.id}`, updatedTask)
        .then((response) => {
          commit("UPDATE_TODO", updatedTask);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  getters: {
    tasks: (state) => state.tasks,
  },
});

export default store;
