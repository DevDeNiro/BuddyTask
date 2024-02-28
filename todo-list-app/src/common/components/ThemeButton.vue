<template>
  <button @click="toggleSwitch" :class="{'button': true, 'is-toggled': isToggled}"
          class="text-indigo-900 py-2 px-2 rounded-full">
    <span v-if="isToggled" class="material-symbols-outlined">toggle_on</span>
    <span v-else class="material-symbols-outlined">toggle_off</span>
  </button>
</template>

<script>
import {onMounted, ref} from 'vue';

export default {
  setup() {
    const userTheme = ref("light");
    const isToggled = ref(false);
    const getTheme = () => {
      return localStorage.getItem("user-theme");
    };

    const setTheme = (theme) => {
      localStorage.setItem("user-theme", theme);
      // userTheme.value = theme;
      document.documentElement.className = theme;
      isToggled.value = theme === "dark";
    };

    const getMediaPreference = () => {
      return window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light";
    };

    const toggleSwitch = () => {
      if (isToggled.value) {
        setTheme("light");
      } else {
        setTheme("dark");
      }
    };

    onMounted(() => {
      const initUserTheme = getTheme() || getMediaPreference();
      setTheme(initUserTheme);
    });

    return {
      userTheme,
      toggleSwitch,
      isToggled,
    };
  },
};
</script>
