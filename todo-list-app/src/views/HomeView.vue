<template>
  <div class="flex">
    <nav class="w-2/12 contents">
      <TheSidebar></TheSidebar>
    </nav>
    <div class="w-10/12 flex flex-col items-center py-8 min-h-screen bg-gray-100">
      <addForm></addForm>
      <TodoCard></TodoCard>
    </div>
  </div>
</template>

<script>

import TheSidebar from '../components/TheSidebar.vue';
import addForm from '../components/addForm.vue'
import TodoCard from '../components/TodoCard.vue'

export default {
    name: "HomeView",
    components: { TheSidebar, addForm, TodoCard }
};
</script>