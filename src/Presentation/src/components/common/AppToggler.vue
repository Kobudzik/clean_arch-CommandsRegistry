<template>
    <div>
        <span class="switcher">
            <input @click="toggle" title="Toggle chart" :checked="!value" type="checkbox" id="switcher" />
            <label for="switcher"></label>
        </span>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({})
export default class Loader extends Vue {
    @Prop() value: boolean;

    private toggle(): void {
        this.$emit("input", !this.value);
    }
}
</script>

<style lang="scss" scoped>
$width: 120px;
$height: 40px;
$checked: black;
$not-checked: hsla(0, 100%, 0%, 0.1);

span.switcher {
    position: relative;
    text-rendering: auto;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;

    input {
        appearance: none;
        position: relative;

        width: $width;
        height: $height;
        border-radius: 25px;

        background-color: var(--primary-color);
        outline: none;

        &:before,
        &:after {
            position: absolute;
            top: 5%;
        }

        &:before {
            font-family: "Font Awesome 5 Free";
            content: "\f2c9";
            display: inline-block;
            vertical-align: middle;
            font-weight: 900;
            font-size: 1.5rem;

            left: 20px;
        }

        &:after {
            font-family: "Font Awesome 5 Free";
            content: "\f043";
            display: inline-block;
            vertical-align: middle;
            font-weight: 900;
            font-size: 1.5rem;

            right: 20px;
        }
    }

    &.switcher {
        input {
            &:checked {
                &:before {
                    color: $not-checked;
                    transition: color 0.5s 0.05s;
                }
                &:after {
                    color: $checked;
                    transition: color 0.5s;
                }
            }
            &:not(:checked) {
                &:before {
                    color: $checked;
                    transition: color 0.5s;
                }
                &:after {
                    color: $not-checked;
                    transition: color 0.5s 0.05s;
                }
            }
        }
    }
}
</style>
