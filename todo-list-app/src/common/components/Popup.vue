<template>
  <Transition>
    <div
        v-if="popup.isVisible"
        class="fixed inset-0 z-50 flex items-center justify-center backdrop-blur-sm transition-all duration-300"
        @click.self="closePopup"
    >
      <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-sm"
           @click.stop="noop"
      >
        <div class="flex justify-between items-start pb-3">
          <p :class="typeClass" class="text-gray-800">{{ message }}</p>
          <button @click="closePopup">
            X
          </button>
        </div>
        <!--Dynamic field for props-->
        <slot></slot>
      </div>
    </div>
  </Transition>
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

    const noop = (e) => {
      e.stopPropagation();
    };

    return {
      popup,
      message,
      closePopup,
      noop,
    };
  },
};
</script>

<style>
.v-enter-from {
  opacity: 0;
  translate: -100px 0;
}

.v-enter-to {
  opacity: 1;
  translate: 0 0;
}

.v-leave-from {
  opacity: 1;
  translate: 0 0;
}

.v-leave-to {
  opacity: 0;
  translate: 100px 0;
}
</style>
