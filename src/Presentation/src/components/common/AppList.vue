<template>
    <div class="row">
        <!-- Data -->
        <div v-if="items && items.length > 0" class="col-12">
            <p v-if="title" style="font-size: large">{{ title }}</p>
            <ul class="list-group">
                <li v-for="(item, i) in items" :key="i" class="list-group-item p-2 px-3">
                    <div class="d-flex justify-content-between align-items-center">
                        <span class="font-font-weight-bold">{{ nameGetter(item) }}</span>
                        <b-button v-if="removeEntry" style="float: right" class="mx-1" variant="outline-danger" title="Remove" @click="removeEntry(item[idProp])">
                            <i class="fas fa-trash"></i>
                        </b-button>
                    </div>
                </li>
            </ul>
        </div>
        <div v-else class="mx-auto mt-4">
            <p>No entries</p>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({
    name: "",
})
export default class AppList extends Vue {
    @Prop()
    private items: unknown[];

    @Prop()
    private title: string;

    @Prop({ default: "id" })
    private idProp: string;

    @Prop()
    private removeEntry: (id: unknown) => Promise<void>;

    @Prop({ type: Function, default: (x: unknown) => x })
    public nameGetter: (object: unknown) => string;
}
</script>
