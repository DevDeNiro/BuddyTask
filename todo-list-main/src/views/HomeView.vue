<template>
  <div class="flex">
    <nav class="w-3/12 contents">
      <TheSidebar></TheSidebar>
    </nav>
    <div class="w-9/12 min-h-screen flex-row bg-gray-100">
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