<template>
    <div>
        <form @keyup.enter.prevent="onSubmit" autocomplete="on">
            <b-form-group label="Username" label-for="username">
                <b-form-input v-model="form.username" type="text" id="username" />
            </b-form-group>
            <b-form-group label="Password" label-for="password">
                <b-form-input v-model="form.password" type="password" id="password" autocomplete="on" />
            </b-form-group>
            <div style="display: grid">
                <b-button :disabled="anyFieldEmpty" variant="success" style="float: right" @click.prevent="onSubmit">Login</b-button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import AuthService from "@/services/Auth/AuthService";
import LoginModel from "@/services/Auth/AuthModels";
import { UserGetters } from "@/helpers/vuex-enums/UserEnums";

@Component({
    components: {},
})
export default class UserLogin extends Vue {
    private form: LoginModel = {
        username: "",
        password: "",
    };

    private async onSubmit(): Promise<void> {
        if (this.anyFieldEmpty) return;

        var isSuccess = await AuthService.Login(this.form);

        if (isSuccess) {
            if (this.$route.name != "home") this.$router.push({ name: "commands" });

            this.$emit("login-successful");
            this.$toast("Login succesful. Hello " + this.$store.getters[UserGetters.GET_CURRENT_USER_NAME] + " :)"), undefined, 1000;
        } else {
            this.$toast("Error during logging in", "warning", 1000);
        }
    }

    private get anyFieldEmpty(): boolean {
        return this.form.username === "" || this.form.password === "";
    }
}
</script>
