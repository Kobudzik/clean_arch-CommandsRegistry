<template>
    <ContentWrapper :loading="form.loading">
        <template #header v-if="!isModal">
            <h4 class="test">Profile details</h4>
        </template>
        <div class="row">
            <!-- column 1 -->
            <div class="col-12 col-lg-6">
                <ContentWrapper>
                    <template #subtitle> Basic information </template>
                    <form class="p-2">
                        <fieldset>
                            <b-form-group label="First name">
                                <b-form-input v-model="form.firstName" type="text" />
                            </b-form-group>
                            <b-form-group label="Last name">
                                <b-form-input v-model="form.lastName" type="text" />
                            </b-form-group>
                            <b-form-group disabled label="Email">
                                <b-form-input v-model="form.email" type="text" />
                            </b-form-group>
                            <b-form-group disabled label="Roles:">
                                <div class="col-12 col-md-7" :class="`col-xl-${isModal ? 7 : 5}`">
                                    <AppList :items="form.roles" :nameGetter="(x) => x" />
                                </div>
                            </b-form-group>
                            <div v-if="userRoles.includes(userRoleEnum.Administrator)" class="mt-4">
                                Theme:
                                <b-button v-b-modal="'themePickModal'" variant="success" class="themed ml-2 px-2 py-1">
                                    <i class="bi bi-eyedropper"></i>
                                    Choose
                                </b-button>
                                <b-button variant="success" class="themed px-2 py-1 ml-1" @click.prevent="restoreDefaultColor">
                                    <i class="bi bi-arrow-repeat"></i>
                                    Reset
                                </b-button>
                            </div>
                        </fieldset>
                    </form>
                </ContentWrapper>
                <ContentWrapper class="mt-3">
                    <div class="d-flex justify-content-end">
                        <b-button variant="danger" @click="remove(form.id)" class="themed py-2 px-3">
                            <i class="fas fa-trash fa-sm"></i>
                            Remove acount
                        </b-button>
                        <b-button @click.prevent="onSubmit()" variant="success" title="Save changes" class="themed py-2 px-3 ml-1">
                            <i class="fas fa-save"></i>
                            Save changes
                        </b-button>
                    </div>
                </ContentWrapper>
            </div>
        </div>
        <b-modal entered size="sm" :hide-footer="true" id="themePickModal" title="Select your theme" class="modal-backdrop">
            <slot name="modal">
                <ThemePicker v-model="form.themePrimaryColor" />
            </slot>
        </b-modal>
    </ContentWrapper>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import UserService from "@/services/User/UserService";
import { UserFormModel, UpdateUserModel } from "@/services/User/UserModels";
import Form from "@/helpers/Form";
import { UserRole } from "@/helpers/Enums";
import AppList from "@/components/common/AppList.vue";
import ThemePicker from "@/components/common/ThemePicker.vue";
import AuthService from "@/services/Auth/AuthService";
import ContentWrapper from "@/components/layout/ContentWrapper.vue";

@Component({
    name: "UserForm",
    components: {
        AppList,
        ThemePicker,
        ContentWrapper,
    },
})
export default class UserForm extends Vue {
    @Prop()
    private userId: string;

    @Prop({ default: false, type: Boolean })
    private isModal: boolean;

    private restoreDefaultColor(): void {
        this.form.themePrimaryColor = `rgb(${107},${187},${70})`;
    }

    private userRoleEnum = UserRole;

    private form: Form<UserFormModel> = Form.create<UserFormModel>({
        id: null,
        userName: "User",
        email: null,
        firstName: null,
        lastName: null,
        isActive: null,
        roles: [],
        themePrimaryColor: "rgb(107, 187, 70)",
    });

    async created(): Promise<void> {
        await this.form.ready([this.loadData()]);
    }

    private async loadData(): Promise<boolean> {
        try {
            const response = await UserService.Get(this.userId);
            this.form.withData(response);
        } catch (ex) {
            return false;
        }

        return true;
    }

    private async onSubmit(): Promise<boolean> {
        try {
            let formData = this.form.data;

            let request: UpdateUserModel = {
                firstName: formData.firstName,
                lastName: formData.lastName,
                userId: this.userId,
                themePrimaryColor: formData.themePrimaryColor,
            };

            await UserService.Update(request);
            await this.form.ready([this.loadData()]);

            if (this.isModal) {
                this.$bvModal.hide("userDetails");
                this.$emit("handle-load-data");
            }

            this.$toast("Changes saved", undefined, 1000);
        } catch (ex) {
            this.$toast("Error during saving changes", "warning", 1000);

            //todo only dev
            console.log(ex);

            return false;
        }
        return true;
    }

    private get userIdStore(): string {
        return this.currentUser.id;
    }

    async remove(userPublicId: string): Promise<void> {
        try {
            let actionAccepted = await this.$confirm();

            if (actionAccepted.isConfirmed == true) {
                this.$toast("User removed", undefined, 1000);

                await UserService.Remove(userPublicId);

                if (this.isModal) {
                    this.$bvModal.hide("userDetails");
                    this.$emit("handle-load-data");
                }

                if (this.userIdStore == userPublicId) {
                    AuthService.Logout();
                }
            }
        } catch {
            this.$toast("Error during removing user", "warning", 1000);
            //todo
        }
    }
}
</script>
