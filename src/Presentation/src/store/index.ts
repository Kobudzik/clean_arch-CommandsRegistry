import Vue from "vue";
import Vuex from "vuex";
import user from "./user";
import menu from "./menu";
import settings from "./settings";
import createPersistedState from "vuex-persistedstate";

//filters
import users from "@/store/filters/users";

Vue.use(Vuex);

export default new Vuex.Store({
    strict: false,
    modules: {
        user,
        menu,
        settings,
        filters: {
            namespaced: true,
            modules: {
                users,
            },
        },
    },
    plugins: [
        createPersistedState({
            reducer: (persistedState: unknown) => {
                const stateFilter = JSON.parse(JSON.stringify(persistedState)); // deep clone
                const states = ["settings"]; // states which we want to persist.

                Object.keys(stateFilter).forEach((key) => {
                    if (!states.includes(key)) delete stateFilter[key];
                });

                return stateFilter;
            },
        }),
    ],
});
