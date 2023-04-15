<template>
  <div
    v-for="(task, index) in tasks"
    :key="index"
    class="card cursor-pointer border border-gray-300 rounded-lg p-4"
  >
    <div class="flex">
      <div class="bg-blue-600 w-1 mr-4"></div>

      <div class="flex items-center">
        <div class="flex items-center">
          <div class="h-7 w-1 bg-blue-600 mr-2"></div>
          <span class="material-icons text-xl px-2 pb-4">check</span>
        </div>
        <div class="flex flex-col ml-2">
          <h3 class="font-bold">{{ task.name }}</h3>
          <p class="text-sm">{{ task.description }}</p>
          <span class="text-sm mt-4">
            {{ formatDate(task.StartDate) }} -
            {{ formatDate(task.EndDate) }}
          </span>
        </div>
      </div>
    </div>
  </div>
  <div class="relative flex py-5 items-center decoration-dashed">
    <div class="flex-grow border-t border-gray-400"></div>
    <span class="flex-shrink mx-4 text-gray-400 uppercase font-semibold"
      >done</span
    >
    <div class="flex-grow border-t border-gray-400"></div>
  </div>
</template>

<script>
import {defineComponent} from "vue";

export default defineComponent({
  name: "TaskList",
  props: {
    tasks: {
      type: Array,
      default: () => [],
    },
  },
  setup() {
    const formatDate = (dateString) => {
      const date = new Date(dateString);
      return date.toLocaleTimeString("fr-FR", {
        hour: "2-digit",
        minute: "2-digit",
      });
    };

    return {formatDate};
  },
});
</script>
