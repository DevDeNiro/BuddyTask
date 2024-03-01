<template>
  <div
      class="dark:bg-surface-100 sidebar h-full fixed left-0 top-0 w-20 bg-indigo-900 flex flex-col items-center justify-between space-y-4 py-5 bg-white"
  >
    <div class="top-icon flex">
      <img
          alt="logo"
          class="bg-cover bg-center"
          :src="logoPath"
      />
    </div>

    <div class="buttons text-center">
      <button class="py-2 px-2 rounded-full text-indigo-900 dark:text-primary-600">
        <router-link to="/">
          <span class="w-7 material-symbols-outlined">home</span>
        </router-link>
      </button>

      <button class="py-2 px-2 rounded-full text-indigo-900 dark:text-white">
        <router-link to="/calendar">
          <span class="w-7 material-symbols-outlined">calendar_month</span>
        </router-link>
      </button>

      <button class="py-2 px-2 rounded-full text-indigo-900 dark:text-white">
        <span class="w-7 material-symbols-outlined">monitoring</span>
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
import {ref, watchEffect} from "vue";
import logoWhite from '@/assets/images/logo-white.svg';
import logoBlack from '@/assets/images/logo-black.svg';

export default {
  components: {ThemeButton},
  setup() {

    const {currentDate} = useCurrentDate();
    const {currentTime} = useCurrentTime();

    const theme = ref(localStorage.getItem('user-theme') || 'light'); // Valeur par défaut
    const logoPath = ref(theme.value === 'dark' ? logoWhite : logoBlack);

    watchEffect(() => {
      logoPath.value = theme.value === 'dark' ? logoWhite : logoBlack;
    });

    // Exemple de mise à jour de thème (à adapter selon votre logique de changement de thème)
    window.addEventListener('themeChanged', () => {
      theme.value = localStorage.getItem('user-theme');
    });


    return {
      currentDate,
      currentTime,
      ThemeButton,
      logoPath,
    };
  },
};
</script>

<style>
.button {
  transition: background-color 0.3s ease;
}
</style>


