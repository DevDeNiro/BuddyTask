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
    ADD_TODO(state, task) {
      state.tasks.push(task);
    },
    DELETE_TODO(state, taskId) {
      state.tasks = state.tasks.filter((task) => task.id !== taskId);
    },
    UPDATE_DATA(state, updatedData) {
      state.data = state.data.map((item) => {
        if (item.id === updatedData.id) {
          return {...item, ...updatedData};
        }
        return item;
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

    addTask({commit}, task) {
      axios
        .post("/api/tasks", task)
        .then((response) => {
          commit("ADD_TODO", response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    deleteTask({commit, dispatch}, task) {
      axios
        .delete("/api/tasks", {params: {id: task}})
        .then((response) => {
          commit("DELETE_TODO", response.data);
          dispatch("fetchTasks");
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateTask({commit}, updatedTask) {
      console.log(updatedTask);
      axios
        .put(`/api/tasks/${updatedTask.id}`, updatedTask.data)
        .then((response) => {
          commit("UPDATE_TODO", response.data.updatedTask);
          console.log(response);
          alert("OK");
        })
        .catch((error) => {
          console.error("Erreur lors de la mise à jour de la tâche :", error);
          alert("Erreur lors de la mise à jour de la tâche.");
        });
    },
  },

  getters: {
    tasks: (state) => state.tasks,
  },
});

export default store;
