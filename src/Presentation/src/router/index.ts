import Vue from "vue";
import Router from "vue-router";
import routes from "./routes";

Vue.use(Router);

const router = new Router({
    mode: "history",
    routes,
});

Vue.router = router;

export default router;

router.beforeEach((to, from, next) => {
    // redirect to login page if not logged in and trying to access a restricted page, PATHS
    const publicPages = ["/login", "/register", "/email-confirmation"];

    const isAuthRequired = !publicPages.includes(to.path);
    const isLoggedIn = localStorage.getItem("user");

    if (isAuthRequired && !isLoggedIn) {
        Vue.prototype.$toast("You cannot acces that page", "warning");
        return next("/login");
    }

    next();
});
