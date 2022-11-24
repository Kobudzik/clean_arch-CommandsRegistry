<template>
    <ContentWrapper :loading="loading">
        <template #header>
            <h4>Commands</h4>
        </template>
        <b-button @click.prevent="onSubmit()" v-b-modal="'commandDetails'" variant="success" title="Save changes" class="themed py-2 px-3 mb-2">
            <i class="fas fa-plus"></i>
            Add
        </b-button>
        <FilterWrapper
            @onApplyFilters="loadData()"
            @onResetFilters="
                filter.reset();
                loadData();
            "
        >
            <div class="row">
                <FilterRowItem label="Name">
                    <b-form-input v-model="filter.name" type="text" />
                </FilterRowItem>
            </div>
        </FilterWrapper>
        <b-table
            bordered
            responsive
            show-empty
            hover
            :items="items"
            :fields="tableColumns"
            no-local-sorting
            @sort-changed="onSort"
            :sort-by.sync="pager.sortBy"
            :sort-desc.sync="isSortingDesc"
        >
            <template #cell(name)="data">
                {{ data.item.name }}
            </template>
            <template #cell(jsonSchema)="data">
                {{ data.item.jsonSchema }}
            </template>
            <template #cell(actions)="data">
                <div style="display: flex; flex-direction: row; justify-content: center">
                    <b-button variant="success" v-b-modal="'commandDetails'" @click="pickedCommandId = data.item.id" class="themed px-2">
                        <i class="fas fa-pen fa-sm"></i>
                    </b-button>
                    <b-button variant="danger" @click="remove(data.item.id)" class="themed ml-1 px-2">
                        <i class="fas fa-trash fa-sm"></i>
                    </b-button>
                </div>
            </template>
        </b-table>
        <AppPagination :pager="pager" @onPagerChange="loadData" />
        <b-modal entered size="lg" :hide-footer="true" id="commandDetails" title="Command details">
            <slot name="modal">
                <CommandForm class="p-0" :commandId="pickedCommandId" @handle-load-data="loadData" />
            </slot>
        </b-modal>
    </ContentWrapper>
</template>

<script lang="ts">
import { Component } from "vue-property-decorator";
import { mixins } from "vue-class-component";
import TableMixin from "@/components/common/table/TableMixin";
import CommandService from "@/services/CommandEntries/CommandEntriesService";
import { CommandFormModel, CommandFilterModel } from "@/services/CommandEntries/CommandEntriesModels";
import CommandForm from "@/components/command/CommandForm.vue";
import Pager from "@/helpers/Pager";
import { BvTableFieldArray } from "bootstrap-vue";
import Form from "@/helpers/Form";
import FilterRowItem from "@/components/common/table/FilterWrapper/FilterRowItem.vue";
import FilterWrapper from "@/components/common/table/FilterWrapper/FilterWrapper.vue";
import AppPagination from "@/components/common/table/AppPagination/AppPagination.vue";
import ContentWrapper from "@/components/layout/ContentWrapper.vue";

@Component({
    components: {
        CommandForm,
        FilterRowItem,
        FilterWrapper,
        AppPagination,
        ContentWrapper,
    },
})
export default class Commands extends mixins(TableMixin) {
    public get filter(): Form<CommandFilterModel> {
        return this.$store.state.filters.commands.filter;
    }

    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    //@ts-ignore
    public get pager(): Pager {
        return this.$store.state.filters.commands.pager;
    }

    private loading: boolean = false;

    private pickedCommandId: string = null;

    private items: CommandFormModel[] = [];

    private tableColumns: BvTableFieldArray = [
        {
            key: "name",
            sortable: true,
        },
        {
            key: "jsonSchema",
            sortable: false,
        },
        {
            key: "actions",
            thClass: "col-width",
        },
    ];

    async created(): Promise<void> {
        await this.loadData();
    }

    async loadData(): Promise<void> {
        this.loading = true;
        const result = await CommandService.GetPaginatedList(this.pager.data, this.filter.data);
        this.items = result.items;
        this.pager.totalRows = result.totalCount;
        this.loading = false;
    }

    async remove(id: number): Promise<void> {
        let actionAccepted = await this.$confirm();

        if (actionAccepted.isConfirmed == true) {
            await CommandService.Remove(id);
            await this.loadData();
            this.$toast("Entry removed", undefined, 1000);
        }
    }
}
</script>

<style scoped>
::v-deep .col-width {
    width: 8rem;
}
</style>
