/* eslint-disable @typescript-eslint/explicit-module-boundary-types */

import { Commit } from "vuex";

export default {
    namespaced: true,
    state: {
        isCollapsed: false,
    } as MenuState,
    mutations: {
        SET_COLLAPSED: (state: MenuState, isCollapsed: boolean) => (state.isCollapsed = isCollapsed),
    },
    actions: {
        SET_COLLAPSED: ({ commit }: { commit: Commit }, isCollapsed: boolean) => commit("SET_COLLAPSED", isCollapsed),
    },
    getters: {
        GET_COLLAPSE_STATE(state: MenuState): boolean {
            return state.isCollapsed;
        },
    },
};

interface MenuState {
    isCollapsed: boolean;
}
