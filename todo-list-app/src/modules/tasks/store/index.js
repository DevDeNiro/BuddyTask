import apiClient from "../../../api";

const state = {
  tasks: [],
};

const mutations = {
  SET_TODO(state, tasks) {
    state.tasks = tasks;
  },

  ADD_TODO(state, task) {
    state.tasks.push(task);
  },

  DELETE_TODO(state, taskId) {
    state.tasks = state.tasks.filter((task) => task.id !== taskId);
  },

  UPDATE_TODO(state, updatedData) {
    state.data = state.data.map((item) =>
      item.id === updatedData.id ? {...item, ...updatedData} : item
    );
  },
};

const actions = {
  fetchTasks({commit}) {
    axios
      .get("/api/tasks")
      .then((response) => {
        commit("SET_TODO", response.data);
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
  updateTask({commit, dispatch}, updatedTask) {
    console.log(updatedTask);
    axios
      .put(`/api/tasks/${updatedTask.id}`, updatedTask, {
        // headers: {
        //   "Content-Type": "application/json; charset=utf-8",
        // },
      })
      .then((response) => {
        console.log(response);
        if (response.data.updatedTodoItem) {
          alert("OK");
          commit("UPDATE_TODO", response.data.updatedTodoItem);
        } else {
          console.error(
            "La réponse du serveur ne contient pas 'updatedTodoItem'."
          );
        }
        dispatch("fetchTasks");
      })
      .catch((error) => {
        console.error("Erreur lors de la mise à jour de la tâche :", error);
      });
  },
};

const getters = {
  tasks: (state) => state.tasks,
};

export default {
  state,
  mutations,
  actions,
  getters,
};
