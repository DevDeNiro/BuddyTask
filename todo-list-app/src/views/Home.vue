<template>
  <div class="flex">
    <Sidebar class="w-20 bg-green" />
    <div class="flex flex-col w-full pl-20">
      <Header class="w-full bg-red" />
      <main class="main w-full flex-grow">
        <div class="grid grid-cols-3 gap-4 px-8 py-4">
          <div v-for="n in 3" :key="n" class="section">
            <h2 class="text-xl font-bold mb-2">
              <span class="bg-blue-600 rounded-full h-4 w-4 mr-2"></span
              >Category Title
            </h2>

            <div
              class="card cursor-pointer border border-gray-300 rounded-lg p-4"
            >
              <div class="flex">
                <div class="bg-blue-600 w-1 mr-4"></div>

                <div class="flex items-center">
                  <div class="flex items-center">
                    <div class="h-7 w-1 bg-blue-600 mr-2"></div>
                    <span class="material-icons text-xl px-2 pb-4">check</span>
                  </div>
                  <div class="flex flex-col ml-2">
                    <h3 class="font-bold">Task Title</h3>
                    <p class="text-sm">Task Description</p>
                    <span class="text-sm mt-4">12:00 - 13:00</span>
                  </div>
                </div>
              </div>
            </div>

            <hr class="border-b my-7" />

            <div class="flex items-center justify-between">
              <div class="flex">
                <span class="text-sm">Completed: 1/3</span>
                <div class="h-2 w-20 bg-gray-200 rounded-full mt-1">
                  <div class="h-full w-1/3 bg-blue-600 rounded-full"></div>
                </div>
              </div>
              <button
                class="bg-blue-600 text-white rounded-full p-2 flex items-center"
              >
                <span class="material-icons">add</span>
                <span class="ml-1">Add Task</span>
              </button>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import CategoryList from "../modules/categories/views/CategoryList.vue";
import Sidebar from "../components/layout/Sidebar.vue";
import Header from "../components/layout/Header.vue";
export default {
  components: {
    CategoryList,
    Sidebar,
    Header,
  },
};
</script>
