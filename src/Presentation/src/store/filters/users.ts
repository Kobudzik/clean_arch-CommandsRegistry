/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import { UserFilterModel } from "@/services/User/UserModels";
import Form from "@/helpers/Form";
import Pager from "@/helpers/Pager";

export default {
    namespaced: true,
    state: {
        filter: Form.create<UserFilterModel>({
            username: "",
            firstName: "",
            lastName: "",
        }),
        pager: new Pager(1, 20, "userName"),
    } as UserListState,
};

interface UserListState {
    filter: Form<UserFilterModel>;
    pager: Pager;
}
