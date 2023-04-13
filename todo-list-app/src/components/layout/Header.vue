<template>
  <header class="flex items-center justify-between py-4 px-6 bg-white">
    <div class="flex items-center space-x-4">
      <span class="text-gray-400">Date antérieure</span>
      <span class="border-b-2 border-purple-600">Today</span>
      <span class="text-gray-400">Date postérieure</span>
      <div class="border-l h-5 mx-4"></div>
      <span>...</span>
    </div>
    <div class="flex items-center space-x-4">
      <span class="font-bold">Categories :</span>
      <span>3/4</span>
      <button class="text-purple-600">&lt;</button>
      <button class="text-purple-600">&gt;</button>
      <div class="border-l h-5 mx-4"></div>
      <div class="w-8 h-8 bg-gray-300 rounded-full">
        <button @click="createCategory" class="material-icons mt-1">add</button>
      </div>
    </div>
  </header>
</template>

<script>
import {useStore} from "vuex";

export default {
  name: "Header",
  setup() {
    const store = useStore();

    const createCategory = () => {
      const newCategory = {
        name: "Nouvelle catégorie",
        createdAt: new Date().toISOString(),
        todoItems: [],
      };
      store.dispatch("createCategory", newCategory);
    };

    return {
      createCategory,
    };
  },
};
</script>

<style scoped>
header {
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1),
    0 2px 4px -1px rgba(0, 0, 0, 0.06);
}
</style>
