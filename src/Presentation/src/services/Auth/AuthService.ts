import Vue from "vue";
import { StorageTokenModel, setUserStateFromStorage, removeUserStateFromStore } from "@/helpers/AuthHelper";
import router from "@/router/index";
import UserService from "@/services/User/UserService";
import LoginModel from "./AuthModels";

export default class AuthService {
    /**
     * Tries to log in in backend.
     * If succesfull- saves token to strage and sets vuex state (currentUser, isAuthenticated),
     *  then fetches settings ands saves to store
     */
    public static async Login(model: LoginModel): Promise<boolean> {
        const response = (await Vue.axios.post<StorageTokenModel>("authentication", model)).data;

        if (response) {
            localStorage.setItem("user", JSON.stringify(response));
            setUserStateFromStorage();
            UserService.GetAndSetSettings();

            return true;
        }

        return false;
    }

    public static async Logout(redirectToLogin: boolean = true): Promise<void> {
        localStorage.removeItem("user");
        removeUserStateFromStore();

        if (redirectToLogin) {
            router.push({ name: "login" });
        }
    }
}
