<template>
  <header class="flex items-center justify-between pt-4 px-9">
    <div class="flex items-center space-x-4 m-10 gap-3 text-3xl font-semibold">
      <span class="text-gray-400 text-grayUnselected dark:text-green">Nov 11</span>
      <span class="border-b-2 border-purple-600">Today</span>
      <span class="text-gray-400 text-grayUnselected">Nov 13</span>
      <div class="border-l h-5 mx-4"></div>
      <span class="material-symbols-outlined text-center text-purple">idk</span>
    </div>
    <div class="flex items-center space-x-3 text-lg">
      <span class="font-bold">Categories :</span>
      <span class="text-purple">{{ numberOfCategories }}</span>
      <div class="border-l h-5 mx-4"></div>
      <div class="w-8 h-8 bg-gray-300 rounded-full">
        <button class="material-icons mt-1" @click="createCategory">add</button>
      </div>
    </div>
  </header>
</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";

export default {
  name: "Header",
  setup() {
    const store = useStore();
    const numberOfCategories = computed(() => {
      return store.getters.categories.length;
    });

    console.log(numberOfCategories.value);

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
      numberOfCategories,
    };
  },
};
</script>
