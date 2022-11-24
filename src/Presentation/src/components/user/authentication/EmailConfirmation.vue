<template>
    <div>Hi</div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { UserGetters } from "@/helpers/vuex-enums/UserEnums";

@Component({
    components: {},
})
export default class UserLogin extends Vue {
    private async onSubmit(): Promise<void> {
        this.$toast("Login succesful. Hello " + this.$store.getters[UserGetters.GET_CURRENT_USER_NAME] + " :)"), undefined, 1000;
    }
}
</script>
