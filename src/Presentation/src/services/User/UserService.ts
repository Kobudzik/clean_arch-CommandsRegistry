import Vue from "vue";
import store from "@/store/index";
import merge from "lodash/merge";
import { PagerContract } from "../../helpers/Pager";
import { PaginatedList } from "@/helpers/Interfaces";
import { SettingsActions } from "@/helpers/vuex-enums/SettingsEnums";
import { UserFilterModel, UserListItemModel, UserFormModel, SettingsVM, UpdateUserModel, CreateUserModel } from "./UserModels";

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
