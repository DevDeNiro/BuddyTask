<template>
  <Popup
    :is-visible="popup.isVisible"
    @update:is-visible="updateIsVisible($event)"
    :message="'Veuillez remplir les champs suivants :'"
    :type="'info'"
  >
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
    <button @click.prevent="submitForm">Submit</button>
  </Popup>
</template>

<script>
import {ref, computed} from "vue";
import Popup from "../common/Popup.vue";
import {useStore} from "vuex";

export default {
  components: {
    Popup,
  },
  setup() {
    const store = useStore();
    const popup = computed(() => store.getters.popup);

    const fields = ref([
      {
        name: "title",
        label: "Titre",
        type: "text",
        placeholder: "Entrez un titre",
      },
      {
        name: "startDate",
        label: "Date de début",
        type: "date",
        placeholder: "Sélectionnez une date",
      },
      {
        name: "startDate",
        label: "Date de fin",
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

    const submitForm = () => {
      $emit("submit", formData.value);
    };

    const updateIsVisible = (isVisible) => {
      if (isVisible) {
        store.dispatch("showPopup", {
          message: "Veuillez remplir les champs suivants :",
          type: "info",
        });
      } else {
        store.dispatch("hidePopup");
      }
    };

    return {
      fields,
      submitForm,
      popup,
      updateIsVisible,
    };
  },
};
</script>
