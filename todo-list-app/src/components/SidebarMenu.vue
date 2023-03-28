<style>
span.color {
  padding: 5px;
  border-left-width: 7px;
  border-left-style: solid;
  border-left-color: #49be8b;
  margin-left: 10px;
}
</style>

<template>
  <div class="w-2/12 static h-full bg-gray-100">
    <div class="w-2/12 h-full fixed shadow-xl flex flex-col">
      <div class="flex items-center h-20 shadow">
        <h1 class="text-green pl-3 text-3xl uppercase font-bold">Harlist</h1>
      </div>
      <ul class="flex shadow flex-col py-4">
        <li class="border-l-green-500">
          <a
            href="#"
            class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
          >
            <span class="color focus:border-green-400 text-sm font-medium"
              >Dashboard</span
            >
          </a>
        </li>

        <li class="border-l-green-500">
          <a
            href="#"
            class="flex flex-row items-center btn h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
            @click="openModal"
          >
            <span class="color focus:border-green-400 text-sm font-medium"
              >Add categorie</span
            >
          </a>
        </li>
      </ul>
      <div class="">
        <ul class="flex shadow flex-col py-4">
          <li class="border-l-green-500">
            <a
              href="#"
              class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
            >
              <span
                class="color focus:border-green-400 text-sm font-medium"
                tabindex="0"
                >Logout</span
              >
            </a>
          </li>
          <li class="border-l-green-500">
            <a
              href="#"
              class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
            >
              <span class="color focus:border-green-400 text-sm font-medium"
                >Setting</span
              >
            </a>
          </li>
        </ul>
        <div>All right deserves</div>
      </div>
    </div>

    <Dialog v-model:open="isOpen" as="div" class="fixed inset-0 z-10">
      <div class="min-h-screen px-4 text-center">
        <Dialog.Overlay class="fixed inset-0 bg-black opacity-30" />

        <span class="inline-block h-screen align-middle" aria-hidden="true"
          >&#8203;</span
        >

        <div
          class="inline-block w-full max-w-md p-6 my-8 overflow-hidden text-left align-middle transition-all transform bg-white shadow-xl rounded-2xl"
        >
          <div class="flex justify-between items-start">
            <Dialog.Title class="text-lg font-medium leading-6 text-gray-900">
              Add a new category
            </Dialog.Title>
            <button
              @click="closeModal"
              class="text-gray-600 hover:text-gray-800"
            >
              ×
            </button>
          </div>

          <form @submit.prevent="submitCategory" class="mt-4">
            <input
              v-model="categoryName"
              type="text"
              placeholder="Category name"
              class="w-full p-2 border border-gray-300 rounded"
            />

            <div class="mt-4 flex justify-end">
              <button type="submit" class="btn-primary">Save</button>
            </div>
          </form>
        </div>
      </div>
    </Dialog>
  </div>
</template>

<script>
import {Dialog} from "@headlessui/vue";
import {ref} from "vue";
import {useStore} from "vuex";

export default {
  components: {
    Dialog,
  },
  setup() {
    const store = useStore();
    const isOpen = ref(false);
    const categoryName = ref("");

    function openModal() {
      isOpen.value = true;
    }

    function closeModal() {
      isOpen.value = false;
    }

    function submitCategory() {
      if (categoryName.value.trim() !== "") {
        const newCategory = {
          name: categoryName.value.trim(),
        };
        store.dispatch("addCategory", newCategory);
        isOpen.value = false;
        categoryName.value = "";
      }
    }

    return {
      isOpen,
      categoryName,
      openModal,
      closeModal,
      submitCategory,
    };
  },
};
</script>
