<template>
  <Sidebar />
  <div class="ml-20">
    <CategoryList />
  </div>
  <Popup />
</template>

<script>
import CategoryList from "@/modules/categories/components/CategoryList.vue";
import Sidebar from "./components/Sidebar.vue";
import Popup from "./components/Popup.vue";

export default {
  components: {
    CategoryList,
    Sidebar,
    // Autres composants de modules
  },
};
</script>
