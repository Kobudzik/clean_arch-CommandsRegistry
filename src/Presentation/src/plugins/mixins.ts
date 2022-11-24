import Vue from "vue";
import { UserGetters } from "@/helpers/vuex-enums/UserEnums";
import store from "@/store";

Vue.mixin({
    computed: {
        phone() {
            return ["xs"].includes(this.$mq as string);
        },
        tablet() {
            return ["sm", "md"].includes(this.$mq as string);
        },
        mobile() {
            return ["xs", "sm", "md"].includes(this.$mq as string);
        },
        laptop() {
            return ["lg"].includes(this.$mq as string);
        },
        desktop() {
            return ["xl"].includes(this.$mq as string);
        },
        userRoles() {
            return store.getters[UserGetters.GET_CURRENT_USER_ROLES];
        },
        isAuthenticated() {
            return store.getters[UserGetters.GET_AUTH_STATE];
        },
        currentUser() {
            return store.getters[UserGetters.GET_CURRENT_USER];
        },
    },
    methods: {
        isToday(utcDate: Date): boolean {
            if (!utcDate) return;

            const now = new Date();

            return now.getUTCDate() == utcDate.getDate() && now.getUTCMonth() == utcDate.getMonth() && now.getUTCFullYear() == utcDate.getFullYear();
        },
        dayName(date: Date, format: "short" | "long" | "narrow" = "short"): string {
            if (!date) return;

            return date.toLocaleString("eng", { weekday: format });
        },
        monthName(date: Date, format: "short" | "long" | "narrow" = "short"): string {
            if (!date) return;

            return date.toLocaleString("eng", { month: format });
        },
        getOffsetFromUtc(): number {
            return (new Date().getTimezoneOffset() / 60) * -1;
        },
    },
});
