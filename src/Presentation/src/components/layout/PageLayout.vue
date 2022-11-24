<template>
    <div>
        <AppTopbar v-if="phone" />
        <transition name="fade">
            <AppSidebar v-if="!phone || !menuCollapsed" :menu-width="menuWidth" :menuWidthCollapsed="menuWidthCollapsed" />
        </transition>
        <div :style="{ 'margin-left': getMenuPadding() }" class="main-transistions">
            <transition name="fade">
                <router-view v-show="(phone && menuCollapsed) || !phone" />
            </transition>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Watch } from "vue-property-decorator";
import AppTopbar from "@/components/layout/AppTopbar.vue";
import { Sidebar } from "@/components/layout/AppSidebar/SidebarMixin";
import { mixins } from "vue-class-component";
import AppSidebar from "@/components/layout/AppSidebar/index.vue";

@Component({
    components: {
        AppTopbar,
        AppSidebar,
    },
})
export default class PageLayout extends mixins(Sidebar) {
    private get menuWidth() {
        //menu na telefonie jeśli pokazane to na cały ekran
        return this.phone && !this.menuCollapsed ? "100%" : this.menuWidthOppened + "px";
    }

    private menuWidthCollapsed = 70;
    private menuWidthOppened = 260;

    created(): void {
        this.menuCollapsed = this.phone;
    }

    /**
     * Auto otwieranie menu
     */
    @Watch("phone")
    onPhoneChanged(newValue: boolean): void {
        if (newValue) this.menuCollapsed = true;
        if (!newValue) this.menuCollapsed = false;
    }

    @Watch("tablet")
    onTabletChanged(newValue: boolean, oldValue: boolean): void {
        if (newValue) this.menuCollapsed = true;
        if (!newValue && !oldValue) this.menuCollapsed = false;
    }

    @Watch("laptop")
    onLaptopChanged(newValue: boolean): void {
        if (newValue) this.menuCollapsed = false;
    }

    private getMenuPadding() {
        if (this.phone) return 0;
        return this.menuCollapsed ? this.menuWidthCollapsed + "px" : this.menuWidthOppened + "px";
    }
}
</script>

<style lang="scss" scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 300ms;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}

::v-deep .main-transistions {
    transition: background-color 3000ms linear;
}
</style>
