import Pager from "@/helpers/Pager";
import Vue from "vue";
import Component from "vue-class-component";
import { BvTableCtxObject } from "bootstrap-vue";

@Component
export default class TableMixin extends Vue {
    public pager: Pager;

    public get isSortingDesc(): boolean {
        return this.pager.order === "desc";
    }

    public async loadData(): Promise<unknown> {
        throw new Error("not implemented");
    }

    public onSort(filter: BvTableCtxObject): void {
        this.pager.sortBy = filter.sortBy;
        this.pager.order = filter.sortDesc ? "DESC" : "ASC";
        this.loadData();
    }

    async created(): Promise<void> {
        await this.loadData();
    }
}
