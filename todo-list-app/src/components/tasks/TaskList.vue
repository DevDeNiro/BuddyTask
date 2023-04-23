<template>
  <draggable v-model="localTasks" :group="{name: 'tasks'}" @end="onDragEnd">
    <div
      v-for="(task, index) in filterTasks.incompleteTasks"
      :key="index"
      class="card cursor-pointer border border-gray-300 rounded-lg p-4 my-4"
    >
      <div class="flex">
        <div class="bg-blue-600 w-1"></div>

        <div class="flex items-center">
          <div class="flex items-center">
            <div class="h-7 w-1 bg-blue-600 mr-2"></div>
            <span class="material-icons text-xl px-2 pb-4">check</span>
          </div>
          <div class="flex flex-col ml-2">
            <h3 class="font-bold">{{ task.name }}</h3>
            <p class="text-sm">{{ task.description }}</p>
            <span class="text-sm mt-4">
              {{ formatDate(task.startDate) }} -
              {{ formatDate(task.endDate) }}
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

    <div
      v-for="(task, index) in filterTasks.completeTasks"
      :key="index"
      class="card cursor-pointer border border-gray-300 rounded-lg p-4 my-4"
    >
      <div class="flex">
        <div class="bg-blue-600 w-1"></div>

        <div class="flex items-center">
          <div class="flex items-center">
            <div class="h-7 w-1 bg-blue-600 mr-2"></div>
            <span class="material-icons text-xl px-2 pb-4">check</span>
          </div>
          <div class="flex flex-col ml-2">
            <h3 class="font-bold">{{ task.name }}</h3>
            <p class="text-sm">{{ task.description }}</p>
            <span class="text-sm mt-4">
              {{ formatDate(task.startDate) }} -
              {{ formatDate(task.endDate) }}
            </span>
          </div>
        </div>
      </div>
    </div>
  </draggable>
</template>

<script>
import {ref, computed} from "vue";
import {defineComponent} from "vue";
import draggable from "vuedraggable";

export default defineComponent({
  name: "TaskList",
  components: {
    draggable,
  },
  props: {
    tasks: {
      type: Array,
      default: () => [{}],
    },
  },
  setup(props, {emit}) {
    const localTasks = ref([...props.tasks]);

    const formatDate = (dateString) => {
      const date = new Date(dateString);
      return date.toLocaleTimeString("fr-FR", {
        hour: "2-digit",
        minute: "2-digit",
      });
    };

    const filterTasks = computed(() => {
      return {
        incompleteTasks: props.tasks.filter((task) => task.completed === false),
        completeTasks: props.tasks.filter((task) => task.completed === true),
      };
    });

    const onDragEnd = (event) => {
      const movedTask = localTasks.value[event.newIndex];
      if (event.newIndex >= filterTasks.incompleteTasks.value.length) {
        movedTask.completed = true;
      } else {
        movedTask.completed = false;
      }
      emit("update-tasks", localTasks.value);

      store.dispatch("updateTask", movedTask);
    };

    return {formatDate, filterTasks, localTasks, onDragEnd};
  },
});
</script>
