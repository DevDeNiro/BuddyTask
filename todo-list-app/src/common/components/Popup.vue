<template>
  <transition name="fade">
    <div
        v-if="popup.isVisible"
        class="fixed inset-0 z-50 flex items-center justify-center backdrop-blur-sm"
    >
      <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm">
        <div class="flex justify-between items-start pb-3">
          <p class="text-gray-800" :class="typeClass">{{ message }}</p>
          <button @click="closePopup">
            X
          </button>
        </div>
        <!--Dynamic field for props-->
        <slot></slot>
      </div>
    </div>
  </transition>
</template>

<script>
import {computed} from "vue";
import {useStore} from "vuex";

export default {
  name: "Popup",
  props: {},

  setup() {
    const store = useStore();
    const popup = computed(() => store.getters.popup);
    const message = computed(() => popup.value.message);

    const closePopup = () => {
      store.dispatch("hidePopup");
    };

    return {
      popup,
      message,
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
