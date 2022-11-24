<template>
    <div style="display: flex; justify-content: center; align-items: center; height: 90vh">
        <ContentWrapper>
            <template #header> Error 404 </template>
            <p>We are sorry, but this page was not found.</p>
            <a href="/" style="display: block; text-align: center">Go home</a>
        </ContentWrapper>
    </div>
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
