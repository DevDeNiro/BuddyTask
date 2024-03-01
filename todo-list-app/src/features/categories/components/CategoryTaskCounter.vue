<template>
  <div class="categoryBottom bg-gray dark:bg-surface-500">
    <hr class="border-b my-4 dark:text-primary-600"/>

    <div class="flex justify-between">
      <div class="completedTask">
        <div class="flex items-center relative categoryCompleted">
                <span class="text-sm font-bold dark:text-primary-600">
                  Completed:
                  <span class="text-orange dark:text-primary-600">
                    {{ completedTasksCount }}/{{ totalTasks }}
                  </span>
                </span>
        </div>
        <ProgressBar
            :completed="completedTasksCount"
            :total="category.todoItems.length"
        />
      </div>

      <button class="button" @click="$emit('toggle-task-form')">
        <span class="material-symbols-outlined"> Add task </span>
      </button>
    </div>
  </div>
</template>

<script>
import {computed} from "vue";
import ProgressBar from "@/features/categories/components/ProgressBar.vue";

export default {
  components: {ProgressBar},
  props: ['category'],

  setup(props) {
    const completedTasksCount = computed(() => {
      return props.category.todoItems.filter(task => task.completed).length;
    });

    const totalTasks = computed(() => {
      return props.category.todoItems.length;
    });

    return {
      completedTasksCount,
      totalTasks
    }
  }
}
</script>
