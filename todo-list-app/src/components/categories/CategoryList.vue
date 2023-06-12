<template>
  <div v-if="loading" class="loading flex align-center justify-center">
    <pixel-spinner
      :animation-duration="2000"
      :pixel-size="70"
      color="#856AFF"
      :loading="loading"
    />
  </div>
  <div v-if="hasError" class="error-message">
    oops, an error occurred, waiting for the server to respond.
  </div>
  <div v-else>
    <div v-if="isEmptyCategory">
      <p>
        Whoa, it's pretty empty in here! Why not create your first category?
      </p>
    </div>
    <div v-else class="grid grid-cols-3 h-screen bg-gray gap-4 px-10">
      <div
        v-for="(category, index) in categories"
        :key="category.id"
        class="section w-10/12 relative h-full scrollbar-hide"
      >
        <h2
          class="flex justify-between sticky top-0 text-xl font-bold -mb-6 mx-0 pb-2 bg-gray z-50"
        >
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

        <div class="categoryBottom bg-gray">
          <hr class="border-b my-4" />

          <div class="flex items-center justify-between categoryCompleted">
            <span class="text-sm">Completed: 1/3</span>
            <button @click="toggleTaskForm(index)" class="button">add</button>
          </div>
        </div>
      </div>
      <div v-if="selectedCategory" :type="'info'">
        <TaskForm :category="selectedCategory" />
      </div>
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
  props: {},
  name: "CategoryList",
  components: {PixelSpinner, TaskForm, TaskList},

  setup() {
    const store = useStore();
    const loading = ref(true);
    const editingCategory = ref(null);
    const editingCategoryName = ref("");
    const hasError = ref(false);

    onMounted(async () => {
      const timer = setTimeout(() => {
        if (loading.value) {
          loading.value = false;
          hasError.value = true;
        }
      }, 30000);

      try {
        await store.dispatch("fetchCategories");
      } catch (error) {
        console.error(error);
        hasError.value = true;
      } finally {
        clearTimeout(timer);
        if (!hasError.value) {
          loading.value = false;
        }
      }
    });

    const categories = computed(() => store.getters.categories);
    const isEmptyCategory = computed(
      () => store.getters.categories.length === 0
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
          message: `Add new task on ${categories.value[index].name}`,
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
      isEmptyCategory,
      hasError,
    };
  },
};
</script>

<style>
.categorieTask {
  height: 100vh;
  display: flex;
  flex-direction: column;
}

.categoryBottom {
  position: sticky;
  left: 0;
  bottom: 0;
  width: 100%;
  padding-bottom: 15px;
}

.categoryCompleted {
  position: relative;
  width: 100%;
  padding: 10px 0;
  margin-bottom: 20px;
}
</style>
