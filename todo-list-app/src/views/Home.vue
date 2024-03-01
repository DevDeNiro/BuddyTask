<template>
  <div class="flex h-[100dvh] overflow-hidden dark:bg-surface-500">
    <Sidebar class="w-20 dark:bg-surface-400" />
    <div class="flex flex-col w-full h-full pl-20 dark:bg-surface-500">
      <Header class="w-full bg-gray dark:bg-surface-500"/>
      <main class="main w-full flex-grow py-4 px-9 dark:bg-surface-500">
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
