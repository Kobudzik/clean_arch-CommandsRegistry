import Vue from "vue";
import Component from "vue-class-component";
import { MenuActions, MenuGetters } from "@/helpers/vuex-enums/MenuEnums";

//TODO
@Component
export class Sidebar extends Vue {
    protected set menuCollapsed(value: boolean) {
        this.$store.dispatch(MenuActions.SET_COLLAPSED, value);
    }

    protected get menuCollapsed(): boolean {
        return this.$store.getters[MenuGetters.GET_COLLAPSE_STATE];
    }
}
