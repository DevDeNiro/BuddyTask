import {createRouter, createWebHistory} from "vue-router";

import Home from "../views/Home.vue";

// Module routes
import tasksRoutes from "../modules/tasks/routes";
import categoriesRoutes from "../modules/categories/routes";

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  ...tasksRoutes,
  ...categoriesRoutes,
];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

export default router;
