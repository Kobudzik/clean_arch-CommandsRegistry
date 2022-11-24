declare module "vue-progressbar" {
    import { PluginFunction } from "vue";

    export const install: PluginFunction<{}>;

    interface ProgressMethods {
        /**
         * 	start the progress bar loading
         */
        start(): void;

        /**
         * finish the progress bar loading
         */
        finish(): void;

        /**
         * cause the progress bar to end and fail
         */
        fail(): void;
    }

    interface ProgressOptions {
        /**
         * allow the progress bar to finish automatically when it is close to 100%
         */
        autoFinish: boolean;

        /**
         * 	inverse the direction of the progress bar
         */
        inverse: boolean;

        /**
         * color of the progress bar
         */
        color: string;

        failedColor: string;

        thickness: string;

        /**
         * will temporary color changes automatically revert upon completion or fail
         */
        autoRevert: boolean;
    }

    module "vue/types/vue" {
        interface Vue {
            $Progress: ProgressMethods;
        }
        interface VueConstructor {
            Progress: ProgressMethods;
        }
    }
}
