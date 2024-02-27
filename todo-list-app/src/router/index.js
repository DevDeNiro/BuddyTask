import {createRouter, createWebHistory} from "vue-router";

import Home from "../views/Home.vue";
import Calendar from "../views/Calendar.vue";

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/calendar",
        name: "Calendar",
        component: Calendar,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
});

export default router;
