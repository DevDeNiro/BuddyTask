import apiClient from "@/api";

const state = {
  tasks: [],
};

const mutations = {
  DELETE_TASK(state, taskId) {
    state.tasks = state.tasks.filter((task) => task.id !== taskId);
  },

  UPDATE_TASK(state, updatedData) {
    state.tasks = state.tasks.map((item) =>
      item.id === updatedData.id ? {...item, ...updatedData} : item
    );
  },

  UPDATE_TASK_CATEGORY(state, {taskId, newCategoryId}) {
    const taskToUpdate = state.tasks.find((t) => t.id === taskId);
    if (taskToUpdate) {
      taskToUpdate.categoryId = newCategoryId;
    }
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
      })
      .catch((error) => {
        console.log(error);
      });
  },

  deleteTask({commit}, task) {
    apiClient
      .delete("/tasks", {params: {id: task}})
      .then((response) => {
        commit("DELETE_TASK", response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },

  async updateTask({commit}, updatedTask) {
    try {
      const response = await apiClient.put(
        `/tasks/${updatedTask.id}`,
        updatedTask
      );

      if (response.data) {
        commit("UPDATE_TASK", response.data);
        console.log("Task updated successfully.");
      } else {
        console.error("Error during update :" + response.data);
      }
    } catch (error) {
      console.error("Error during update :", error);
    }
  },

  async updateTaskCategory({commit}, {taskId, newCategoryId}) {
    try {
      const response = await apiClient.put(`/tasks/${taskId}`, {
        categoryId: newCategoryId,
      });
      if (response.data) {
        commit("UPDATE_TASK_CATEGORY", {taskId, newCategoryId});
      } else {
        console.error("Error during update :" + response.data);
      }
    } catch (error) {
      console.error("Error :", error);
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
