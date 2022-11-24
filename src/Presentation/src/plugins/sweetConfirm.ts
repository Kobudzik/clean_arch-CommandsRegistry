/* eslint-disable */
export default {
    install(Vue: any) {
        Vue.prototype.$confirm = () => {
            return Vue.prototype.$swal({
                title: "Are you sure?",
                icon: "warning",
                showCancelButton: true,
            });
        };

        Vue.confirm = Vue.prototype.$confirm;
    },
};
