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
      class="relative bg-white flex gap-6 my-3 sm:p-4 items-center"
      v-for="task in tasks"
      :key="task.id"
    >
      <div class="flex">
        <img
          :src="completSvg"
          @click="toggleTaskCompletedStatus"
          alt="complet"
        />
        <img
          :src="uncompletSvg"
          @click="toggleTaskCompletedStatus"
          alt="uncomplet"
        />
        <img :src="penSvg" @click="editTask(task)" alt="update" />
        <img :src="deleteSvg" @click="deleteTask(task.id)" alt="delete" />
      </div>

      <div class="flex">{{ task.name }}</div>

      <div class="flex absolute right-6">
        <div class="text-dark">{{ formattedDate(task.dueDate) }}</div>
      </div>
    </div>

    <div v-if="editingTask">
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
    </div>
  </div>
</template>

<script>
import axios from "axios";
import moment from "moment";

export default {
  name: "TaskDetail",
  props: {
    tasks: Array,
    fetchTasks: Function,
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
  mounted() {
    this.fetchTasks();
    this.localTasks = this.tasks;
  },
  methods: {
    formattedDate(dueDate) {
      const momentDate = moment.utc(dueDate);
      const formattedDateString = momentDate.format("dddd Do, MMMM YYYY");
      return formattedDateString;
    },

    fetchTasks() {
      axios.get("/api/Todo").then((response) => {
        // Copie les données pour éviter de modifier directement le props
        this.localTasks = response.data;
      });
    },

    deleteTask(id) {
      axios
        .delete(`/api/Todo/${id}`)
        .then((response) => {
          this.fetchTasks();
          console.log(response);
          alert("DELETE OK");
        })
        .catch((error) => {
          console.log(error);
          // alert("Post fail");
        });
    },

    editTask(task) {
      this.editingTask = task;
    },

    updateTask() {
      axios
        .put(`/api/Todo/${this.editingTask.id}`, this.editingTask)
        .then((response) => {
          this.editingTask = null;
          this.fetchTasks();
          console.log(response);
          alert("UPDATE OK");
        });
    },
  },
};
</script>
