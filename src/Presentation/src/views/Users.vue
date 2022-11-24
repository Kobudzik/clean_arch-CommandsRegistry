<template>
    <ContentWrapper :loading="loading">
        <template #header>
            <h4>Users</h4>
        </template>
        <FilterWrapper
            @onApplyFilters="loadData()"
            @onResetFilters="
                filter.reset();
                loadData();
            "
        >
            <div class="row">
                <FilterRowItem label="Username">
                    <b-form-input v-model="filter.username" type="text" />
                </FilterRowItem>
                <FilterRowItem label="Firt name">
                    <b-form-input v-model="filter.firstName" type="text" />
                </FilterRowItem>
                <FilterRowItem label="Last name">
                    <b-form-input v-model="filter.lastName" type="text" />
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
            <template #cell(firstName)="data">
                {{ data.item.firstName }}
            </template>

            <template #cell(roles)="data">
                <SimpleList :items="data.item.roles" />
            </template>
            <template #head(isActive)> Account state </template>
            <template #cell(isActive)="data">
                {{ data.item.isActive ? "Active" : "Disabled" }}
            </template>

            <template #cell(actions)="data">
                <div style="display: flex; flex-direction: row; justify-content: center">
                    <b-button variant="success" v-b-modal="'userDetails'" @click="pickedUserId = data.item.id" class="themed px-2">
                        <i class="fas fa-info-circle fa-sm"></i>
                    </b-button>
                    <b-button variant="danger" @click="remove(data.item.publicId)" class="themed ml-1 px-2">
                        <i class="fas fa-trash fa-sm"></i>
                    </b-button>
                </div>
            </template>
        </b-table>
        <AppPagination :pager="pager" @onPagerChange="loadData" />
        <b-modal entered size="lg" :hide-footer="true" id="userDetails" title="Profile details">
            <slot name="modal">
                <UserForm isModal class="p-0" :userId="pickedUserId" @handle-load-data="loadData" />
            </slot>
        </b-modal>
    </ContentWrapper>
</template>

<script lang="ts">
import { Component } from "vue-property-decorator";
import { mixins } from "vue-class-component";
import TableMixin from "@/components/common/table/TableMixin";

import UserService, { UserFilterModel, UserListItemModel } from "@/services/UserService";
import SimpleList from "@/components/common/SimpleList.vue";
import UserForm from "@/components/user/UserForm.vue";
import Pager from "@/helpers/Pager";
import { BvTableFieldArray } from "bootstrap-vue";
import Form from "@/helpers/Form";
import FilterRowItem from "@/components/common/table/FilterWrapper/FilterRowItem.vue";
import FilterWrapper from "@/components/common/table/FilterWrapper/FilterWrapper.vue";
import AppPagination from "@/components/common/table/AppPagination/AppPagination.vue";
import ContentWrapper from "@/components/layout/ContentWrapper.vue";

@Component({
    components: {
        SimpleList,
        UserForm,
        FilterRowItem,
        FilterWrapper,
        AppPagination,
        ContentWrapper,
    },
})
export default class Users extends mixins(TableMixin) {
    public get filter(): Form<UserFilterModel> {
        return this.$store.state.filters.users.filter;
    }

    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    //@ts-ignore
    public get pager(): Pager {
        return this.$store.state.filters.users.pager;
    }

    private loading: boolean = false;

    private pickedUserId: string = null;

    private items: UserListItemModel[] = [];

    private tableColumns: BvTableFieldArray = [
        {
            key: "userName",
            sortable: true,
        },
        {
            key: "email",
            sortable: true,
        },
        {
            key: "firstName",
            sortable: true,
        },
        {
            key: "lastName",
            sortable: true,
        },
        "roles",
        {
            key: "isActive",
            sortable: true,
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
        const result = await UserService.GetList(this.pager.data, this.filter.data);
        this.items = result.items;
        this.pager.totalRows = result.totalCount;
        this.loading = false;
    }

    async remove(userPublicId: string): Promise<void> {
        try {
            let actionAccepted = await this.$confirm();

            if (actionAccepted.isConfirmed == true) {
                this.$toast("User removed", undefined, 1000);

                await UserService.Remove(userPublicId);
                this.loadData();
            }
        } catch {
            this.$toast("Error during removing user", "warning", 1000);
            //todo
        }
    }
}
</script>

<style scoped>
::v-deep .col-width {
    width: 8rem;
}
</style>
