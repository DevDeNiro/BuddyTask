<script setup>

import {ref} from "vue";
import store from "@/store/index.js";

const selectedCategoryIndex = ref(null);
const selectedCategory = ref(null);
const toggleTaskForm = (index) => {
  if (selectedCategoryIndex.value === index) {
    store.dispatch("hidePopup");
    selectedCategoryIndex.value = null;
    selectedCategory.value = null;
  } else {
    selectedCategoryIndex.value = index;
    selectedCategory.value = categories.value[index];
    store.dispatch("showPopup", {
      message: `Add new task for ${categories.value[index].name}`,
      type: "info",
    });
  }
};
</script>

<template>

</template>

<style scoped>

</style>