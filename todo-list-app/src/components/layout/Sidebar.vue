<style scoped></style>

<template>
  <div
    class="sidebar h-full fixed left-0 top-0 w-20 bg-indigo-900 flex flex-col items-center justify-between space-y-4 py-5"
  >
    <div class="top-icon">
      <i class="">icone</i>
    </div>

    <div class="buttons">
      <button class="bg-white text-indigo-900 py-2 px-2 rounded-full">
        Button 1
      </button>
      <button class="bg-white text-indigo-900 py-2 px-2 rounded-full">
        Button 2
      </button>
      <button class="bg-white text-indigo-900 py-2 px-2 rounded-full">
        Button 3
      </button>
      <button class="bg-white text-indigo-900 py-2 px-2 rounded-full">
        Button 4
      </button>
    </div>

    <div class="bottom-datetime text-white">
      <div>{{ currentTime }}</div>
      <div>{{ currentDate }}</div>
    </div>
  </div>
</template>

<script>
import {ref, onMounted, computed} from "vue";

export default {
  setup() {
    const now = ref(new Date());

    function updateDateTime() {
      now.value = new Date();
      setTimeout(updateDateTime, 60000);
    }

    onMounted(() => {
      updateDateTime();
    });

    function formatDateWithoutPeriod(date) {
      const monthNames = [
        "janv",
        "févr",
        "mars",
        "avr",
        "mai",
        "juin",
        "juil",
        "août",
        "sept",
        "oct",
        "nov",
        "déc",
      ];

      const day = date.getDate();
      const monthIndex = date.getMonth();

      return `${monthNames[monthIndex]} ${day}`;
    }

    const currentDate = computed(() => {
      return formatDateWithoutPeriod(now.value);
    });

    const currentTime = computed(() => {
      const timeFormatter = new Intl.DateTimeFormat("fr", {
        hour: "2-digit",
        minute: "2-digit",
      });
      return timeFormatter.format(now.value);
    });

    return {
      currentDate,
      currentTime,
    };
  },
};
</script>
