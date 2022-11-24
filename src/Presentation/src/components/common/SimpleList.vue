<template>
    <div v-if="items && items.length > 0">
        <ul style="list-style-type: square" class="ml-4 p-0">
            <li v-for="(item, i) in items" :key="i">{{ item }}</li>
        </ul>
    </div>
    <div v-else>-</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({})
export default class SimpleList extends Vue {
    @Prop() private items: string[];
}
</script>
