import store from "@/store/index";
import { UserActions } from "@/helpers/vuex-enums/UserEnums";
import { SettingsActions } from "@/helpers/vuex-enums/SettingsEnums";

/**
 * Zwraca token ze stora jako string (Bearer + token), albo null
 */
export function getAuthHeaderValue(): string | null {
    const user = JSON.parse(localStorage.getItem("user") || "{}");

    if (user && user.token) {
        return "Bearer " + user.token;
    } else {
        return null;
    }
}

/**
 * Parses jwt coded string token to TokenModel object
 */
export function parseJwtToken(token: string): TokenModel | null {
    try {
        return JSON.parse(atob(token.split(".")[1]));
    } catch (e) {
        return null;
    }
}

/**
 * Sets user to authenticated
 * Set "UserModel" and "isAuthenticated" in store from token held in storage
 */
export function setUserStateFromStorage(): void {
    try {
        const userToken = localStorage.getItem("user") as string;

        if (userToken) {
            const parsedToken = parseJwtToken(userToken);

            const user: UserModel = {
                id: parsedToken?.sub,
                email: parsedToken?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
                firstName: parsedToken?.FirstName,
                lastName: parsedToken?.LastName,
                roles: parsedToken?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
            };

            store.dispatch(UserActions.setCurrentUser, user);
        }
    } catch (e) {
        //todo disable at production
        console.log(e);
    }
}

export function removeUserStateFromStore(): void {
    try {
        store.dispatch(UserActions.setCurrentUser, null);
    } catch (e) {
        //todo disable at production
        console.log(e);
    }
}

// #region interfaces
export interface TokenModel {
    iss: string;
    sub: string;
    aud: string;
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string[];
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string;
    FirstName: string;
    LastName: string;
}

export interface UserModel {
    id: string;
    email: string | null | undefined;
    firstName: string | null | undefined;
    lastName: string | null | undefined;
    roles: string[] | null | undefined;
}

export interface StorageTokenModel {
    token: string;
    refreshToken: string;
}
//#endregion
