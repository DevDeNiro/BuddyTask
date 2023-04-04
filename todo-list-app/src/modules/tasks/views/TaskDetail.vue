<style scoped>
.blockElements {
  align-items: center;
}

img {
  align-items: center;
  margin: 0 4px;
  width: 30px;
  height: 30px;
  cursor: pointer;
  transition: 0.4s, background-position 0s;
}

img:hover {
  background-position: right;
  transform: scale(1.1);
}
</style>

<template>
  <div class="self-center">
    <div>
      <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
        completed
      </h3>
      <div class="overflow-hidden sm:rounded-md">
        <div
          class="relative shadow-lg bg-white flex gap-6 my-3 sm:p-4 items-center"
          v-for="(task, index) in completedTasks"
          :key="'completed-' + index"
        >
          <div class="flex">
            <img
              :src="completSvg"
              @click="toggleTaskStatus(task)"
              alt="complet"
            />
            <img
              :src="uncompletSvg"
              @click="toggleTaskStatus(task)"
              alt="uncomplet"
            />
            <img :src="penSvg" @click="onUpdateTask(task)" alt="updateTask" />
            <img
              :src="deleteSvg"
              @click="deleteTask(task.id)"
              alt="deleteTask"
            />
          </div>

          <div class="flex">{{ task.name }}</div>

          <div class="flex absolute right-6">
            <div class="text-red">{{ formattedDate(task.dueDate) }}</div>
          </div>
        </div>
        <h3 class="uppercase block text-sm py-3 font-medium text-gray-700">
          uncompleted
        </h3>

        <div
          class="relative shadow-lg bg-white flex gap-6 my-3 sm:p-4 items-center"
          v-for="(task, index) in uncompletedTasks"
          :key="'uncompleted-' + index"
        >
          <div class="flex">
            <img :src="completSvg" alt="complet" />
            <img :src="uncompletSvg" alt="uncomplet" />
            <img :src="penSvg" @click="onUpdateTask(task)" alt="updateTask" />
            <img
              :src="deleteSvg"
              @click="deleteTask(task.id)"
              alt="deleteTask"
            />
          </div>

          <div class="flex">{{ task.name }}</div>

          <div class="flex absolute right-6">
            <div class="text-red">{{ formattedDate(task.dueDate) }}</div>
          </div>
        </div>

        <div v-if="showDialog" class="dialog">
          <h2>Mise à jour de la tâche</h2>
          <form @submit.prevent="submitForm">
            <div class="my-5">
              <label for="name">Nom de la tâche :</label>
              <input type="text" name="name" v-model="name" required />
            </div>
            <div class="my-5">
              <label for="dueDate">Date d'échéance :</label>
              <input type="date" name="dueDate" v-model="dueDate" />
            </div>
            <button type="submit" class="m-5">Mettre à jour la tâche</button>
            <button @click="showDialog = false">Annuler</button>
          </form>
        </div>
      </div>
      <div></div>
    </div>
  </div>
</template>

<script>
import moment from "moment";
import {ref, computed, onMounted} from "vue";
import {useStore} from "vuex";

export default {
  setup() {
    const store = useStore();
    const tasks = computed(() => store.getters.tasks);
    const completedTasks = computed(() =>
      tasks.value.filter((task) => task.completed === true)
    );
    const uncompletedTasks = computed(() =>
      tasks.value.filter((task) => task.completed === false)
    );

    const showDialog = ref(false);
    const id = ref(null);
    const name = ref("");
    const dueDate = ref("");
    const completSvg = ref("/complet.svg");
    const uncompletSvg = ref("/uncomplet.svg");
    const penSvg = ref("/pen.svg");
    const deleteSvg = ref("/delete.svg");

    const fetchTasks = () => store.dispatch("fetchTasks");
    const deleteTask = (taskId) => store.dispatch("deleteTask", taskId);
    const updateTask = (task) => store.dispatch("updateTask", task);

    const submitForm = () => {
      const updatedTask = {
        id: id.value,
        name: name.value,
        dueDate: dueDate.value,
        completed: completed.value,
      };

      updateTask(updatedTask);
      showDialog.value = false;
    };

    const onUpdateTask = (task) => {
      showDialog.value = true;
      id.value = task.id;
      name.value = task.name;
      dueDate.value = task.dueDate;
      completed.value = task.completed;
    };

    const toggleTaskStatus = (task) => {
      task.completed = !task.completed;
      updateTask(task);
      console.log(updateTask(task));
    };

    const formattedDate = (dueDate) => {
      const momentDate = moment.utc(dueDate);
      const formattedDateString = momentDate.format("dddd Do, MMMM YYYY");
      return formattedDateString;
    };

    onMounted(() => {
      if (!store) {
        console.error("Vuex store is not correctly configured");
      }
      fetchTasks();
    });

    return {
      tasks,
      completedTasks,
      uncompletedTasks,
      showDialog,
      id,
      name,
      dueDate,
      completSvg,
      uncompletSvg,
      penSvg,
      deleteSvg,
      submitForm,
      onUpdateTask,
      toggleTaskStatus,
      formattedDate,
    };
  },
};
</script>
