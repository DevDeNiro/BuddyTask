<template>
  <div class="grid grid-cols-3 gap-4 px-8 py-4">
    <div
      v-for="category in getCategories.value"
      :key="category.id"
      class="section"
    >
      <h2 class="text-xl font-bold mb-2">
        <span class="bg-blue-600 rounded-full h-4 w-4 mr-2"></span>
        {{ category.name }}
      </h2>

      <div class="flex items-center justify-between">
        <div class="flex">
          <span class="text-sm">Completed: 1/3</span>
          <div class="h-2 w-20 bg-gray-200 rounded-full mt-1">
            <div class="h-full w-1/3 bg-blue-600 rounded-full"></div>
          </div>
        </div>
        <button
          class="bg-blue-600 text-white rounded-full p-2 flex items-center"
          @click="toggleTaskForm"
        >
          <span class="material-icons">add</span>

          <span class="ml-1">Add Task</span>
          <TaskForm v-if="showTaskForm" @submit="submitTaskForm" />
        </button>
      </div>

      <hr class="border-b my-7" />
      <TaskList />
    </div>
  </div>
</template>

<script>
import TaskList from "../tasks/TaskList.vue";
import TaskForm from "../tasks/TaskForm.vue";

import {ref, computed, onMounted} from "vue";
import {useStore, mapActions} from "vuex";

export default {
  name: "CategoryList",
  components: {TaskForm, TaskList},

  setup() {
    const store = useStore();

    const getCategories = computed(() => store.getters.categories);

    onMounted(() => {
      store.dispatch("fetchCategories");
    });

    const showTaskForm = ref(false);

    const toggleTaskForm = () => {
      showTaskForm.value = !showTaskForm.value;
    };

    const submitTaskForm = (taskData) => {
      store.dispatch("addTask", taskData);
      showTaskForm.value = false;
    };

    return {
      getCategories,
      showTaskForm,
      toggleTaskForm,
      submitTaskForm,
    };
  },
};
</script>
