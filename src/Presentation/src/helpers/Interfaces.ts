import { BvTableField } from "bootstrap-vue";
import { ChartOptions } from "chart.js";

export interface Statement {
    code?: number;
    message?: string;
}

export interface Option<V = string> {
    value: V;
    text: string;
    disabled?: boolean;
    icon?: string;

    //**route name */
    route?: string;
}

export interface PickerItemtDto {
    id: number;
    name: string;
}

export interface PaginatedList<T> {
    items: T[];
    pageIndex: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}

export type BootstrapColumns = Array<BvTableField & { key: string } & { filterFunc?: () => boolean }>;
export type CharMergedOptions = ChartOptions;

export interface SweetAlertConfirmResult {
    isConfirmed: boolean;
    isDenied: boolean;
    isDismissed: boolean;
}
