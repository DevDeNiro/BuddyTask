import apiClient from "../../api";

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
};

const actions = {
  fetchCategories({commit}) {
    apiClient
      .get("/categories")
      .then((response) => {
        commit("GET_CATEGORIES", response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },

  // async getCategory(_, id) {
  //   const response = await apiClient.get(`/categories/${id}`);
  //   return response.data;
  // },

  createCategory({commit}, category) {
    console.log(category);
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
    console.log("Updated Category from store: ", updatedCategory);
    apiClient
      .put(`/categories/${updatedCategory.id}`, updatedCategory, {})
      .then((response) => {
        console.log("API response:", response);
        commit("UPDATE_CATEGORY", response.data);
        dispatch("fetchCategories");
      })
      .catch((error) => {
        console.log("Erreur lors de la mise à jour de la catégorie :", error);
      });
  },

  deleteCategory({commit}, id) {
    apiClient
      .delete(`/categories/${id}`)
      .then((response) => {
        commit("DELETE_CATEGORY", id);
      })
      .catch((error) => {
        console.log(error);
      });
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
