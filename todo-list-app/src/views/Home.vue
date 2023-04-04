<template>
  <div class="flex">
    <Sidebar class="w-20" />
    <div class="flex flex-col w-full">
      <Header class="w-full" />
      <main class="w-full bg-gray-100 flex-grow"></main>
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
