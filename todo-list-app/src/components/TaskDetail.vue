<style>
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
  <div class="overflow-hidden sm:rounded-md">
    <div
      class="relative shadow-lg bg-white flex gap-6 my-3 sm:p-4 items-center"
      v-for="task in tasks"
      :key="task.id"
    >
      <div class="flex">
        <img :src="completSvg" alt="complet" />
        <img :src="uncompletSvg" alt="uncomplet" />
        <img :src="penSvg" @click="onUpdateTask(task.id)" alt="updateTask" />
        <img :src="deleteSvg" @click="deleteTask(task.id)" alt="deleteTask" />
      </div>

      <div class="flex">{{ task.name }}</div>

      <div class="flex absolute right-6">
        <div class="text-red">{{ formattedDate(task.dueDate) }}</div>
      </div>
    </div>

    <!-- <form
      v-if="editingTaskId === task.id" 
      @submit.prevent="onUpdateTask(task.id, updatedName, updatedDate)"
    >
      <input type="text" v-model="updatedName" placeholder="Nouveau nom" />
      <input type="date" v-model="updatedDate" placeholder="Nouvelle date" />
      <button type="submit">Mettre à jour</button>
    </form> -->
  </div>
</template>

<script>
import moment from "moment";
import {mapGetters, mapActions} from "vuex";

export default {
  computed: {
    ...mapGetters(["tasks"]),
  },

  methods: {
    // deleteTask((taskId) => { this.$store.dispatch("DELETE_TODO", taskId); } )
    ...mapActions(["fetchTasks", "deleteTask", "updateTask"]),

    toggleEditForm(taskId) {
      this.editingTaskId = this.editingTaskId === taskId ? null : taskId;
    },

    onUpdateTask(taskId, name, date) {
      const updatedTask = {
        id: taskId,
        name: name,
        dueDate: date,
      };
      this.updateTask(updatedTask);
    },

    formattedDate(dueDate) {
      const momentDate = moment.utc(dueDate);
      const formattedDateString = momentDate.format("dddd Do, MMMM YYYY");
      return formattedDateString;
    },
  },

  mounted() {
    if (!this.$store) {
      console.error("Vuex store is not correctly configure");
    }
    this.$store.dispatch("fetchTasks");
  },

  data() {
    return {
      editingTaskId: null,
      updatedName: "",
      updatedDate: "",

      completSvg: "/complet.svg",
      uncompletSvg: "/uncomplet.svg",
      penSvg: "/pen.svg",
      deleteSvg: "/delete.svg",
    };
  },
};
</script>
