/* eslint-disable @typescript-eslint/explicit-module-boundary-types */

import { Commit } from "vuex";

export default {
    namespaced: true,
    state: {} as SettingsState,
    mutations: {
        SET_THEME_PRIMARY_COLOR: (state: SettingsState, themePrimaryColor: string) => {
            state.themePrimaryColor = themePrimaryColor;
            document.body.style.setProperty("--primary-color", themePrimaryColor);
        },
    },
    actions: {
        SET_THEME_PRIMARY_COLOR: ({ commit }: { commit: Commit }, themePrimaryColor: string) => {
            commit("SET_THEME_PRIMARY_COLOR", themePrimaryColor);
        },
    },
    getters: {
        GET_THEME_PRIMARY_COLOR(state: SettingsState): string {
            document.body.style.setProperty("--primary-color", state.themePrimaryColor);
            return state.themePrimaryColor;
        },
    },
};

interface SettingsState {
    themePrimaryColor: string;
}
