<template>
  <div
      :data-category-id="categoryId"
      class="task-list scrollbar-hide overflow-auto h-screen pt-8"
      style="padding-bottom: 20em"
  >
    <Draggable
        :group="{name: 'tasks', pull: true, put: true}"
        :itemKey="(task) => task.id"
        :list="filterTasks.incompleteTasks"
        class="incompleteTasks"
        @end="onDragEnd"
        @start="onDragStart"
    >
      <template #item="{element}">
        <div
            :data-task-id="element.id"
            class="card cursor-pointer border border-gray-300 bg-white rounded-lg p-2 my-4"
            @click="showSelectedTask(element)"
        >
          <div class="flex">
            <div class="bg-blue-600 w-1"></div>

            <div class="flex items-center">
              <div class="flex items-center">
                <div class="h-7 w-1 sideColor mr-2"></div>
                <span class="customColorIcon material-icons text-xl px-2"
                >check</span
                >
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
        :group="{name: 'tasks', pull: true, put: true}"
        :itemKey="(task) => task.id"
        :list="filterTasks.completeTasks"
        class="completeTasks"
        @end="onDragEnd"
        @start="onDragStart"
    >
      <template #item="{element}">
        <div
            :data-task-id="element.id"
            class="card cursor-pointer border border-gray-300 bg-white rounded-lg p-4 my-4"
            @click="showSelectedTask(element)"
        >
          <div class="flex">
            <div class="bg-blue-600 w-1"></div>

            <div class="flex items-center">
              <div class="flex items-center">
                <div class="h-7 w-1 sideColor mr-2"></div>
                <span class="customColorIcon material-icons text-xl px-2"
                >check</span
                >
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
import {computed, ref} from "vue";
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

    let oldCategoryId;
    const onDragStart = (event) => {
      oldCategoryId = event.from.parentElement.dataset.categoryId;
    };

    const onDragEnd = (event) => {
      const taskId = event.item.dataset.taskId;
      const {to, oldDraggableIndex, newDraggableIndex} = event;

      const newCategoryId = event.to.parentElement.dataset.categoryId;

      console.log(
          `oldCategoryId: ${oldCategoryId}, newCategoryId: ${newCategoryId}`
      );

      if (oldCategoryId !== newCategoryId) {
        let completed;
        if (to.className === "completeTasks") {
          completed = true;
        } else if (to.className === "incompleteTasks") {
          completed = false;
        }
        // Move task to another category
        store.dispatch("moveTask", {
          taskId,
          oldCategoryId,
          newCategoryId,
          completed,
        });
      } else {
        // Move task to another position in the same category
        const updatedTasks = [...props.tasks];

        const movedTask = updatedTasks.splice(oldDraggableIndex, 1)[0];
        updatedTasks.splice(newDraggableIndex, 0, movedTask);

        // Update task completed status
        if (to.className === "completeTasks") {
          updatedTasks[newDraggableIndex].completed = true;
        } else if (to.className === "incompleteTasks") {
          updatedTasks[newDraggableIndex].completed = false;
        }

        updatedTasks[newDraggableIndex].categoryId = props.categoryId;

        store.dispatch("updateTask", updatedTasks[newDraggableIndex]);

        localTasks.value.splice(0, localTasks.value.length, ...updatedTasks);
      }
    };

    const selectedTask = ref({});

    const showSelectedTask = () => {
      selectedTask.value = task;
      console.log("selectedTask: ", selectedTask.value);
      store.dispatch("showPopup", {
        message: "Task Detail",
        type: "info",
        taskDetail: task,
      });
    };


    // const taskStatusColorMap = {
    //   store.taskStatuses.INCOMPLETE: "bg-red-500",
    //   store.taskStatuses.COMPLETE: "bg-green-500",
    // };

    return {
      filterTasks,
      localTasks,
      selectedTask,
      formatDate,
      onDragEnd,
      onDragStart,
      showSelectedTask,
    };
  },
};
</script>

<style scoped>
.sideColor {
  background: blanchedalmond;
  height: 70px;
}

.customColorIcon {
  background: antiquewhite;
  border-radius: 50%;
  padding: 8px;
  margin: 0 10px 25px 10px;
}
</style>
