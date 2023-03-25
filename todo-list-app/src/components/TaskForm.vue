<template>
  <div class="w-10/12 self-center">
    <p class="text-gray-700 block text-sm py-3 font-medium text-gray-700">
      Create new task
    </p>
    <!-- <alert v-if="errorMessage" message="Une erreur est survenue !" /> -->
    <form @submit.prevent="addNewTask">
      <div class="overflow-hidden shadow sm:rounded-md">
        <div class="bg-black px-4 py-5 sm:p-6">
          <div class="grid grid-cols-6 gap-6">
            <div class="col-span-6 sm:col-span-3">
              <label
                for="first-name"
                class="block text-sm font-medium text-gray-700"
                >Title</label
              >
              <input
                type="text"
                required
                v-model="newTaskTitle"
                name="name"
                id="first-name"
                autocomplete="given-name"
                placeholder="e.g. Take the bins out"
                class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
              />
            </div>

            <div class="col-span-6 sm:col-span-3">
              <label
                for="last-name"
                class="block text-sm font-medium text-gray-700"
                >Due date</label
              >
              <input
                type="date"
                @invalid="handleInvalidDate"
                required
                v-model="newTaskDate"
                name="dueDate"
                class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
              />
            </div>
          </div>
          <button
            type="submit"
            class="text-white bg-green flex justify-center text-center cursor-pointer shadow-lg font-bold p-1 w-1/6 uppercase my-3"
          >
            <span class="text-gray text-center">Create</span>
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
  data() {
    return {
      newTaskTitle: "",
      newTaskDate: "",
    };
  },
  methods: {
    ...mapActions(["addTask"]),

    addNewTask() {
      if (this.newTaskTitle.trim() !== "") {
        const newTask = {
          name: this.newTaskTitle.trim(),
          dueDate: this.newTaskDate.trim(),
        };
        this.addTask(newTask);
        this.newTaskTitle = "";
        this.newTaskDate = "";
      }
    },

    isDateValid(date) {
      const now = new Date();
      const selectedDate = new Date(date);
      return selectedDate >= now;
    },

    handleInvalidDate(event) {
      event.preventDefault();
      alert(
        "La date sélectionnée est invalide. Veuillez sélectionner une date ultérieure à la date d'aujourd'hui."
      );
    },
  },
};
</script>
