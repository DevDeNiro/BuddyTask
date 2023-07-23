<template>
  <div
    class="task-list scrollbar-hide overflow-auto h-screen pt-8"
    style="padding-bottom: 20em"
  >
    <Draggable
      :list="filterTasks.incompleteTasks"
      :group="{name: 'tasks'}"
      :itemKey="(task) => task.id"
      @end="onDragEnd"
      class="incompleteTasks"
    >
      <template #item="{element}">
        <div
          class="card cursor-pointer border border-gray-300 bg-white rounded-lg p-4 my-4"
          @click="handleCardClick(element)"
          :data-task-id="element.id"
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

    <div class="relative my-6 flex items-center">
      <div class="flex-grow border-t border-gray-500 border-dashed"></div>
      <span class="mx-4 text-gray-400 uppercase font-semibold">done</span>
      <div class="flex-grow border-t border-gray-500 border-dashed"></div>
    </div>

    <Draggable
      :list="filterTasks.completeTasks"
      :group="{name: 'tasks'}"
      :itemKey="(task) => task.id"
      @end="onDragEnd"
      class="completeTasks"
    >
      <template #item="{element}">
        <div
          class="card cursor-pointer border border-gray-300 bg-white rounded-lg p-4 my-4"
          @click="handleCardClick(element)"
          :data-task-id="element.id"
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
import Draggable from "vuedraggable";
import {useStore} from "vuex";

export default {
  name: "TaskList",
  components: {
    Draggable,
  },
  props: {
    tasks: {
      type: Array,
      default: () => [{}],
    },
    categoryId: {
      type: String,
      required: true,
    },
  },
  setup(props) {
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
      console.log("event", event);

      const taskId = event.item.dataset.taskId; // récupérer l'id de la tâche
      const {to, oldIndex, newIndex} = event;

      // let movedTaskIndex = props.tasks.findIndex((task) => task.id === taskId);
      let updatedTasks = [...props.tasks];

      // First, move the task within the array
      let movedTask = updatedTasks.splice(oldIndex, 1)[0];
      updatedTasks.splice(newIndex, 0, movedTask);

      // Then, update the task's state and category id
      if (to.className === "completeTasks") {
        updatedTasks[newIndex].completed = true;
      } else if (to.className === "incompleteTasks") {
        updatedTasks[newIndex].completed = false;
      }

      updatedTasks[newIndex].categoryId = props.categoryId;

      store.dispatch("updateTask", updatedTasks[newIndex]);

      // Finally, update localTasks
      localTasks.value.splice(0, localTasks.value.length, ...updatedTasks);

      console.log("taskId", taskId);
      console.log("updatedTasks", updatedTasks);
    };

    return {
      formatDate,
      filterTasks,
      localTasks,
      onDragEnd,
      handleCardClick,
    };
  },
};
</script>

<style scoped></style>
