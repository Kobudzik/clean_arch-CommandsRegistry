declare module "*.vue" {
    import Vue from "vue";
    export default Vue;
}

declare module "vue-mq";
declare module "vue-color";

// Global properties can be declared
// on the `VueConstructor` interface
declare module "global" {
    module "vue/types/vue" {
        interface VueConstructor {
            router: any;
        }
    }
}
