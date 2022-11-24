<template>
    <select v-model="pager.size" @change="$emit('onPageSizeChange')" class="form-control w-auto" title="Pager size">
        <option v-for="item in [10, 20, 50, 100]" :key="item" :value="item" class="text-center">
            <span v-if="phone"> Records per row: </span>
            {{ item }}
        </option>
    </select>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import Pager from "@/helpers/Pager";

@Component({
    name: "data-size",
})
export default class DataSize extends Vue {
    @Prop() private pager!: Pager;
}
</script>
