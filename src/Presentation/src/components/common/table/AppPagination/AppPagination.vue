<template>
    <div class="d-flex flex-column flex-sm-row justify-content-between px-2 text-center">
        <DataSize :pager="pager" @onPageSizeChange="$emit('onPagerChange')" />
        <b-pagination v-model="pager.index" @input="$emit('onPagerChange')" :total-rows="pager.totalRows" :per-page="pager.size" align="center" class="mt-2 mt-md-0" />
        <PaginationInfo :pager="pager" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import Pager from "@/helpers/Pager";
import PaginationInfo from "./PaginationInfo.vue";
import DataSize from "./DataSize.vue";

@Component({
    components: {
        PaginationInfo,
        DataSize,
    },
    name: "AppPagination",
})
export default class AppPagination extends Vue {
    @Prop() private pager: Pager;
}
</script>
