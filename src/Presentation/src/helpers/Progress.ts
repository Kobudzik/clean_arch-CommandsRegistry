/* eslint-disable */
import Vue, { PluginObject } from "vue";
import { AxiosInstance } from "axios";
import VueProgressBar, { ProgressOptions } from "vue-progressbar";
import { Route } from "vue-router";

interface Progress {
    start(): void;
    finish(): void;
    wait(): void;
    resume(): void;
}

export class ProgressHelper implements Progress {
    private axios: AxiosInstance;
    private router: any;
    private options: any;
    private started: boolean = false;
    private waiting: number = 0;
    private timeout: any;
    private delay: number = 250;

    public constructor(axios: AxiosInstance, router: any, options: any) {
        this.axios = axios;
        this.router = router;
        this.options = options;

        this.initProgress();
        this.initAxios();
        this.initRouter();
    }

    private initProgress(): void {
        Vue.use(VueProgressBar, this.options);
        Vue.Progress = Vue.prototype.$Progress;
    }

    private initAxios(): void {
        this.axios.interceptors.request.use((config) => {
            this.wait();
            this.start();

            return config;
        });

        this.axios.interceptors.response.use(
            (response) => {
                this.finish();
                this.resume();

                return response;
            },
            (error) => {
                this.fail();
                this.resume();

                return Promise.reject(error);
            }
        );
    }

    private initRouter(): void {
        this.router.beforeEach((to: Route, from: Route, next: any) => {
            this.start();
            next();
        });

        this.router.afterEach((to: Route, from: Route) => {
            this.finish();
        });
    }

    /**
     * clears timeout
     **/
    private clear(): void {
        if (this.timeout) {
            clearTimeout(this.timeout);
            this.timeout = null;
        }
    }

    private fail(): void {
        this.clear();

        this.timeout = setTimeout(() => {
            Vue.Progress.finish();
            Vue.Progress.fail();
            this.started = false;
        }, this.delay);
    }

    /**
     * clears timeout and starts progress if not started
     */
    public start(): void {
        this.clear();

        if (this.started === false) {
            Vue.Progress.start();
            this.started = true;
        }
    }

    /**
     * resets timeout, and finishes loading after delay
     */
    public finish(): void {
        this.clear();

        this.timeout = setTimeout(() => {
            Vue.Progress.finish();
            this.started = false;
        }, this.delay);
    }

    /**
     * if waiting is less than 0 appends waiting class, and
     * increases waiting
     */
    public wait(): void {
        if (this.waiting <= 0) {
            document.querySelector("body").classList.add("waiting");
        }

        this.waiting++;
    }

    /**
     * decreases waiting, and
     * if waiting is less than 0 removes waiting class and resets waiting
     */
    public resume(): void {
        this.waiting--;

        if (this.waiting <= 0) {
            document.querySelector("body").classList.remove("waiting");
            this.waiting = 0;
        }
    }
}

export const ProgressPlugin: PluginObject<Record<string, ProgressOptions>> = {
    install(Vue, options) {
        if (!Vue.axios) throw new Error("Vue.axios must be set.");

        if (!Vue.router) throw new Error("Vue.router must be set.");

        Vue.progress = Vue.prototype.$progress = new ProgressHelper(Vue.axios, Vue.router, options);
    },
};

declare module "vue/types/vue" {
    interface Vue {
        $progress: Progress;
    }

    interface VueConstructor {
        progress: Progress;
    }
}
