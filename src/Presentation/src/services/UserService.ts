import Vue from "vue";
import store from "@/store/index";
import merge from "lodash/merge";
import { PagerContract } from "./../helpers/Pager";
import { PaginatedList } from "@/helpers/Interfaces";
import { SettingsActions } from "@/helpers/vuex-enums/SettingsEnums";

export default class UserService {
    public static moduleUrl = "users";

    /**
     * fetches list of application users.
     * Only for Administrator
     */
    public static async GetList(pager: PagerContract, filter: UserFilterModel): Promise<PaginatedList<UserListItemModel>> {
        return (
            await Vue.axios.get(`${this.moduleUrl}/list`, {
                params: merge({}, filter, pager),
            })
        ).data;
    }

    public static async Get(publicId: string): Promise<UserFormModel> {
        return (await Vue.axios.get(`${this.moduleUrl}/${publicId}`)).data;
    }

    /**
     * Fetches user settings from API, then saves it in store
     */
    public static async GetAndSetSettings(): Promise<SettingsVM> {
        const response = (await Vue.axios.get<SettingsVM>(`${this.moduleUrl}/settings`)).data;
        store.dispatch(SettingsActions.SET_THEME_PRIMARY_COLOR, response.themePrimaryColor);

        return response;
    }

    /**
     * Updates user data
     */
    public static async Update(updateModel: UpdateUserModel): Promise<UserFormModel> {
        return (await Vue.axios.patch(`${this.moduleUrl}`, updateModel)).data;
    }

    /**
     * Pernamently removes user
     */
    public static async Remove(userPuiblicId: string): Promise<UserFormModel> {
        return (await Vue.axios.delete(`${this.moduleUrl}/${userPuiblicId}`)).data;
    }

    /**Creates User account.'
     *Used in register process */
    public static async Create(userModel: CreateUserModel): Promise<UserFormModel> {
        return (await Vue.axios.post(`${this.moduleUrl}`, userModel)).data;
    }
}

export interface UserFormModel {
    id: string;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    roles: string[];
    themePrimaryColor: string;
}

export interface UserListItemModel {
    publicId: string;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    roles: string[];
}

export interface UpdateUserModel {
    userId: string;
    firstName: string;
    lastName: string;
    themePrimaryColor: string;
}

export interface SettingsVM {
    themePrimaryColor: string;
}

export interface UserFilterModel {
    username: string;
    firstName: string;
    lastName: string;
}

export interface CreateUserModel {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    isActive?: boolean;
    roles?: string[];
}
