/* eslint-disable */
export default {
    install(Vue: any) {
        Vue.prototype.$toast = (title: string, icon = "success", timer: number = 4000, position: string = "top-end") => {
            Vue.prototype.$swal.fire({
                position: position,
                timer: timer,
                title: title,
                icon: icon,
                toast: true,
                showConfirmButton: false,
                timerProgressBar: true,
            });
        };
    },
};
