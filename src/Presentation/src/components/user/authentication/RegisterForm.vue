<template>
    <ValidationObserver ref="observer">
        <b-form @submit.stop.prevent="register()" autocomplete="off">
            <ValidationProvider name="username" :rules="{ required: true, min: 3, alpha_num: true }" v-slot="validationContext">
                <b-form-group label="Username">
                    <b-form-input v-model="form.userName" type="text" autocomplete="off" :state="getValidationState(validationContext)" />
                    <b-form-invalid-feedback>
                        {{ validationContext.errors[0] }}
                    </b-form-invalid-feedback>
                </b-form-group>
            </ValidationProvider>

            <ValidationProvider name="first name" :rules="{ required: true, min: 3, alpha: true }" v-slot="validationContext">
                <b-form-group label="First name">
                    <b-form-input v-model="form.firstName" type="text" autocomplete="off" :state="getValidationState(validationContext)" />
                    <b-form-invalid-feedback>
                        {{ validationContext.errors[0] }}
                    </b-form-invalid-feedback>
                </b-form-group>
            </ValidationProvider>

            <ValidationProvider name="last name" :rules="{ required: true, min: 3, alpha: true }" v-slot="validationContext">
                <b-form-group label="Last name">
                    <b-form-input v-model="form.lastName" type="text" autocomplete="off" :state="getValidationState(validationContext)" />
                    <b-form-invalid-feedback>
                        {{ validationContext.errors[0] }}
                    </b-form-invalid-feedback>
                </b-form-group>
            </ValidationProvider>

            <ValidationProvider name="e-mail" :rules="{ required: true, min: 6, email: true }" v-slot="validationContext">
                <b-form-group label="E-mail">
                    <b-form-input v-model="form.email" type="email" autocomplete="off" :state="getValidationState(validationContext)" />
                    <b-form-invalid-feedback>
                        {{ validationContext.errors[0] }}
                    </b-form-invalid-feedback>
                </b-form-group>
            </ValidationProvider>

            <ValidationProvider name="password" :rules="{ required: true, min: 6 }" v-slot="validationContext">
                <b-form-group label="Password">
                    <b-form-input type="password" v-model="form.password" :state="getValidationState(validationContext)" autocomplete="new-password" ref="password" />
                    <b-form-invalid-feedback>
                        {{ validationContext.errors[0] }}
                    </b-form-invalid-feedback>
                </b-form-group>
            </ValidationProvider>

            <ValidationProvider name="repeated password" :rules="{ required: true, min: 6, confirmed: 'password' }" v-slot="validationContext">
                <b-form-group label="Repeat password">
                    <b-form-input v-model="repeatedPassword" type="password" autocomplete="off" :state="getValidationState(validationContext)" />
                    <b-form-invalid-feedback>
                        {{ validationContext.errors[0] }}
                    </b-form-invalid-feedback>
                </b-form-group>
            </ValidationProvider>
            <div style="display: grid">
                <b-button variant="success" type="submit" style="float: right">Register</b-button>
            </div>
        </b-form>
    </ValidationObserver>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UserService from "@/services/User/UserService";
import { CreateUserModel } from "@/services/User/UserModels";
import Form from "@/helpers/Form";
import { ValidationFlags } from "vee-validate/dist/types/types";
import { ValidationObserver, ValidationProvider, extend, localize } from "vee-validate";
import en from "vee-validate/dist/locale/en.json";
import * as rules from "vee-validate/dist/rules";

Object.keys(rules).forEach((rule) => {
    extend(rule, rules[rule]);
});

localize("en", en);

@Component({
    components: {
        ValidationObserver,
        ValidationProvider,
    },
})
export default class UserLogin extends Vue {
    private form: Form<CreateUserModel> = Form.create<CreateUserModel>({
        email: "",
        password: "",
        firstName: "",
        lastName: "",
        userName: "",
    });

    private repeatedPassword: string = "";

    private getValidationState({ dirty, validated, valid = null }: ValidationFlags) {
        return dirty || validated ? valid : null;
    }

    private submit() {
        if (this.form.password != this.repeatedPassword) {
            Vue.prototype.$toast("Passwords doesn't match!", "warning", "2500", "top");
        }
    }

    private async register(): Promise<void> {
        try {
            await UserService.Create(this.form.data);
            this.$emit("registered");
            this.$toast("Account created successfully! Remember to confirm your email");
        } catch {
            //todo
        }
    }
}
</script>
