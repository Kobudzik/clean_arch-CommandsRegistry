<template>
    <div class="loader-overlay">
        <div class="loader" :class="[{ 'fixed-loader': fixedPosition }, { 'static-loader': !fixedPosition }]"></div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component({})
export default class Loader extends Vue {
    @Prop({ type: Boolean, default: true })
    private fixedPosition: boolean;
}
</script>

<style lang="scss" scoped>
@import "@/styles/style.scss";

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}

.loader-overlay {
    position: fixed;
    left: 0;
    top: 0;
    width: 100vw;
    height: 100vh;
    z-index: 1000;
    background-color: rgba(0, 0, 0, 0.3);

    .loader {
        border: 14px solid #f3f3f3;
        border-top: 14px solid var(--primary-color);
        border-radius: 50%;
        width: 13rem;
        height: 13rem;
        animation: spin 2s linear infinite;
    }

    //position
    .fixed-loader {
        position: fixed;
        left: 25%;
        top: 30%;
        z-index: 9999;

        @include media-breakpoint-up(sm) {
            left: 50%;
            top: 30%;
        }
    }

    .static-loader {
        display: flex;
        margin: auto;
        width: 9rem;
        height: 9rem;
    }
}
</style>
