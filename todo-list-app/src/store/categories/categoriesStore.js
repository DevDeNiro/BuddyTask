import apiClient from "@/api";

const state = {
  categories: [],
};

const mutations = {
  GET_CATEGORIES(state, categories) {
    state.categories = categories;
  },

  ADD_CATEGORY(state, category) {
    state.categories.push(category);
  },

  ADD_TASK_TO_CATEGORY(state, {categoryId, task}) {
    const category = state.categories.find((cat) => cat.id === categoryId);
    if (category.todoItems) {
      category.todoItems = [...category.todoItems, task];
    } else {
      alert("Cannot get todoItems from category");
    }
  },

  DELETE_CATEGORY(state, categoryId) {
    state.categories = state.categories.filter(
      (category) => category.id !== categoryId
    );
  },

  UPDATE_CATEGORY(state, updatedCategory) {
    const index = state.categories.findIndex(
      (c) => c.id === updatedCategory.id
    );
    if (index !== -1) {
      state.categories.splice(index, 1, updatedCategory);
    }
  },

  MOVE_TASK(state, {taskId, oldCategoryId, newCategoryId}) {
    const oldCategory = state.categories.find((c) => c.id === oldCategoryId);
    const newCategory = state.categories.find((c) => c.id === newCategoryId);

    if (!oldCategory || !newCategory) {
      console.error("An error occurred while moving the task");
      return;
    }

    const taskIndex = oldCategory.todoItems.findIndex((t) => t.id === taskId);
    const task = oldCategory.todoItems[taskIndex];

    if (task) {
      oldCategory.todoItems.splice(taskIndex, 1);
      newCategory.todoItems.push(task);
    } else {
      console.error("An error occurred while moving the task");
    }
    // Triggered the update of the categories
    state.categories = [...state.categories];
  },
};

const actions = {
  async fetchCategories({commit}) {
    try {
      const response = await apiClient.get("/categories");
      commit("GET_CATEGORIES", response.data);
      console.log("Categories fetched from API.", response.data);
    } catch (error) {
      throw new Error("An error occurred while fetching data from API.");
    }
  },

  createCategory({commit}, category) {
    apiClient
      .post("/categories", category)
      .then((response) => {
        commit("ADD_CATEGORY", response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },

  updateCategory({commit, dispatch}, updatedCategory) {
    apiClient
      .put(`/categories/${updatedCategory.id}`, updatedCategory, {})
      .then((response) => {
        commit("UPDATE_CATEGORY", response.data);
        dispatch("fetchCategories");
      })
      .catch((error) => {
        console.log("Error during category update :", error);
      });
  },

  deleteCategory({commit}, id) {
    apiClient
      .delete(`/categories/${id}`)
      .then(() => {
        commit("DELETE_CATEGORY", id);
      })
      .catch((error) => {
        console.log(error);
      });
  },

  async moveTask({commit, dispatch}, payload) {
    const {taskId, oldCategoryId, newCategoryId, completed} = payload;

    // Find the category in the store
    const oldCategory = state.categories.find((c) => c.id === oldCategoryId);
    const newCategory = state.categories.find((c) => c.id === newCategoryId);

    if (!oldCategory || !newCategory) {
      console.error("An error occurred while moving the task");
      return;
    }

    // Find the task in the old category
    const task = oldCategory.todoItems.find((t) => t.id === taskId);

    if (task) {
      commit("MOVE_TASK", payload);
      // Update the task category in the task object
      task.categoryId = newCategoryId;
      // Update the task completed status in the task object
      task.completed = completed;
      // Use the tasks/updateTask action to update the task in the database
      await dispatch("updateTask", task, {root: true});
    } else {
      console.error("Task not found in the store.");
    }
  },
};

const getters = {
  categories: (state) => state.categories,
};

export default {
  state,
  mutations,
  actions,
  getters,
};
