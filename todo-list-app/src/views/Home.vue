<template>
  <div class="flex">
    <Sidebar class="w-20" />
    <div class="flex flex-col w-full h-full pl-20">
      <Header class="w-full bg-gray" />
      <main class="main w-full flex-grow py-4 px-9">
        <CategoryList />
      </main>
    </div>
  </div>
</template>

<script>
import CategoryList from "@/components/categories/CategoryList.vue";
import Sidebar from "@/components/layout/Sidebar.vue";
import Header from "@/components/layout/Header.vue";

export default {
  components: {
    CategoryList,
    Sidebar,
    Header,
  },
};
</script>
