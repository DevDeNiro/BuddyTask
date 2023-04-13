<template>
  <div v-if="loading" class="loading">Loading...</div>
  <div v-else class="grid grid-cols-3 gap-4 px-8 py-4">
    <div v-for="category in categories" :key="category.id" class="section">
      <h2 class="flex justify-between text-xl font-bold mb-2">
        <span
          v-if="editingCategoryId !== category.id"
          class="bg-blue-600 rounded-full h-4 mr-2"
        >
          {{ category.name }}
        </span>
        <input
          v-else
          v-model="editingCategoryName"
          @change="updateCategory(category)"
          class="bg-blue-600 rounded-full h-4 mr-2"
        />

        <div class="flex">
          <button @click="startEditingCategory(category.id)" class="mx-2">
            u
          </button>
          <button @click="deleteCategory(category.id)" class="mx-2">d</button>
        </div>
      </h2>

      <div class="flex items-center justify-between">
        <div class="flex">
          <span class="text-sm">Completed: 1/3</span>
          <div class="h-2 w-20 bg-gray-200 rounded-full mt-1">
            <div class="h-full w-1/3 bg-blue-600 rounded-full"></div>
          </div>
        </div>
      </div>

      <hr class="border-b my-7" />
      <TaskList />
    </div>
  </div>
</template>

<script>
import TaskList from "../tasks/TaskList.vue";
import TaskForm from "../tasks/TaskForm.vue";

import {ref, watch, onMounted, computed} from "vue";
import {useStore} from "vuex";

export default {
  name: "CategoryList",
  components: {TaskForm, TaskList},

  setup() {
    const store = useStore();
    const categories = ref([]);
    const editingCategory = ref(null);
    const loading = ref(true);
    const editingCategoryName = ref("");

    onMounted(async () => {
      console.log("CategoryList mounted");
      await store.dispatch("fetchCategories");
    });

    watch(
      () => store.getters.categories,
      (newCategories) => {
        console.log("Categories from store:", newCategories);
        categories.value = newCategories;
        loading.value = false;
      }
    );

    const deleteCategory = (id) => {
      store.dispatch("deleteCategory", id);
    };

    const startEditingCategory = (id) => {
      const category = categories.value.find((c) => c.id === id);
      editingCategoryName.value = category.name;
      editingCategory.value = id;
    };

    const editingCategoryId = computed(() => editingCategory.value);

    const updateCategory = (category) => {
      console.log("Editing category name:", editingCategoryName.value);
      const updatedCategory = {...category, name: editingCategoryName.value};
      store.dispatch("updateCategory", updatedCategory);
      editingCategory.value = null;
    };

    const showTaskForm = ref(false);

    const toggleTaskForm = () => {
      showTaskForm.value = !showTaskForm.value;
    };

    const submitTaskForm = (taskData) => {
      store.dispatch("addTask", taskData);
      showTaskForm.value = false;
    };

    return {
      categories,
      loading,
      showTaskForm,
      toggleTaskForm,
      submitTaskForm,
      deleteCategory,
      updateCategory,
      startEditingCategory,
      editingCategoryId,
      editingCategoryName, // Ajoutez cette ligne
    };
  },
};
</script>
