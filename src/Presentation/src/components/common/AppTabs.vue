<template>
    <ul class="nav nav-underline flex-nowrap">
        <!-- Taby -->
        <li class="nav-item" v-for="item in tabs" :key="item.value">
            <span class="nav-link text-nowrap" :class="{ active: selectedOption.value == item.value }" @click="selectedOption = item">
                <i :class="item.icon"></i>
                <span class="ml-2">{{ item.text }}</span>
            </span>
        </li>
    </ul>
</template>

<script lang="ts">
import { Option } from "@/helpers/Interfaces";
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({
    name: "",
})
export default class AppList extends Vue {
    @Prop() private tabs: Option[];
    @Prop() private value: Option;

    private get selectedOption(): Option {
        return this.value;
    }

    private set selectedOption(option: Option) {
        this.$emit("input", option);

        if (option.route && this.$route.name != option.route) {
            this.$router.push({ name: option.route });
        }
    }
}
</script>
