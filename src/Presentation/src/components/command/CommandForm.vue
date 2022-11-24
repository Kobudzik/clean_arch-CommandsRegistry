<template>
    <ContentWrapper :loading="form.loading">
        <div class="row">
            <!-- column 1 -->
            <div class="col-12">
                <ContentWrapper>
                    <template #subtitle> Details </template>
                    <form class="p-2">
                        <fieldset>
                            <b-form-group label="Name">
                                <b-form-input v-model="form.name" type="text" />
                            </b-form-group>
                            <b-form-group label="Json schema">
                                <b-form-textarea v-model="form.jsonSchema" rows="19" />
                            </b-form-group>
                        </fieldset>
                    </form>
                </ContentWrapper>
                <ContentWrapper class="mt-3">
                    <div class="d-flex justify-content-end mt-5">
                        <b-button @click.prevent="onSubmit()" variant="success" title="Save changes" class="themed py-2 px-3 ml-1">
                            <i class="fas fa-save"></i>
                            Save changes
                        </b-button>
                    </div>
                </ContentWrapper>
            </div>
        </div>
    </ContentWrapper>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import CommandService from "@/services/CommandEntries/CommandEntriesService";
import { CommandFormModel } from "@/services/CommandEntries/CommandEntriesModels";
import Form from "@/helpers/Form";
import ContentWrapper from "@/components/layout/ContentWrapper.vue";

@Component({
    name: "CommandForm",
    components: {
        ContentWrapper,
    },
})
export default class CommandForm extends Vue {
    @Prop()
    private commandId: number;

    private form: Form<CommandFormModel> = Form.create<CommandFormModel>({
        id: null,
        name: "",
        jsonSchema:"",
    });

    async created(): Promise<void> {
        await this.form.ready([this.loadData()]);
    }

    private async loadData(): Promise<boolean> {
        try {
            const response = await CommandService.Get(this.commandId);
            this.form.withData(response);
        } catch (ex) {
            return false;
        }

        return true;
    }

    private async onSubmit(): Promise<boolean> {
        try {
            let formData = this.form.data;

            let request: CommandFormModel = {
                id: formData.id,
                name: formData.name,
                jsonSchema: formData.jsonSchema,
            };

            await CommandService.Update(request);
            await this.form.ready([this.loadData()]);
            this.$toast("Changes saved", undefined, 1000);
        } catch (ex) {
            this.$toast("Error during saving changes", "warning", 1000);

            //todo only dev
            console.log(ex);
            return false;
        }
        return true;
    }

    async cancel(): Promise<void> {
        this.$bvModal.hide("commandDetails");
    }
}
</script>
