import { RouteConfig } from "vue-router";

const routes: Array<RouteConfig> = [
    {
        path: "/",
        redirect: "login",
        name: "entry-page",
        component: () => import(/* webpackChunkName: "home-entry-point" */ "../views/EntryPoint.vue"),
        children: [
            {
                path: "/login",
                name: "login",
                props: true,
                component: () => import(/* webpackChunkName: "home-login-form" */ "../components/user/authentication/LoginForm.vue"),
            },
            {
                path: "/register",
                name: "register",
                props: true,
                component: () => import(/* webpackChunkName: "home-register-form" */ "../components/user/authentication/RegisterForm.vue"),
            },
        ],
    },
    {
        path: "/",
        component: () => import(/* webpackChunkName: "home-register-form" */ "../components/layout/PageLayout.vue"),
        children: [
            {
                path: "/commands",
                name: "commands",
                component: () => import(/* webpackChunkName: "commands" */ "../views/Commands.vue"),
            },
            {
                path: "/users",
                name: "users",
                component: () => import(/* webpackChunkName: "users" */ "../views/Users.vue"),
            },
            {
                path: "/profile",
                name: "my-profile",
                component: () => import(/* webpackChunkName: "my-profile" */ "../views/MyProfile.vue"),
            },
        ],
    },
    {
        path: "*",
        component: () => import(/* webpackChunkName: "not-found" */ "../views/404NotFound.vue"),
    },
];

export default routes;
