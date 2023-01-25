<template>
  <div class="flex">
    <div class="w-3/12 min-h-screen flex flex-row bg-gray-100">
      <TheSidebar></TheSidebar>
    </div>
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