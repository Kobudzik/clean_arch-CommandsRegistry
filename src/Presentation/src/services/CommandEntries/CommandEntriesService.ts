import Vue from "vue";
import merge from "lodash/merge";
import { PagerContract } from "./../../helpers/Pager";
import { PaginatedList } from "@/helpers/Interfaces";
import { CommandFilterModel, CommandFormModel, CreateCommandModel } from "./CommandEntriesModels";

export default class UserService {
    public static moduleUrl = "commands";

    public static async GetPaginatedList(pager: PagerContract, filter: CommandFilterModel): Promise<PaginatedList<CommandFormModel>> {
        return (
            await Vue.axios.get(`${this.moduleUrl}/paginated-list`, {
                params: merge({}, filter, pager),
            })
        ).data;
    }

    public static async Get(id: number): Promise<CommandFormModel> {
        return (await Vue.axios.get(`${this.moduleUrl}/${id}`)).data;
    }

    public static async GetList(): Promise<CommandFormModel[]> {
        return (await Vue.axios.get(`${this.moduleUrl}/list`)).data;
    }

    public static async Update(model: CommandFormModel): Promise<CommandFormModel> {
        return (await Vue.axios.patch(`${this.moduleUrl}`, model)).data;
    }

    public static async Remove(entityId: number): Promise<void> {
        await Vue.axios.delete(`${this.moduleUrl}/${entityId}`);
    }

    public static async Create(model: CreateCommandModel): Promise<CommandFormModel> {
        return (await Vue.axios.post(`${this.moduleUrl}`, model)).data;
    }
}
