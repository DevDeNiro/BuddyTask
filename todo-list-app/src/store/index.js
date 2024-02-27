import {createStore} from "vuex";

import tasks from "./tasksStore.js";
import categories from "./categoriesStore.js";
import popup from "./popupStore.js";

export default createStore({
    modules: {
        tasks,
        categories,
        popup,
    },
});
