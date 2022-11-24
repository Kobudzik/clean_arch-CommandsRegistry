<template>
    <div class="wrapper-container">
        <!-- HEADER -->
        <div v-if="hasHeaderSlot()" class="card-header themed-header card px-2">
            <slot name="header" />
        </div>
        <!-- MAIN -->
        <div class="p-1 p-md-2">
            <Loader v-show="loading" />
            <!-- title -->
            <h5 v-if="hasTitleSlot()" class="card-title">
                <slot name="title" />
            </h5>
            <!-- subtitle -->
            <h6 v-if="hasSubtitleSlot()" class="card-subtitle mb-2 text-muted">
                <slot name="subtitle" />
            </h6>
            <!-- default slot -->
            <slot />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import Loader from "@/components/common/Loader.vue";

@Component({
    components: {
        Loader,
    },
})
export default class ContentWrapper extends Vue {
    @Prop({ default: false, type: Boolean }) private loading: boolean;

    hasHeaderSlot(): boolean {
        return !!this.$slots.header;
    }

    hasTitleSlot(): boolean {
        return !!this.$slots.title;
    }

    hasSubtitleSlot(): boolean {
        return !!this.$slots.subtitle;
    }
}
</script>

<style lang="scss" scoped>
.card-header {
    padding: 0.75rem 0.5rem;
    border-radius: 0;
    border-left: 0;
    border-right: 0;
    border-bottom: 1px solid lightgray;
}

.wrapper-container {
    width: 100%;
    padding: 0;
}

.themed-header {
    border-top: 0;
    background-color: rgba(var(--primary-color-rgb), 0.6);
}

.custom-card {
    border-radius: 0 0 0 0;
}
</style>
