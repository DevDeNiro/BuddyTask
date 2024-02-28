<template>
  <div
      class="dark:bg-black sidebar h-full fixed left-0 top-0 w-20 bg-indigo-900 flex flex-col items-center justify-between space-y-4 py-5 bg-white"
  >
    <div class="top-icon">
      <img
          alt="logo"
          class="bg-cover bg-center"
          src="../assets/images/logo.png"
      />
    </div>

    <div class="buttons text-center">
      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <router-link to="/">
          <span class="w-7 material-symbols-outlined"> home </span>
        </router-link>
      </button>

      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <router-link to="/calendar">
          <span class="w-7 material-symbols-outlined"> calendar_month </span>
        </router-link>
      </button>

      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <span class="w-7 material-symbols-outlined"> monitoring </span>
      </button>

      <ThemeButton/>

    </div>

    <div class="bottom-datetime">
      <div class="font-semibold text-purple text-xl">{{ currentTime }}</div>
      <div class="font-bold text-base text-center">{{ currentDate }}</div>
    </div>
  </div>
</template>

<script>

import {useCurrentDate} from "@/common/composables/useCurrentDate";
import {useCurrentTime} from "@/common/composables/useCurrentTime";
import ThemeButton from "@/common/components/ThemeButton.vue";

export default {
  components: {ThemeButton},
  setup() {

    const {currentDate} = useCurrentDate();
    const {currentTime} = useCurrentTime();

    return {
      currentDate,
      currentTime,
      ThemeButton,
    };
  },
};
</script>

<style>
.button {
  transition: background-color 0.3s ease;
}
</style>


