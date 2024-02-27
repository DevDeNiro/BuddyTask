<template>
  <div class="flex h-[100dvh] overflow-hidden">

    <Sidebar class="w-20"/>

    <!-- Content area -->
    <div
        class="w-full h-full pl-20 flex flex-col flex-1 overflow-y-auto overflow-x-hidden"
    >
      <main class="grow">
        <div class="px-4 sm:px-6 lg:px-8 py-8 w-full max-w-9xl mx-auto">
          <!-- Page header -->
          <div class="sm:flex sm:justify-between sm:items-center mb-4">
            <!-- Left: Title -->
            <div class="mb-4 sm:mb-0">
              <h1
                  class="text-2xl md:text-3xl text-slate-800 dark:text-slate-100 font-bold"
              >
                <span>{{ `${monthNames[month]} ${year}` }}</span> ✨
              </h1>
            </div>

            <!-- Right: Actions -->
            <div
                class="grid grid-flow-col sm:auto-cols-max justify-start sm:justify-end gap-2"
            >
              <!-- Previous month button -->
              <button
                  :disabled="month === 0"
                  class="btn px-2.5 bg-white dark:bg-slate-800 border-slate-200 dark:border-slate-700 hover:border-slate-300 dark:hover:border-slate-600 text-slate-500 hover:text-slate-600 dark:text-slate-400 dark:hover:text-slate-300 disabled:border-slate-200 dark:disabled:border-slate-700 disabled:bg-slate-100 dark:disabled:bg-slate-800 disabled:text-slate-400 dark:disabled:text-slate-600 disabled:cursor-not-allowed"
                  @click="
                  month--;
                  getDays();
                "
              >
                <span class="sr-only">Previous month</span>
                <wbr/>
                <svg class="h-4 w-4 fill-current" viewBox="0 0 16 16">
                  <path d="M9.4 13.4l1.4-1.4-4-4 4-4-1.4-1.4L4 8z"/>
                </svg>
              </button>

              <!-- Next month button -->
              <button
                  :disabled="month === 11"
                  class="btn px-2.5 bg-white dark:bg-slate-800 border-slate-200 dark:border-slate-700 hover:border-slate-300 dark:hover:border-slate-600 text-slate-500 hover:text-slate-600 dark:text-slate-400 dark:hover:text-slate-300 disabled:border-slate-200 dark:disabled:border-slate-700 disabled:bg-slate-100 dark:disabled:bg-slate-800 disabled:text-slate-400 dark:disabled:text-slate-600 disabled:cursor-not-allowed"
                  @click="
                  month++;
                  getDays();
                "
              >
                <span class="sr-only">Next month</span>
                <wbr/>
                <svg class="h-4 w-4 fill-current" viewBox="0 0 16 16">
                  <path d="M6.6 13.4L5.2 12l4-4-4-4 1.4-1.4L12 8z"/>
                </svg>
              </button>

              <hr
                  class="w-px h-full bg-slate-200 dark:bg-slate-700 border-none mx-1"
              />

              <!-- Create event button -->
              <button class="btn bg-indigo-500 hover:bg-indigo-600 text-white">
                <svg
                    class="w-4 h-4 fill-current opacity-50 shrink-0"
                    viewBox="0 0 16 16"
                >
                  <path
                      d="M15 7H9V1c0-.6-.4-1-1-1S7 .4 7 1v6H1c-.6 0-1 .4-1 1s.4 1 1 1h6v6c0 .6.4 1 1 1s1-.4 1-1V9h6c.6 0 1-.4 1-1s-.4-1-1-1z"
                  />
                </svg>
                <span class="hidden xs:block ml-2">Create Event</span>
              </button>
            </div>
          </div>

          <!-- Filters and view buttons -->
          <div class="sm:flex sm:justify-between sm:items-center mb-4">
            <!-- Filters  -->
            <div class="mb-4 sm:mb-0 mr-2">
              <div class="category-filter">
                <label v-for="category in categories" :key="category.id">
                  <input
                      v-model="selectedCategories"
                      :value="category.id"
                      type="checkbox"
                  />
                  {{ category.name }}
                </label>
              </div>
            </div>

            <!-- View buttons (requires custom integration) -->
            <div class="flex flex-nowrap -space-x-px">
              <button
                  class="btn bg-slate-50 dark:bg-slate-900 border-slate-200 dark:border-slate-700 hover:bg-slate-50 text-indigo-500 rounded-none first:rounded-l last:rounded-r"
              >
                Month
              </button>
              <button
                  class="btn bg-white dark:bg-slate-800 border-slate-200 dark:border-slate-700 hover:bg-slate-50 dark:hover:bg-slate-900 text-slate-600 dark:text-slate-300 rounded-none first:rounded-l last:rounded-r"
              >
                Week
              </button>
              <button
                  class="btn bg-white dark:bg-slate-800 border-slate-200 dark:border-slate-700 hover:bg-slate-50 dark:hover:bg-slate-900 text-slate-600 dark:text-slate-300 rounded-none first:rounded-l last:rounded-r"
              >
                Day
              </button>
            </div>
          </div>

          <!-- Calendar table -->
          <div
              class="bg-white dark:bg-slate-800 rounded-sm shadow overflow-hidden"
          >
            <!-- Days of the week -->
            <div
                class="grid grid-cols-7 gap-px border-b border-slate-200 dark:border-slate-700"
            >
              <div
                  v-for="(day, index) in dayNames"
                  :key="index"
                  class="px-1 py-3"
              >
                <div
                    class="text-slate-500 text-sm font-medium text-center lg:hidden"
                >
                  {{ day.substring(0, 3) }}
                </div>
                <div
                    class="text-slate-500 dark:text-slate-400 text-sm font-medium text-center hidden lg:block"
                >
                  {{ day }}
                </div>
              </div>
            </div>

            <!-- Day cells -->
            <div class="grid grid-cols-7 gap-px bg-slate-200 dark:bg-slate-700">
              <!-- Diagonal stripes pattern -->
              <svg class="sr-only">
                <defs>
                  <pattern
                      id="stripes"
                      height="5"
                      patternTransform="rotate(135)"
                      patternUnits="userSpaceOnUse"
                      width="5"
                  >
                    <line
                        class="stroke-current text-slate-200 dark:text-slate-700 opacity-50"
                        stroke-width="2"
                        x1="0"
                        x2="0"
                        y="0"
                        y2="5"
                    />
                  </pattern>
                </defs>
              </svg>
              <!-- Empty cells (previous month) -->
              <div
                  v-for="(blankday, index) in startingBlankDays"
                  :key="index"
                  class="bg-slate-50 dark:bg-slate-800 h-20 sm:h-28 lg:h-36"
              >
                <svg
                    height="100%"
                    width="100%"
                    xmlns="http://www.w3.org/2000/svg"
                >
                  <rect fill="url(#stripes)" height="100%" width="100%"/>
                </svg>
              </div>
              <!-- Days of the current month -->
              <div
                  v-for="day in daysInMonth"
                  :key="day"
                  class="day relative bg-white dark:bg-slate-800 h-20 sm:h-28 lg:h-36 overflow-hidden"
              >
                <div class="h-full flex flex-col justify-between borderStyle">
                  <!-- Events -->
                  <div
                      class="grow flex flex-col relative p-0.5 sm:p-1.5 overflow-hidden"
                  >
                    <button
                        v-for="(event, index) in getEvents(day)"
                        :key="index"
                        class="relative w-full text-left mb-1"
                    >
                      <div
                          :class="eventColor(event.eventColor)"
                          class="px-2 py-0.5 rounded overflow-hidden"
                      >
                        <!-- Event name -->
                        <div class="text-xs font-semibold truncate">
                          {{ event.eventName }}
                        </div>
                        <!-- Event time -->
                        <div class="text-xs uppercase truncate hidden sm:block">
                          <!-- Start date -->
                          <span v-if="event.eventStart">{{
                              event.eventStart.toLocaleTimeString([], {
                                hour12: true,
                                hour: "numeric",
                                minute: "numeric",
                              })
                            }}</span>
                          <!-- End date -->
                          <span v-if="event.eventEnd">
                            -
                            <span>{{
                                event.eventEnd.toLocaleTimeString([], {
                                  hour12: true,
                                  hour: "numeric",
                                  minute: "numeric",
                                })
                              }}</span>
                          </span>
                        </div>
                      </div>
                    </button>
                    <div
                        aria-hidden="true"
                        class="absolute bottom-0 left-0 right-0 h-4 bg-gradient-to-t from-white dark:from-slate-800 to-transparent pointer-events-none"
                    ></div>
                  </div>
                  <!-- Cell footer -->
                  <div
                      class="flex justify-between items-center p-0.5 sm:p-1.5 insetBorder"
                  >
                    <!-- More button (if more than 2 events) -->
                    <button
                        v-if="getEvents(day).length > 2"
                        class="text-xs text-slate-500 dark:text-slate-300 font-medium whitespace-nowrap text-center sm:py-0.5 px-0.5 sm:px-2 border border-slate-200 dark:border-slate-700 rounded"
                    >
                      <span class="md:hidden">+</span
                      ><span>{{ getEvents(day).length - 2 }}</span>
                      <span class="hidden md:inline">more</span>
                    </button>
                    <!-- Day number -->
                    <button
                        :class="{'text-indigo-500': isToday(day)}"
                        class="inline-flex ml-auto w-6 h-6 items-center justify-center text-xs sm:text-sm dark:text-slate-300 font-medium text-center rounded-full hover:bg-indigo-100 dark:hover:bg-slate-600"
                    >
                      {{ day }}
                    </button>
                  </div>
                </div>
              </div>
              <!-- Empty cells (next month) -->
              <div
                  v-for="(blankday, index) in endingBlankDays"
                  :key="index"
                  class="bg-slate-50 dark:bg-slate-800 h-20 sm:h-28 lg:h-36"
              ></div>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import {computed, onMounted, ref} from "vue";
