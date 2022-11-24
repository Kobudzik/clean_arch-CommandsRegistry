<template>
    <div class="d-flex justify-content-between align-items-center wrapper">
        <!-- Hamburger -->
        <div class="ml-1 hamburger-icon" @click="menuCollapsed = !menuCollapsed">
            <b-icon-chevron-bar-right font-scale="2.2" :rotate="menuCollapsed ? 90 : 270" />
        </div>
        <!-- Logo -->
        <div class="logo-image">
            <img src="/static/app-logo.webp" alt="logo" />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { MenuActions, MenuGetters } from "@/helpers/vuex-enums/MenuEnums";

@Component({
    components: {},
})
export default class AppTopbar extends Vue {
    private set menuCollapsed(value: boolean) {
        this.$store.dispatch(MenuActions.SET_COLLAPSED, value);
    }

    private get menuCollapsed(): boolean {
        return this.$store.getters[MenuGetters.GET_COLLAPSE_STATE];
    }
}
</script>

<style lang="scss" scoped>
.wrapper {
    min-height: 1rem;
    border-bottom: 1px solid lightgray;
    z-index: 600;
    position: relative;

    & > .hamburger-icon {
        cursor: pointer;
    }

    & > .logo-image {
        display: flex;
        justify-content: center;

        & > img {
            width: 4rem;
        }
    }
}
</style>
