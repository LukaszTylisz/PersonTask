import { createWebHistory, createRouter } from "vue-router";

const routes =  [
    {
        path: "/",
        name: "Persons",
        component: () => import("./components/Persons"),
    },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;