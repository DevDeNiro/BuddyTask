<template>
  <header class="flex items-center justify-between pt-4 px-9">
    <div class="flex items-center space-x-4 m-10 gap-3 text-3xl font-semibold">
      <span class="text-gray-400 text-grayUnselected">Nov 11</span>
      <span class="border-b-2 border-purple-600">Today</span>
      <span class="text-gray-400 text-grayUnselected">Nov 13</span>
      <div class="border-l h-5 mx-4"></div>
      <span class="material-symbols-outlined text-center text-purple text-4xl">
        more_horiz
      </span>
    </div>
    <div class="flex items-center space-x-3 text-lg">
      <span class="font-bold">Categories :</span>
      <span class="text-purple">3/4</span>
      <span class="material-symbols-outlined"> chevron_left </span>
      <span class="material-symbols-outlined"> chevron_right </span>
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
