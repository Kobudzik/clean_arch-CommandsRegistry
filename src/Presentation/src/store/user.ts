/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import { UserModel } from "@/helpers/AuthHelper";
import { Commit } from "vuex";
import AuthService from "@/services/AuthService";

export default {
    namespaced: true,
    state: {
        isAuthenticated: false,
        currentUser: null,
    } as UserState,
    mutations: {
        SET_AUTH: (state: UserState, isAuthenticated: boolean) => (state.isAuthenticated = isAuthenticated),
        SET_CURRENT_USER: (state: UserState, user: UserModel) => (state.currentUser = user),
        LOGOUT: () => {
            AuthService.Logout();
        },
    },
    actions: {
        setCurrentUser: ({ commit }: { commit: Commit }, user: UserModel) => {
            commit("SET_CURRENT_USER", user);
            commit("SET_AUTH", user != null);
        },
        logout: ({ commit }: { commit: Commit }) => commit("LOGOUT"),
    },
    getters: {
        GET_AUTH_STATE(state: UserState): boolean {
            return state.isAuthenticated;
        },

        GET_CURRENT_USER(state: UserState): UserModel | null {
            return state.currentUser;
        },

        GET_CURRENT_USER_NAME(state: UserState): string | null | undefined {
            return state.currentUser?.firstName;
        },

        GET_CURRENT_USER_ROLES(state: UserState): string[] | null | undefined {
            return state.currentUser?.roles;
        },
    },
};

interface UserState {
    isAuthenticated: boolean;
    currentUser: UserModel | null;
}