import {useStore} from "vuex";
import Sidebar from "@/layout/Sidebar.vue";

export default {
  name: "Calendar",
  components: {Sidebar},
  setup() {
    const today = new Date();
    const sidebarOpen = ref(false);
    const store = useStore();
    const selectedCategories = ref([]);
    const categories = computed(() => store.getters.categories);

    // fetch categories from API on component mounted
    onMounted(async () => {
      await store.dispatch("fetchCategories");
      getDays();
    });

    // replace existing events ref with a computed property to get categories from store
    const events = computed(() => {
      let allEvents = [];

      // Only include categories that are selected
      store.getters.categories
          .filter((category) => selectedCategories.value.includes(category.id))
          .forEach((category) => {
            category.todoItems.forEach((todoItem) => {
              allEvents.push({
                eventStart: new Date(todoItem.startDate),
                eventEnd: new Date(todoItem.endDate),
                eventName: todoItem.name,
                eventColor: "red", // TODO: adjust color logic as needed
              });
            });
          });

      return allEvents;
    });

    const monthNames = [
      "January",
      "February",
      "March",
      "April",
      "May",
      "June",
      "July",
      "August",
      "September",
      "October",
      "November",
      "December",
    ];

    const dayNames = [
      "Sunday",
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday",
    ];

    const month = ref(today.getMonth());
    const year = ref(today.getFullYear());
    const daysInMonth = ref([]);
    const startingBlankDays = ref([]);
    const endingBlankDays = ref([]);

    const isToday = (date) => {
      const day = new Date(year.value, month.value, date);
      return today.toDateString() === day.toDateString();
    };

    const getEvents = (date) => {
      return events.value.filter(
          (e) =>
              new Date(e.eventStart).toDateString() ===
              new Date(year.value, month.value, date).toDateString()
      );
    };

    const getDays = () => {
      const days = new Date(year.value, month.value + 1, 0).getDate();

      // starting empty cells (previous month)
      const startingDayOfWeek = new Date(year.value, month.value).getDay();
      let startingBlankDaysArray = [];
      for (let i = 1; i <= startingDayOfWeek; i++) {
        startingBlankDaysArray.push(i);
      }

      // ending empty cells (next month)
      const endingDayOfWeek = new Date(year.value, month.value + 1, 0).getDay();
      let endingBlankDaysArray = [];
      for (let i = 1; i < 7 - endingDayOfWeek; i++) {
        endingBlankDaysArray.push(i);
      }

      // current month cells
      let daysArray = [];
      for (let i = 1; i <= days; i++) {
        daysArray.push(i);
      }

      startingBlankDays.value = startingBlankDaysArray;
      endingBlankDays.value = endingBlankDaysArray;
      daysInMonth.value = daysArray;
    };

    const eventColor = (color) => {
      switch (color) {
        case "sky":
          return "text-white bg-sky-500";
        case "indigo":
          return "text-white bg-indigo-500";
        case "yellow":
          return "text-white bg-amber-500";
        case "emerald":
          return "text-white bg-emerald-500";
        case "red":
          return "text-white bg-rose-400";
        default:
          return "";
      }
    };

    onMounted(() => {
      getDays();
    });

    return {
      sidebarOpen,
      monthNames,
      dayNames,
      month,
      year,
      daysInMonth,
      startingBlankDays,
      endingBlankDays,
      events,
      selectedCategories,
      categories,
      isToday,
      getEvents,
      getDays,
      eventColor,
    };
  },
};
</script>

<style>
.borderStyle {
  border: outset;
}

.insetBorder {
  border: inset;
}
</style>
