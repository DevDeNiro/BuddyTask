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

  UPDATE_CATEGORY(state, {id, category}) {
    const index = state.categories.findIndex((c) => c.id === id);
    if (index !== -1) {
      state.categories[index] = category;
    }
  },
};

const actions = {
  async fetchCategories({commit}) {
    const response = await apiClient.get("/categories");
    commit("GET_CATEGORIES", response.data);
  },
  // async getCategory(_, id) {
  //   const response = await apiClient.get(`/categories/${id}`);
  //   return response.data;
  // },
  async createCategory({commit}, category) {
    const response = await apiClient.post("/categories", category);
    commit("ADD_CATEGORY", response.data);
  },

  async updateCategory({commit}, {id, category}) {
    const response = await apiClient.put(`/categories/${id}`, category);
    commit("UPDATE_CATEGORY", {id, category: response.data});
  },

  async deleteCategory({commit}, id) {
    await apiClient.delete(`/categories/${id}`);
    commit("DELETE_CATEGORY", id);
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
