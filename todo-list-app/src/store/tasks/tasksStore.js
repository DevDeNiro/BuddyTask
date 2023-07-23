import apiClient from "../../api";

const state = {
  tasks: [],
};

const mutations = {
  ADD_TODO(state, task) {
    state.tasks.push(task);
  },

  DELETE_TODO(state, taskId) {
    state.tasks = state.tasks.filter((task) => task.id !== taskId);
  },

  UPDATE_TODO(state, updatedData) {
    state.tasks = state.tasks.map((item) =>
      item.id === updatedData.id ? {...item, ...updatedData} : item
    );
  },
};

const actions = {
  addTask({commit}, task) {
    apiClient
      .post("/tasks", task)
      .then((response) => {
        commit(
          "ADD_TASK_TO_CATEGORY",
          {categoryId: task.categoryId, task: response.data},
          {root: true}
        );
        console.log(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },

  deleteTask({commit, dispatch}, task) {
    apiClient
      .delete("/tasks", {params: {id: task}})
      .then((response) => {
        commit("DELETE_TODO", response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },

  async updateTask({commit}, updatedTask) {
    try {
      const response = await apiClient.put(
        `/tasks/${updatedTask.id}`,
        updatedTask,
        {}
      );
      console.log(response);
      if (response.data) {
        alert("OK");
        commit("UPDATE_TODO", response.data);
      } else {
        console.error(
          "La réponse du serveur ne contient pas 'updatedTodoItem'."
        );
      }
    } catch (error) {
      console.error("Erreur lors de la mise à jour de la tâche :", error);
    }
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
