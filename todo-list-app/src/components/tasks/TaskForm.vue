<template>
  <form @submit.prevent="submitForm">
    <Popup>
      <div v-for="(field, index) in fields" :key="index">
        <label :for="field.name" class="block">{{ field.label }}</label>
        <input
          :id="field.name"
          :name="field.name"
          :type="field.type"
          :placeholder="field.placeholder"
          class="border rounded p-1 w-full"
          v-model="formData[field.name]"
          required
        />
      </div>
      <button @click.prevent="submitForm">Create</button>
    </Popup>
  </form>
</template>

<script>
import {ref, computed} from "vue";
import Popup from "../common/Popup.vue";
import {useStore} from "vuex";

export default {
  name: "TaskForm",
  props: {
    category: {
      type: Object,
      default: () => ({}),
    },
  },
  components: {
    Popup,
  },

  setup(props) {
    const store = useStore();
    const popup = computed(() => store.getters.popup);

    const fields = ref([
      {
        name: "name",
        label: "Titre",
        type: "text",
        placeholder: "Entrez un titre",
        class: "border rounded p-1 w-full",
      },
      {
        name: "startDate",
        label: "Date de début",
        type: "date",
        placeholder: "Sélectionnez une date",
        class: "border rounded p-1 w-full",
      },
      {
        name: "endDate",
        label: "Date de fin",
        type: "date",
        placeholder: "Sélectionnez une date",
        class: "border rounded p-1 w-full",
      },
      {
        name: "description",
        label: "Description",
        type: "textarea",
        placeholder: "Entrez une description",
        class: "border rounded p-1 w-full",
      },
    ]);

    const formData = ref({}); // ref to store data from form

    const submitForm = () => {
      // add category to task
      formData.value.categoryId = props.category.id;

      store
        .dispatch("addTask", formData.value)
        .then(() => {})
        .finally(() => {
          store.dispatch("hidePopup");
        });
    };

    return {
      fields,
      popup,
      formData,
      submitForm,
    };
  },
};
</script>
