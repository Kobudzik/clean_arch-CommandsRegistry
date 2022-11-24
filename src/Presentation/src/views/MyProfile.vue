<template>
    <UserForm :userId="userId" />
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UserForm from "@/components/user/UserForm.vue";

@Component({
    components: {
        UserForm,
    },
})
export default class MyProfile extends Vue {
    private get userId(): string {
        return this.currentUser.id;
    }
}
</script>
