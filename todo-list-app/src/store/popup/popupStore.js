const state = {
  popup: {
    isVisible: false,
    message: "",
    type: "",
  },
};

const mutations = {
  SHOW_POPUP(state, payload) {
    state.popup.isVisible = true;
    state.popup.message = payload.message;
    state.popup.type = payload.type;
  },
  HIDE_POPUP(state) {
    state.popup.isVisible = false;
    state.popup.message = "";
    state.popup.type = "";
  },
};

const actions = {
  showPopup({commit}, payload) {
    commit("SHOW_POPUP", payload);
  },
  hidePopup({commit}) {
    commit("HIDE_POPUP");
  },
};

export default {
  state,
  mutations,
  actions,
  getters,
};
