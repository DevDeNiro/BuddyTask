<style>
.blockElements {
  align-items: center;
}

.svgToAction {
  align-items: center;
  width: 35px;
}
</style>

<!-- v-for="(item, index) in items"
:key="index"  -->

<template>
  <div class="overflow-hidden shadow sm:rounded-md">
    <div
      class="relative bg-white flex gap-6 px-4 py-5 sm:p-6 items-center"
      v-for="task in tasks"
      :key="task.id"
    >
      <div class="flex">
        <!--svg here-->
        <img
          class="svgToAction"
          @click="toggleTaskCompletedStatus(task, task.completed)"
          v-bind:src="completSvg"
          alt="complet"
        />

        <img
          class="svgToAction"
          @click="toggleTaskCompletedStatus(task, !task.completed)"
          v-bind:src="uncompletSvg"
          alt="uncomplet"
        />

        <img
          class="svgToAction"
          @click="editTask(task)"
          v-bind:src="penSvg"
          alt="update"
        />

        <img
          class="svgToAction"
          @click="deleteTask(task.id)"
          v-bind:src="deleteSvg"
          alt="delete"
        />

        <!-- <button @click="toggleTaskCompletedStatus(task, !task.completed)">
          {{ task.completed ? "Terminé" : "En cours" }}
        </button> -->
      </div>

      <div class="flex">{{ task.name }}</div>

      <div class="flex absolute right-6">
        <div class="">{{ task.dueDate }}</div>
      </div>
    </div>

    <div v-if="editingTask">
      <h2>Modifier une tâche</h2>
      <form @submit.prevent="updateTask">
        <label for="title">Titre</label>
        <input type="text" id="title" v-model="editingTask.title" />

        <label for="description">Description</label>
        <textarea id="description" v-model="editingTask.description"></textarea>

        <button type="submit">Enregistrer</button>
      </form>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "TodoCard",

  data() {
    return {
      tasks: [],
      editingTask: null,

      completSvg: "",
      uncompletSvg: "",
      penSvg: "",
      deleteSvg: "",
    };
  },

  mounted() {
    // Récupérer les URLs des SVG stockés dans le localStorage
    const svgFiles = [
      "../public/complet.svg",
      "../public/uncomplet.svg",
      "../public/pen.svg",
      "../public/delete.svg",
    ];

    svgFiles.forEach((filePath) => {
      const url = localStorage.getItem(filePath);
      if (url) {
        this[`${filePath.split("/").pop().split(".")[0]}Svg`] = url;
      } else console.log(url);
    });

    this.fetchTasks();
  },
  methods: {
    fetchTasks() {
      axios.get("/api/Todo").then((response) => {
        this.tasks = response.data;
      });
    },

    deleteTask(id) {
      axios.delete(`/api/Todo/${id}`).then(() => {
        this.fetchTasks();
      });
    },

    editTask(task) {
      this.editingTask = task;
    },

    updateTask() {
      axios
        .put(`/api/Todo/${this.editingTask.id}`, this.editingTask)
        .then(() => {
          this.editingTask = null;
          this.fetchTasks();
        });
    },

    // toggleTaskCompletedStatus(task) {
    //   task.completed = !task.completed;
    //   axios.put(`/api/Todo/${task.id}`, task).then(() => {
    //     this.fetchTasks();
    //   });
    // },

    toggleTaskCompletedStatus(task, status) {
      task.completed = status;
      axios.put(`/api/Todo/${task.id}`, task).then(() => {
        this.fetchTasks();
      });
    },
  },
};
</script>
