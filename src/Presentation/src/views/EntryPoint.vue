<template>
    <div class="d-flex flex-column align-self-center justify-content-center mt-5">
        <div class="d-flex flex-column flex-md-row align-items-center align-self-center justify-content-center">
            <img src="/static/app-logo.webp" class="logo-image" alt="logo" />
            <div class="d-flex flex-column">
                <span class="my-auto ml-3 app-name-version"> Easy Commands </span>
                <p class="version-font m-0">v1.0.0</p>
            </div>
        </div>
        <div class="d-flex flex-row justify-content-center mt-5 mx-3">
            <div class="col-12 col-md-6 col-lg-5 col-xl-3">
                <ContentWrapper class="card p-3">
                    <AppTabs class="d-flex flex-row justify-content-center mb-3" v-model="selectedOption" :tabs="tabs" />
                    <router-view />
                </ContentWrapper>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LoginForm from "@/components/user/authentication/LoginForm.vue";
import RegisterForm from "@/components/user/authentication/RegisterForm.vue";
import { Option } from "@/helpers/Interfaces";
import AuthService from "@/services/Auth/AuthService";
import AppTabs from "@/components/common/AppTabs.vue";
import ContentWrapper from "@/components/layout/ContentWrapper.vue";

@Component({
    components: {
        LoginForm,
        RegisterForm,
        AppTabs,
        ContentWrapper,
    },
})
export default class EntryPoint extends Vue {
    private selectedOption: Option = this.tabs[0];

    protected get tabs(): Option[] {
        return [
            { value: "login", text: "Login", icon: "fas fa-fw fa-key text-warning", route: "login" },
            { value: "register", text: "Register", icon: "fas fa-fw fa-user text-info", route: "register" },
        ];
    }

    public created(): void {
        AuthService.Logout(false);
    }
}
</script>

<style lang="scss" scoped>
.logo-image {
    width: 9rem;
    height: 9.25rem;
    max-width: unset;
}

.app-name-version {
    font-family: "Orbitron", cursive;
    font-size: 3.5rem;
}

.version-font {
    font-style: italic;
    font-size: 0.8rem;
    text-align: right;
    float: right;
    vertical-align: bottom;
}
</style>
