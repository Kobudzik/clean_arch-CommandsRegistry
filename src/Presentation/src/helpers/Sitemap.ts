import { SidebarItem } from "@/components/layout/AppSidebar/vue-sidebar-menu/types/index";
import { UserRole } from "@/helpers/Enums";

export const sitemapItems: SidebarItem[] = [
    {
        title: "Commands",
        href: { path: "commands" },
        icon: {
            element: "b-icon-info-circle",
        },
        badge: {
            text: "new",
            class: "vsm--badge_default",
        },
    },
    {
        title: "My profile",
        href: { path: "profile" },
        icon: {
            element: "b-icon-person",
        },
        requireAuthenticated: true,
    },
    {
        title: "Users",
        href: { path: "users" },
        icon: {
            element: "b-icon-people",
        },
        requireAnyRole: [UserRole.Administrator],
    },
    {
        title: "Logout",
        icon: {
            element: "b-icon-unlock",
        },
        requireAuthenticated: true,
        requireAnonymous: false,
    },
];
