const state = {
  popup: {
    isVisible: false,
    message: "",
    type: "",
    taskDetails: null,
  },
};

const mutations = {
  SHOW_POPUP(state, payload) {
    state.popup.isVisible = true;
    state.popup.message = payload.message;
    state.popup.type = payload.type;
    state.popup.taskDetails = payload.taskDetails;
  },
  HIDE_POPUP(state) {
    state.popup.isVisible = false;
    state.popup.message = "";
    state.popup.type = "";
    state.popup.taskDetails = null;
  },
};

const actions = {
  showPopup({commit}, payload) {
    console.log("payload", payload);
    commit("SHOW_POPUP", payload);
  },
  hidePopup({commit}) {
    commit("HIDE_POPUP");
  },
};

const getters = {popup: (state) => state.popup};

export default {
  state,
  mutations,
  actions,
  getters,
};
