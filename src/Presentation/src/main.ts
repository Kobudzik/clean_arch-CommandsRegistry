import "@/plugins/components";
import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import { ProgressPlugin } from "@/helpers/Progress";

import { setUserStateFromStorage } from "@/helpers/AuthHelper";

import "@/plugins/axios";
import "@/plugins/filters";
import "@/plugins/mixins";
import "bootstrap-icons/font/bootstrap-icons.css";

Vue.use(ProgressPlugin, {
    color: "var(--primary-color)",
    failedColor: "red",
    thickness: "5px",
    autoFinish: false,
    autoRevert: false,
    inverse: false,
});

Vue.config.productionTip = false;

new Vue({
    router,
    store,
    render: (h) => h(App),
    created() {
        const html = document.documentElement;
        html.setAttribute("lang", "en-US");
    },
}).$mount("#app");

setUserStateFromStorage();
