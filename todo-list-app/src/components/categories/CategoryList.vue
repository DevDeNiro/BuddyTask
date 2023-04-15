<template>
  <div v-if="loading" class="loading">Loading...</div>
  <div v-else class="grid grid-cols-3 gap-4 px-8 py-4">
    <div
      v-for="(category, index) in categories"
      :key="category.id"
      class="section"
    >
      <h2 class="flex justify-between text-xl font-bold mb-2">
        <span
          v-if="editingCategoryId !== category.id"
          class="bg-blue-600 rounded-full"
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
        <span class="text-sm">Completed: 1/3</span>
        <button @click="toggleTaskForm(index)" class="button">add</button>
      </div>

      <hr class="border-b my-7" />
      <TaskList :tasks="category.todoItems" />
    </div>
    <div
      v-if="selectedCategory"
      :message="`Ajouter une nouvelle tâche pour ${selectedCategory.name}`"
      :type="'info'"
    >
      <TaskForm :category="selectedCategory" />
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
    const loading = ref(true);
    const categories = ref([]);
    const editingCategory = ref(null);
    const editingCategoryName = ref("");

    onMounted(async () => {
      await Promise.all([
        store.dispatch("fetchCategories"),
        // store.dispatch("fetchTasks"),
      ]);
    });

    // Reactive state to get local copies from store in 'categories'
    watch(
      () => store.getters.categories,
      (newCategories) => {
        console.log("Categories from store:", newCategories);
        categories.value = newCategories;
        loading.value = false;
      }
    );

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

    const deleteCategory = (id) => {
      store.dispatch("deleteCategory", id);
    };

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
          message: `Ajouter une nouvelle tâche pour ${categories.value[index].name}`,
          type: "info",
        });
      }
    };

    return {
      categories,
      loading,
      editingCategoryId,
      editingCategoryName,
      startEditingCategory,
      deleteCategory,
      updateCategory,
      toggleTaskForm,
      selectedCategoryIndex,
      selectedCategory,
    };
  },
};
</script>
