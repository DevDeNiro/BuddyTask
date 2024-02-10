<template>
  <form @submit.prevent="submitForm" CLASS="flex flex-col">
    <Popup>
      <!-- Titre -->
      <div class="mx-auto">
        <label :for="fields[0].name" class="block">{{ fields[0].label }}</label>
        <input
            :id="fields[0].name"
            :name="fields[0].name"
            :type="fields[0].type"
            :placeholder="fields[0].placeholder"
            class="border rounded p-1 w-full my-1"
            v-model="formData[fields[0].name]"
            required
        />
      </div>

      <!-- Dates group -->
      <div class="flex justify-between max-[768]:flex-column my-1">
        <div>
          <label :for="fields[1].name" class="block mb-1">{{ fields[1].label }}</label>
          <input
              :id="fields[1].name"
              :name="fields[1].name"
              :type="fields[1].type"
              :placeholder="fields[1].placeholder"
              class="border rounded  w-full "
              v-model="formData[fields[1].name]"
              required
          />
        </div>
        <div>
          <label :for="fields[2].name" class="block mb-1">{{ fields[2].label }}</label>
          <input
              :id="fields[2].name"
              :name="fields[2].name"
              :type="fields[2].type"
              :placeholder="fields[2].placeholder"
              class="border rounded w-full"
              v-model="formData[fields[2].name]"
              required
          />
        </div>
      </div>

      <!-- Description -->
      <div class="mx-auto my-2">
        <label :for="fields[3].name" class="block mb-1">{{ fields[3].label }}</label>
        <textarea
            :id="fields[3].name"
            :name="fields[3].name"
            :placeholder="fields[3].placeholder"
            class="border rounded p-1 w-full h-24 my-1"
            v-model="formData[fields[3].name]"
            required
        ></textarea>
      </div>

      <button type="submit"
              class="font-bold"
              @click.prevent="submitForm">Create
      </button>
    </Popup>
  </form>
</template>


<script>
import {computed, ref} from "vue";
import Popup from "../../common/components/Popup.vue";
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
      },
      {
        name: "startDate",
        label: "Date de début",
        type: "date",
        placeholder: "Sélectionnez une date",
      },
      {
        name: "endDate",
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

    const formData = ref({}); // ref to store data from form

    const submitForm = () => {
      // add category to task
      formData.value.categoryId = props.category.id;

      store
          .dispatch("addTask", formData.value)
          .then(() => {
          })
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