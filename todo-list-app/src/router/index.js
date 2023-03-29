import {createRouter, createWebHistory} from "vue-router";

import tasksRoutes from "../modules/tasks/routes";
import categoriesRoutes from "../modules/categories/routes";

const routes = [...tasksRoutes, ...categoriesRoutes];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

export default router;
