import { SweetAlertConfirmResult } from "./helpers/Interfaces";
import { UserModel } from "./helpers/AuthHelper";
import { AxiosStatic } from "axios";

declare module "vue/types/vue" {
    interface Vue {
        //global methods (from global mixins)
        isToday(date: Date): boolean;
        dayName(date: Date, format?: "short" | "long" | "narrow"): string;
        monthName(date: Date, format?: "short" | "long" | "narrow"): string;
        getOffsetFromUtc(): number;

        $toast(title: string, icon?: string, timer?: number, position?: string): void;
        $confirm(): Promise<SweetAlertConfirmResult>;

        //computed properties
        phone: boolean;
        userRoles: string[];
        isAuthenticated: boolean;
        currentUser: UserModel | null;
        axios: AxiosStatic;
    }
}
