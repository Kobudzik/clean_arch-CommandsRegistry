/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import { CommandFilterModel } from "./../../services/CommandEntries/CommandEntriesModels";
import Form from "@/helpers/Form";
import Pager from "@/helpers/Pager";

export default {
    namespaced: true,
    state: {
        filter: Form.create<CommandFilterModel>({
            name: "",
        }),
        pager: new Pager(1, 20, "name"),
    } as CommandListState,
};

interface CommandListState {
    filter: Form<CommandFilterModel>;
    pager: Pager;
}
