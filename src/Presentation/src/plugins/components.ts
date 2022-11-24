/* eslint-disable @typescript-eslint/no-var-requires */
import Vue from "vue";
import VueMq from "vue-mq";

//font awesome
import "@fortawesome/fontawesome-free/css/all.css";
import "@fortawesome/fontawesome-free/js/all.js";

//boostrap
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

//sweet alert
import VueSweetalert2 from "vue-sweetalert2";
import "sweetalert2/dist/sweetalert2.min.css";
import SweetConfirm from "./sweetConfirm";
import SweetToast from "./sweetToast";

import "vue2-datepicker/index.css";

import VueEvents from "vue-events";

Vue.use(VueMq, {
    breakpoints: {
        xs: 640, // Telefon (do 639px)
        sm: 960, // Mały tablet (do 959px)
        md: 1366, // Duży tablet (do 1365px)
        lg: 1600, // Laptop (do 1599px)
        xl: Infinity, // Desktop
    },
});

Vue.use(BootstrapVue);
Vue.use(IconsPlugin);

Vue.use(VueSweetalert2);
Vue.use(SweetConfirm);
Vue.use(SweetToast);

Vue.use(VueEvents);
