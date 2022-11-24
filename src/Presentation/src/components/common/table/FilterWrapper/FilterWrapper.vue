<template>
    <div @keypress.enter="$emit('onApplyFilters')">
        <div class="card bg-light">
            <div class="card-body py-2 px-3">
                <!-- Toggler -->
                <div class="d-flex align-items-center">
                    <b-button @click.prevent="filtersVisible = !filtersVisible" variant="transparent" :class="{ active: filtersVisible }" class="themed" title="Show filters">
                        <i v-if="filtersVisible" class="bi bi-funnel-fill"></i>
                        <i v-else class="bi bi-funnel"></i>
                        <span class="ml-2 text-capitalize">Filters</span>
                    </b-button>
                </div>
                <!-- Collapse -->
                <b-collapse v-model="filtersVisible" class="mt-1">
                    <!-- Data -->
                    <slot></slot>
                    <hr />
                    <!-- Filter buttons -->
                    <div class="d-flex flex-column flex-sm-row">
                        <b-button class="mr-0 mr-sm-2 themed" variant="success" title="Filter" @click.prevent="$emit('onApplyFilters')">
                            <i class="bi bi-search"></i>
                            Filter
                        </b-button>
                        <b-button class="mt-1 mt-md-0" variant="secondary" title="Reset" @click.prevent="$emit('onResetFilters')">
                            <i class="bi bi-eraser"></i>
                            Reset
                        </b-button>
                    </div>
                </b-collapse>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";

@Component({
    name: "FilterWrapper",
})
export default class FilterWrapper extends Vue {
    private filtersVisible: boolean = false;
}
</script>
