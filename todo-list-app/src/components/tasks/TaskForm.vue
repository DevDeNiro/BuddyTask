<template>
  <Popup :message="'Veuillez remplir les champs suivants :'" :type="'info'">
    <div v-for="(field, index) in fields" :key="index">
      <label :for="field.name" class="block">{{ field.label }}</label>
      <input
        :id="field.name"
        :name="field.name"
        :type="field.type"
        :placeholder="field.placeholder"
        class="border rounded p-1 w-full"
      />
    </div>
  </Popup>
  <button @click.prevent="$emit('submit', {title, dueDate, description})">
    Submit
  </button>
</template>

<script>
import {ref} from "vue";
import Popup from "../common/Popup.vue";

export default {
  components: {
    Popup,
  },
  setup() {
    const fields = ref([
      {
        name: "title",
        label: "Titre",
        type: "text",
        placeholder: "Entrez un titre",
      },
      {
        name: "dueDate",
        label: "Date d'échéance",
        type: "date",
        placeholder: "Sélectionnez une date",
      },
      {
        name: "description",
        label: "Description",
        type: "textarea",
        placeholder: "Entrez une description",
      },
    ]);

    return {
      fields,
    };
  },
};
</script>
