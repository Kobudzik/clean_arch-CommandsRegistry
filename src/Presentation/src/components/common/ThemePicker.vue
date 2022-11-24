<template>
    <ColorPicker :disable-alpha="true" :disable-fields="true" :value="themeColor" @input="updatePickedColor" style="margin: auto" />
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Chrome } from "vue-color";

@Component({
    components: {
        ColorPicker: Chrome,
    },
})
export default class ThemePicker extends Vue {
    private get themeColor(): string {
        return getComputedStyle(document.body).getPropertyValue("--primary-color");
    }

    private set themeColor(color: string) {
        this.$emit("input", color);
        document.body.style.setProperty("--primary-color", color);
    }

    private set themeColorRgb(color: string) {
        document.body.style.setProperty("--primary-color-rgb", color);
    }

    private get themeColorRgb(): string {
        return getComputedStyle(document.body).getPropertyValue("--primary-color-rgb");
    }

    private updatePickedColor(event: { rgba: { r: number; g: number; b: number } }) {
        this.themeColor = `rgb(${event.rgba.r},${event.rgba.g},${event.rgba.b})`;
        this.themeColorRgb = `${event.rgba.r},${event.rgba.g},${event.rgba.b}`;
    }
}
</script>
