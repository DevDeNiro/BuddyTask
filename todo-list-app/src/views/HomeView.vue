<template>
  <div class="flex">
    <nav class="w-2/12 contents">
      <TheSidebar></TheSidebar>
    </nav>
    <div
      class="w-10/12 flex flex-col items-center py-8 min-h-screen bg-gray-100"
    >
      <AddForm></AddForm>
      <div class="self-center w-10/12 pt-5">
        <TodoListCard>
          <TodoCard></TodoCard>
        </TodoListCard>
      </div>
    </div>
  </div>
</template>

<script>
import TheSidebar from "../components/TheSidebar.vue";
import AddForm from "../components/AddForm.vue";
import TodoCard from "../components/TodoCard.vue";
import TodoListCard from "../components/TodoListCard.vue";

export default {
  name: "HomeView",
  components: {TheSidebar, AddForm, TodoCard, TodoListCard},
};
</script>
