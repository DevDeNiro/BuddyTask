import {createStore} from "vuex";
import tasks from "./tasks/tasksStore";
import categories from "./categories/categoriesStore";
import popup from "./popup/popupStore";

export default createStore({
  modules: {
    tasks,
    categories,
    popup,
  },
});
