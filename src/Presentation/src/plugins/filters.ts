import Vue from "vue";

Vue.filter("symbolIfNull", function (value: string, symbol: string = "-") {
    return value ? value : symbol;
});

Vue.filter("sign", function (value: number) {
    return value > 0 ? "+" + value : value == 0 || value == null ? value : "-" + value;
});

Vue.filter("trim", function (value: string, length: number) {
    if (!value) return "";

    return value.length > length ? value.slice(0, length).trim() + "..." : value;
});

Vue.filter("firstLetterUppercase", function (value: string) {
    if (typeof value !== "string") return "";
    return value.charAt(0).toUpperCase() + value.slice(1);
});

Vue.filter("readableDate", function (utcValue: string): string {
    return new Date(utcValue).toLocaleDateString();
});

Vue.filter("readableDateTime", function (utcValue: string): string {
    return new Date(utcValue).toLocaleString();
});

Vue.filter("precision", function (value: number, precision: number): string {
    return value.toFixed(precision);
});
