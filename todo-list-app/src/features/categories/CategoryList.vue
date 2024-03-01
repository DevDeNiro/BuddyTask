<template>
  <div v-if="loading" class="loading flex align-center justify-center">
    <pixel-spinner
        :animation-duration="2000"
        :loading="loading"
        :pixel-size="70"
        color="#856AFF"
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
    <div v-else class="grid grid-cols-3 h-screen bg-gray gap-4 px-10 dark:bg-surface-500">
      <div
          v-for="(category, index) in categories"
          :key="category.id"
          class="section w-10/12 relative h-full scrollbar-hide"
      >
        <h2 class="flex justify-between sticky top-0 text-xl font-bold -mb-6 mx-0 pb-2 bg-gray z-50 dark:bg-surface-500">
          <span
              v-if="editingCategoryId !== category.id"
              class="bg-blue-600 rounded-full h-4 mr-2 cursor-pointer dark:text-primary-600"
              @dblclick="startEditingCategory(category.id)"
          >
            {{ category.name }}
          </span>
          <input
              v-else
              v-model="editingCategoryName"
              class="bg-blue-600 rounded-xl h-6 mr-2 "
              type="text"
              @change="updateCategory(category)"
          />

          <span class="flex dark:text-white">
            <button
                class="material-symbols-outlined mx-2"
                @click="deleteCategory(category.id)"
            >
              cancel
            </button>
          </span>
        </h2>

        <TaskList :categoryId="category.id" :tasks="category.todoItems"/>
        <CategoryTaskCounter
            :category="category"
            @toggle-task-form="toggleTaskForm"
        />
      </div>
      <div v-if="selectedCategory">
        <TaskForm :category="selectedCategory"/>
      </div>
    </div>
  </div>
</template>

<script>
// Internal imports
import TaskList from "../tasks/TaskList.vue";
import TaskForm from "../tasks/TaskForm.vue";
import {useLoading} from "@/common/composables/useLoading.js";
import {useCategories} from "@/features/categories/composables/useCategories.js";
import ProgressBar from "@/features/categories/components/ProgressBar.vue";
import CategoryTaskCounter from "@/features/categories/components/CategoryTaskCounter.vue";
// External imports
import {useStore} from "vuex";
import {computed, onMounted, ref} from "vue";
import {PixelSpinner} from "epic-spinners";

export default {
  props: {},
  name: "CategoryList",
  components: {
    CategoryTaskCounter,
    ProgressBar,
    PixelSpinner,
    TaskForm,
    TaskList
  },

  setup: function () {
    const store = useStore();

    const {loading, hasError, setLoading, setError} = useLoading();
    // const {selectedCategoryIndex, selectedCategory, toggleTaskForm} = useToggleTaskForm();
    const {
      categories,
      editingCategoryId,
      editingCategoryName,
      updateCategory,
      deleteCategory,
      startEditingCategory,
    } = useCategories();

    const isEmptyCategory = computed(() => categories.value.length === 0);

    onMounted(async () => {
      const timer = setTimeout(() => {
        if (loading.value) {
          setLoading(false);
          setError(true);
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

    // const completedTasksCount = (category) => {
    //   return category.todoItems.filter((task) => task.completed).length;
    // };

    const selectedCategoryIndex = ref(null);
    const selectedCategory = ref(null);

    const toggleTaskForm = (index) => {
      if (selectedCategoryIndex.value === index) {
        store.dispatch("hidePopup").then(r => console.log(r));
        selectedCategoryIndex.value = null;
        selectedCategory.value = null;
      } else {
        selectedCategoryIndex.value = index;
        index.value = index;
        selectedCategory.value = categories.value[index];
        store.dispatch("showPopup", {
          message: `Add new task for ${categories.value[index].name}`,
          type: "info",
        }).then(r => console.log(r));
      }
    };

    return {
      categories,
      selectedCategoryIndex,
      selectedCategory,
      editingCategoryId,
      editingCategoryName,
      isEmptyCategory,
      loading, hasError, setLoading, setError,
      startEditingCategory,
      CategoryTaskCounter,
      // completedTasksCount,
      updateCategory,
      deleteCategory,
      toggleTaskForm,
    };
  },
};
</script>

<style scoped>
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
}

.text-orange {
  color: orange;
  gap: 5px;
}

.completedTask {
  width: -webkit-fill-available;
  width: 9rem;
}
</style>
