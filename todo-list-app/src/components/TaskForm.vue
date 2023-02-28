<template>
  <div class="w-10/12 self-center">
    <p class="block text-sm py-3 font-medium text-gray-700">Create new task</p>
    <alert v-if="errorMessage" message="Une erreur est survenue !" />
    <form @submit.prevent="createTodo">
      <div class="overflow-hidden shadow sm:rounded-md">
        <div class="bg-white px-4 py-5 sm:p-6">
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
                v-model="name"
                name="name"
                id="first-name"
                autocomplete="given-name"
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
                v-model="dueDate"
                name="dueDate"
                class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
              />
            </div>
          </div>
          <button
            type="submit"
            class="bg-white hover:bg-gray-100 text-gray-800 font-semibold py-1 px-2 mt-4 border border-gray-400 rounded shadow"
          >
            Create
          </button>
          {{ formattedDate }}
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import axios from "axios";
import Alert from "./Alert.vue";

export default {
  name: "AddForm",
  components: {Alert},
  data() {
    return {
      name: "",
      dueDate: "",
      errorMessage: "",
    };
  },
  methods: {
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

    createTodo() {
      const formData = new FormData();
      formData.append("name", this.name);
      formData.append("dueDate", this.dueDate);

      if (this.isDateValid(formData.dueDate)) {
        axios
          .post("/api/Todo", formData, {
            headers: {
              "Content-Type": "application/json",
            },
          })
          .then((response) => {
            console.log(response);
            alert("Post succeed");
          })
          .catch((error) => {
            console.log(error);
            alert("Post fail");
          });
      } else {
        this.errorMessage = "Une erreur est survenue !";
        return;
      }
    },
  },
};
</script>
