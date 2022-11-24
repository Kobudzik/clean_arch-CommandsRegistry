/* eslint-disable no-console */
import Vue from "vue";
import VueAxios from "vue-axios";
import axios, { AxiosResponse, AxiosError } from "axios";
import { getAuthHeaderValue } from "@/helpers/AuthHelper";
import AuthService from "@/services/AuthService";

Vue.use(VueAxios, axios);

Vue.axios.defaults.baseURL = process.env.VUE_APP_API_URL;

Vue.axios.interceptors.request.use((config) => {
    //use auth for all requests- NO external requests from frontend expected
    config.headers.Authorization = getAuthHeaderValue();
    return config;
});

Vue.axios.interceptors.response.use(
    (response: AxiosResponse) => {
        return response;
    },
    (error: AxiosError) => {
        // if (error.response) {
        //     ex.code = error.response.status;
        //     ex.data = typeof error.response.data === "string" ? { message: error.response.data } : error.response.data || {};
        //     ex.message = "message" in ex.data ? ex.data.message : error.message;
        //     ex.inner = error;
        // }

        if (error.response?.data.message) {
            Vue.prototype.$toast(error.response?.data.message, "warning");
        } else {
            const errorCode = error.response && error.response.status ? error.response.status : null;

            if (errorCode != null) {
                if (errorCode === 400) {
                    //HIDE in production
                    let message = "Error 400 occured. Bad request sent! ";

                    const details = error?.response?.data;

                    if (details) {
                        message += "\n" + details.title + "\n";
                        if (details.errors) {
                            const errorsObjects = details.errors;

                            //loop for property names
                            for (const property in errorsObjects) {
                                const stringError = errorsObjects[property];

                                message += "\n- " + stringError;
                            }
                        }
                    }

                    Vue.prototype.$toast(message, "warning");
                }

                if (errorCode === 401) {
                    //Todo logout only if 401 from this app
                    console.log(error.request);
                    AuthService.Logout();
                    Vue.prototype.$toast("Error 401 occured. You are not authorized!", "warning");
                }
                if (errorCode === 403) {
                    Vue.prototype.$toast("Eror 403 occured. You don't have required permissions for this operation!", "warning");
                }
                if (errorCode === 500) {
                    let message = "Error 500 occured! ";
                    const errorData = error.response.data;

                    if (errorData) {
                        message += "\n" + errorData.detail;
                    } else {
                        message += "\n" + "Bad things happened while processing your request";
                    }

                    Vue.prototype.$toast(message, "warning");
                }
                if (errorCode === 503) {
                    Vue.prototype.$toast("Error 503 occured!", "warning");
                }
            } else {
                Vue.prototype.$toast("Unexpected network error occured. ", "warning");
            }
        }

        return Promise.reject(error);
    }
);
