<template>
    <div>
        <!-- Sidebar -->
        <SidebarMenu
            class="phone-margin"
            hideToggle
            :collapsed="menuCollapsed"
            :width="menuWidth"
            :width-collapsed="menuWidthCollapsed + 'px'"
            :menu="filteredSitemap"
            @item-click="onItemClick"
        >
            <!-- Header -->
            <template #header>
                <!-- Logo -->
                <div v-if="!phone" class="mb-4 mt-2 header-wrapper">
                    <img src="/static/app-logo.webp" class="logo-image" alt="logo" />
                    <span v-if="!menuCollapsed" class="my-auto app-name"> Easy Commands </span>
                </div>
            </template>
            <!-- Footer -->
            <template #footer>
                <div v-show="!phone" class="footer-button" @click="menuCollapsed = !menuCollapsed" title="Logout">
                    <b-icon-chevron-bar-right font-scale="1.5" :rotate="menuCollapsed ? 0 : 180" />
                </div>
            </template>
        </SidebarMenu>
    </div>
</template>

<script lang="ts">
import { Component, Prop } from "vue-property-decorator";
import { UserActions } from "@/helpers/vuex-enums/UserEnums";
import SidebarMenu from "@/components/layout/AppSidebar/vue-sidebar-menu/src/components/SidebarMenu.vue";
import { sitemapItems } from "@/helpers/Sitemap";
import { Sidebar } from "@/components/layout/AppSidebar/SidebarMixin";
import { mixins } from "vue-class-component";

@Component({
    components: {
        SidebarMenu,
    },
})
export default class AppSidebar extends mixins(Sidebar) {
    @Prop() private menuWidth: string;
    @Prop() private menuWidthCollapsed: number;

    private get filteredSitemap() {
        return sitemapItems.filter((menuEntry) => {
            if (menuEntry.requireAnonymous) return !this.isAuthenticated;
            if (!this.userRoles) return false;
            if (menuEntry.requireAuthenticated) return this.isAuthenticated;

            //if any role filter exists
            if (menuEntry.requireAnyRole?.length > 0) {
                return menuEntry.requireAnyRole.some((x) => this.userRoles.includes(x));
            }

            //no filters hit- display sitemap entry
            return true;
        });
    }

    private get logoutWrapperStyle(): unknown {
        if (this.menuCollapsed) {
            return {
                display: "flex",
                "justify-content": "center",
            };
        } else {
            return {
                "padding-left": "2rem",
            };
        }
    }

    private onItemClick(event: unknown, item: { modal: string; title: string }) {
        if (item.modal == "login-modal") this.$bvModal.show("login-modal");
        if (item.modal == "register-modal") this.$bvModal.show("register-modal");
        if (item.title.toLowerCase() == "logout") this.logout();
        if (this.phone) this.menuCollapsed = true;
    }

    private logout() {
        this.$store.dispatch(UserActions.logout);
        this.$toast("Logout succesful", undefined, 1000);
    }
}
</script>

<style lang="scss">
@import "@/styles/custom-sidebar.scss";
@import "vue-sidebar-menu/src/scss/vue-sidebar-menu.scss";

.footer-button {
    display: flex;
    justify-content: center;
    cursor: pointer;
    padding-top: 0.4rem;
    padding-bottom: 0.4rem;
    background-color: var(--primary-color);
    margin-right: -1px;

    &:hover {
        background-color: gray;
    }
}

//menu items padding
.v-sidebar-menu .vsm--link {
    padding-left: $icon-padding-left;
}

//menu items padding
.v-sidebar-menu {
    border-right: 1px solid hsl(0, 0%, 85%);
}

.vsm--title {
    margin-left: 2rem;
}

//usunięcie tła ikony na hover
.v-sidebar-menu.vsm_collapsed .vsm--icon {
    background-color: transparent !important;
}

//powiększenie znacznika aktywnej strony
.v-sidebar-menu .vsm--link_level-1.vsm--link_active {
    box-shadow: 7px 0px 0px 0px $primary-color inset !important;
}

img {
    max-width: 70%;
}

.header-wrapper {
    display: flex;
    justify-content: center;

    & > .logo-image {
        width: 5rem;
        margin: 0 10px 0 10px;
    }

    & > .app-name {
        font-family: "Orbitron", cursive;
        font-size: 1.5rem;
    }
}

.modal-backdrop {
    opacity: 0.7 !important;
}

.phone-margin {
    @include media-breakpoint-down(xs) {
        margin-top: 2.9rem; // hard coded margin
    }
}
</style>
