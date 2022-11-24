<template>
    <span class="pagerContainer"> Showing [{{ displayedFromRange }}-{{ displayedToRange }}] from {{ pager.totalRows }} </span>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import Pager from "@/helpers/Pager";

@Component({
    name: "pagination-info",
})
export default class PaginationInfo extends Vue {
    @Prop() private pager: Pager;

    private get displayedFromRange(): number {
        return (this.pager.index - 1) * this.pager.index + 1;
    }

    private get displayedToRange(): number {
        if (this.pager.totalRows < this.pager.size) {
            return this.pager.totalRows;
        } else {
            const result = this.pager.index * this.pager.size;

            return result > this.pager.totalRows ? this.pager.totalRows : result;
        }
    }
}
</script>
