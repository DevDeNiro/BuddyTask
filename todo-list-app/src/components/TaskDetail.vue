<style scoped>
.dialog {
  /* ajoutez ici le style pour le dialogue */
}

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
        <img :src="penSvg" @click="onUpdateTask(task)" alt="updateTask" />
        <img :src="deleteSvg" @click="deleteTask(task.id)" alt="deleteTask" />
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

    submitForm() {
      const updatedTask = {
        id: this.id,
        name: this.name,
        dueDate: this.dueDate,
      };

      console.log(updatedTask);

      this.updateTask(updatedTask);
      this.showDialog = false;
    },

    onUpdateTask(task) {
      this.showDialog = true;
      this.id = task.id;
      this.name = task.name;
      this.dueDate = task.dueDate;

      console.log(this.id, this.name, this.dueDate);
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
      showDialog: false,
      id: null,
      name: "",
      dueDate: "",

      completSvg: "/complet.svg",
      uncompletSvg: "/uncomplet.svg",
      penSvg: "/pen.svg",
      deleteSvg: "/delete.svg",
    };
  },
};
</script>
