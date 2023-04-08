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
  fetchCategories({commit}) {
    apiClient
      .get("/categories")
      .then((response) => {
        commit("GET_CATEGORIES", response.data);
      })
      .catch((error) => {
        console.log(error)
      })
  },

  // async getCategory(_, id) {
  //   const response = await apiClient.get(`/categories/${id}`);
  //   return response.data;
  // },

  createCategory({commit}, category) {
    apiClient.post("/categories", category).then((response) => {
      commit("ADD_CATEGORY", response.data)
    }).catch((error) => {
      console.log(error)
    })
    ;
  },

 updateCategory({commit}, {id, category}) {
    apiClient.put(`/categories/${id}`, category).then((response) => {
      commit("UPDATE_CATEGORY", {id, category: response.data});
    }).catch((error) => {
      console.log(error)
    })
  },

  deleteCategory({commit}, id) {
    apiClient.delete(`/categories/${id}`).then((response) => {
      commit("DELETE_CATEGORY", id)
    }).catch((error) => {
      console.log(error)
    })
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
