<template>
  <transition name="fade">
    <div
      v-if="isVisible"
      class="fixed inset-0 z-50 flex items-center justify-center"
    >
      <div class="bg-white rounded shadow-lg p-6 w-full max-w-sm">
        <p class="text-gray-800" :class="typeClass">{{ message }}</p>
        <button
          @click="closePopup"
          class="mt-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
        >
          Fermer
        </button>
      </div>
    </div>
  </transition>
</template>

<script>
import {ref, computed} from "vue";
import {useStore} from "vuex";

export default {
  setup() {
    const store = useStore();
    const popup = computed(() => store.getters.popup);
    const isVisible = computed(() => popup.value.isVisible);
    const message = computed(() => popup.value.message);
    const type = computed(() => popup.value.type);

    const typeClass = computed(() => {
      const typeClasses = {
        info: "text-blue-500",
        warning: "text-yellow-500",
        error: "text-red-500",
        success: "text-green-500",
      };
      return typeClasses[type.value] || "text-gray-500";
    });

    const closePopup = () => {
      store.dispatch("hidePopup");
    };

    return {
      isVisible,
      message,
      type,
      typeClass,
      closePopup,
    };
  },
};
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
