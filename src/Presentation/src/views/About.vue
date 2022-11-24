<template>
    <ContentWrapper>
        <template #header>
            <h4>About</h4>
        </template>
        <p>Informations about us will be here soon...</p>
    </ContentWrapper>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ContentWrapper from "@/components/layout/ContentWrapper.vue";

@Component({
    components: {
        ContentWrapper,
    },
})
export default class About extends Vue {}
</script>
