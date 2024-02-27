<template>
  <div class="flex h-[100dvh] overflow-hidden">
    <Sidebar class="w-20"/>
    <div class="flex flex-col w-full h-full pl-20">
      <Header class="w-full bg-gray"/>
      <main class="main w-full flex-grow py-4 px-9">
        <CategoryList/>
      </main>
    </div>
  </div>
</template>

<script>
import CategoryList from "@/features/categories/CategoryList.vue";
import Sidebar from "@/layout/Sidebar.vue";
import Header from "@/layout/Header.vue";

export default {
  components: {
    CategoryList,
    Sidebar,
    Header,
  },
};
</script>
