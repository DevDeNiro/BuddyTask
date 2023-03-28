<template>
    <div>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        <CategoryCard v-for="category in categories" :key="category.id" :category="category" />
      </div>
  
      <button
        class="bg-green-500 text-white px-4 py-2 mt-4 rounded hover:bg-green-600"
        @click="openModal"
      >
        Ajouter une catégorie
      </button>
  
      <transition name="fade">
        <div v-if="showModal" class="fixed z-10 inset-0 overflow-y-auto" aria-labelledby="modal-title">
          <!-- Votre code modal ici -->
          <div class="flex items-center justify-center min-h-screen pt-4 px-4 pb-20 text-center">
            <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" @click="closeModal"></div>
            <div class="bg-white rounded-lg px-4 pt-5 pb-4 shadow-xl transform sm:w-auto sm:p-6">
              <div class="text-lg font-bold mb-4">Ajouter une catégorie</div>
              <form @submit.prevent="submitCategory">
                <input
                  type="text"
                  v-model="categoryName"
                  placeholder="Nom de la catégorie"
                  class="w-full px-3 py-2 mb-4 border border-gray-300 rounded"
                />
                <button
                  type="submit"
                  class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                >
                  Ajouter
                </button>
              </form>
              <button class="absolute top-2 right-2" @click="closeModal">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  class="h-6 w-6 text-gray-400 hover:text-gray-800"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M6 18L18 6M6 6l12 12"
                  />
                </svg>
              </button>
            </div>
          </div>
        </div>
      </transition>
    </div>
  </template>
  
  <script>
  import { ref } from "vue";
  import CategoryCard from "./CategoryCard.vue";
  import { useStore } from "vuex";
  
  export default {
    components: {
      CategoryCard,
    },
    setup() {
      const store = useStore();
      const categories = store.state.categories;
      const showModal = ref(false);
      const categoryName = ref("");
  
      function openModal() {
        showModal.value = true;
      }
  
      function closeModal() {
        showModal.value = false;
      }
  
      function submitCategory() {
        if (categoryName.value.trim() !== "") {
          const newCategory = {
            name: categoryName.value.trim(),
          };
          store.dispatch("addCategory", newCategory);
          closeModal();
          categoryName.value = "";
        }
      }
  
      return {
        categories,
        showModal,
        categoryName,
        openModal,
        closeModal,
        submitCategory,
      };
    },
  };
  </script>
  
  <style>
  .fade-enter-active,
  .fade-leave-active {
    transition: opacity 0.3s
}