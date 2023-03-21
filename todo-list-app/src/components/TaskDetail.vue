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
        <img :src="penSvg" @click="editTask(task)" alt="updateTask" />
        <img :src="deleteSvg" @click="deleteTask(task.id)" alt="deleteTask" />
      </div>

      <div class="flex">{{ task.name }}</div>

      <div class="flex absolute right-6">
        <div class="text-dark">{{ formattedDate(task.dueDate) }}</div>
      </div>
    </div>

    <!-- <div v-if="editingTask">
      <h2>Modifier une putain de tâche</h2>
      <form @submit.prevent="updateTask">
        <label for="name">Titre</label>
        <input type="text" id="name" v-model="editingTask.name" />

        <label for="dueDate">Description</label>
        <textarea
          type="date"
          id="dueDate"
          v-model="editingTask.dueDate"
        ></textarea>

        <button type="submit">Enregistrer</button>
      </form>
    </div> -->
  </div>
</template>

<script>
import moment from "moment";
import {mapGetters} from "vuex";

export default {
  computed: {
    ...mapGetters(["tasks"]),
  },

  methods: {
    deleteTask(taskId) {
      this.$store.commit("DELETE_TODO", taskId);
    },
    editTask(updateTask) {
      this.$store.commit("UPDATE_TODO", updateTask);
    },
  },

  mounted() {
    if (!this.$store) {
      console.error("Le store Vuex n'est pas correctement configuré");
    }
    this.$store.dispatch("fetchTasks");
  },

  data() {
    return {
      editingTask: null,
      // Copie vide car props en lecture seul
      localTasks: [],
      completSvg: "/complet.svg",
      uncompletSvg: "/uncomplet.svg",
      penSvg: "/pen.svg",
      deleteSvg: "/delete.svg",
    };
  },

  // props: {
  //   tasks: Array,
  //   // fetchTasks: Function,
  // },

  // mounted() {
  //   // this.fetchTasks();
  //   // this.localTasks = this.tasks;
  // },
  methods: {
    formattedDate(dueDate) {
      const momentDate = moment.utc(dueDate);
      const formattedDateString = momentDate.format("dddd Do, MMMM YYYY");
      return formattedDateString;
    },
  },

  //   fetchTasks() {
  //     axios.get("/api/tasks").then((response) => {
  //       // Copie les données pour éviter de modifier directement le props
  //       this.localTasks = response.data;
  //     });
  //   },

  //   deleteTask(id) {
  //     axios
  //       .delete(`/api/tasks/${id}`)
  //       .then((response) => {
  //         this.fetchTasks();
  //         console.log(response);
  //         alert("DELETE OK");
  //       })
  //       .catch((error) => {
  //         console.log(error);
  //         // alert("Post fail");
  //       });
  //   },

  //   editTask(task) {
  //     this.editingTask = task;
  //   },

  //   updateTask() {
  //     axios
  //       .put(`/api/tasks/${this.editingTask.id}`, this.editingTask)
  //       .then((response) => {
  //         this.editingTask = null;
  //         this.fetchTasks();
  //         console.log(response);
  //         alert("UPDATE OK");
  //       });
  //   },
  // },
};
</script>
