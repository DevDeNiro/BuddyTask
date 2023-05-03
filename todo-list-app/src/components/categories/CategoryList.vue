<template>
  <div v-if="loading" class="loading flex align-center justify-center">
    <pixel-spinner
      :animation-duration="2000"
      :pixel-size="70"
      color="#856AFF"
      :loading="loading"
    />
  </div>
  <div v-else class="grid grid-cols-3 bg-gray gap-4 px-10">
    <div
      v-for="(category, index) in categories"
      :key="category.id"
      class="section w-10/12"
    >
      <h2 class="flex justify-between text-xl font-bold mb-2">
        <span
          v-if="editingCategoryId !== category.id"
          class="bg-blue-600 rounded-full"
          @dblclick="startEditingCategory(category.id)"
        >
          {{ category.name }}
        </span>
        <input
          v-else
          type="text"
          v-model="editingCategoryName"
          @change="updateCategory(category)"
          class="bg-blue-600 rounded-full h-4 mr-2"
        />

        <div class="flex">
          <button
            @click="deleteCategory(category.id)"
            class="material-symbols-outlined mx-2"
          >
            cancel
          </button>
        </div>
      </h2>

      <TaskList :tasks="category.todoItems" />
      <!-- @update-tasks="updateTasks(category, $event)" -->
      <div class="">
        <hr class="border-b my-7" />
        <div class="flex items-center justify-between">
          <span class="text-sm">Completed: 1/3</span>
          <button @click="toggleTaskForm(index)" class="button">add</button>
        </div>
      </div>
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
import {PixelSpinner} from "epic-spinners";
import TaskList from "../tasks/TaskList.vue";
import TaskForm from "../tasks/TaskForm.vue";

import {ref, onMounted, computed} from "vue";
import {useStore} from "vuex";

export default {
  props: {
    categories: Array,
  },
  name: "CategoryList",
  components: {PixelSpinner, TaskForm, TaskList},

  setup() {
    const store = useStore();
    const loading = ref(true);
    const editingCategory = ref(null);
    const editingCategoryName = ref("");

    onMounted(async () => {
      try {
        await Promise.all([store.dispatch("fetchCategories")]);
        loading.value = false;
      } catch (error) {
        console.error(error);
      }
    });

    const categories = computed(() => store.getters.categories);

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

    /*

    const updateTasks = async (category, newTasks) => {
      category.todoItems = newTasks;

      try {
        await store.dispatch("updateCategory", category);
        console.log("Category updated successfully.");
      } catch (error) {
        console.error("Failed to update category:", error);
      }
    };

    */
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
      // updateTasks,
    };
  },
};
</script>
