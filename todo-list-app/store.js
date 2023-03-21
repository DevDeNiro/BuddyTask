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
      state.tasks = state.tasks.filter((task) => task.Id !== taskId);
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

    addTask({commit, dispatch}, taskId) {
      axios
        .post(`/api/tasks/${taskId}`, taskId)
        .then((response) => {
          commit("ADD_TODO", response.data);
          // dispatch("fetchTasks");
          alert("OK");
        })
        .catch((error) => {
          console.log(error);
          alert("KO");
        });
    },
    deleteTask({commit}, taskId) {
      axios
        .delete(`/api/tasks/${taskId}`)
        .then((response) => {
          commit("DELETE_TODO", response.data.Id);
          console.log(response.data);
          alert("OK");
        })
        .catch((error) => {
          console.log(error);
          alert("KO");
        });
    },
    updateTask({commit}, updatedTask) {
      console.log(updatedTask);

      axios
        .put(`/api/tasks/${updatedTask.id}`, updatedTask)
        .then((response) => {
          commit("UPDATE_TODO", response.data.updatedTask);
          console.log(response);
          alert("OK");
        })
        .catch((error) => {
          alert("KO");
        });
    },
  },

  getters: {
    tasks: (state) => state.tasks,
  },
});

export default store;
