<style scoped></style>

<template>
  <div
    class="sidebar h-full fixed left-0 top-0 w-20 bg-indigo-900 flex flex-col items-center justify-between space-y-4 py-5 bg-white"
  >
    <div class="top-icon">
      <img
        class="bg-cover bg-center"
        src="../../assets/images/logo.jpg"
        alt="logo"
      />
    </div>

    <div class="buttons text-center">
      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <span class="w-7 material-symbols-outlined"> calendar_today </span>
      </button>
      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <span class="w-7 material-symbols-outlined"> event_list </span>
      </button>
      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <span class="material-symbols-outlined"> monitoring </span>
      </button>
      <button class="text-indigo-900 py-2 px-2 rounded-full">
        <span class="w-7 material-symbols-outlined"> settings </span>
      </button>
    </div>

    <div class="bottom-datetime">
      <div class="font-semibold text-purple">{{ currentTime }}</div>
      <div class="font-bold">{{ currentDate }}</div>
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
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "June",
        "July",
        "Aug",
        "Sept",
        "Oct",
        "Nov",
        "Dec",
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
