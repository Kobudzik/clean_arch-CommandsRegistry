<template>
    <div class="col-12 col-md-5 col-lg-4 col-xl-3 px-1">
        <b-form-group :label="label">
            <slot />
        </b-form-group>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";

@Component({
    name: "FilterRowItem",
})
export default class FilterRowItem extends Vue {
    @Prop() private label: string;
}
</script>
