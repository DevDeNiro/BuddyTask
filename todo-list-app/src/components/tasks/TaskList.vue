<template>
  <div class="scrollbar-hide h-screen pb-16">
    <Draggable
      :list="filterTasks.incompleteTasks"
      :group="{name: 'tasks'}"
      :itemKey="(task) => task.id"
      @end="onDragEnd"
    >
      <template #item="{element}">
        <div
          class="card cursor-pointer border border-gray-300 bg-white rounded-lg p-4 my-4"
          @click="handleCardClick(element)"
        >
          <div class="flex">
            <div class="bg-blue-600 w-1"></div>

            <div class="flex items-center">
              <div class="flex items-center">
                <div class="h-7 w-1 bg-blue-600 mr-2"></div>
                <span class="material-icons text-xl px-2 pb-4">check</span>
              </div>
              <div class="flex flex-col ml-2">
                <div>
                  <h3 class="font-bold">{{ element.name }}</h3>
                  <p class="text-sm">{{ element.description }}</p>
                </div>
                <span class="text-sm mt-4">
                  <span
                    >{{ formatDate(element.startDate) }} -
                    {{ formatDate(element.endDate) }}</span
                  >
                </span>
              </div>
            </div>
          </div>
        </div>
      </template>
    </Draggable>

    <div class="relative flex py-5 items-center decoration-dashed">
      <div class="flex-grow border-t border-gray-400"></div>
      <span class="flex-shrink mx-4 text-gray-400 uppercase font-semibold"
        >done</span
      >
      <div class="flex-grow border-t border-gray-400"></div>
    </div>

    <Draggable
      :list="filterTasks.completeTasks"
      :group="{name: 'tasks'}"
      :itemKey="(task) => task.id"
      @end="onDragEnd"
    >
      <template #item="{element}">
        <div
          class="card cursor-pointer border border-gray-300 bg-white rounded-lg p-4 my-4"
          @click="handleCardClick(element)"
        >
          <div class="flex">
            <div class="bg-blue-600 w-1"></div>

            <div class="flex items-center">
              <div class="flex items-center">
                <div class="h-7 w-1 bg-blue-600 mr-2"></div>
                <span class="material-icons text-xl px-2 pb-4">check</span>
              </div>
              <div class="flex flex-col ml-2">
                <div>
                  <h3 class="font-bold">{{ element.name }}</h3>
                  <p class="text-sm">{{ element.description }}</p>
                </div>
                <span class="text-sm mt-4">
                  <span
                    >{{ formatDate(element.startDate) }} -
                    {{ formatDate(element.endDate) }}</span
                  >
                </span>
              </div>
            </div>
          </div>
        </div>
      </template>
    </Draggable>
  </div>
</template>

<script>
import {ref, computed} from "vue";
import {defineComponent} from "vue";
import Draggable from "vuedraggable";
import {useStore} from "vuex";

export default defineComponent({
  name: "TaskList",
  components: {
    Draggable,
  },
  props: {
    tasks: {
      type: Array,
      default: () => [{}],
    },
  },
  setup(props, {emit}) {
    const store = useStore();
    const localTasks = ref(props.tasks);

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

    const handleCardClick = (task) => {
      console.log(task);
    };

    const onDragEnd = (event) => {
      const incompleteTasks = filterTasks.value.incompleteTasks;
      if (incompleteTasks) {
        const movedTask = localTasks.value[event.newIndex];
        if (event.newIndex >= incompleteTasks.length) {
          movedTask.completed = true;
        } else {
          movedTask.completed = false;
        }
        // emit("update-tasks", localTasks.value);

        store.dispatch("updateTask", movedTask);
      }
    };

    return {formatDate, filterTasks, localTasks, onDragEnd, handleCardClick};
  },
});
</script>
