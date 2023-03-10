<template>
  <div class="flex">
    <nav class="w-2/12 contents">
      <SidebarMenu></SidebarMenu>
    </nav>
    <div
      class="w-10/12 bg-gray-200 flex flex-col items-center py-8 min-h-screen"
    >
      <TaskForm></TaskForm>
      <div class="self-center w-10/12 pt-5">
        <TaskList> </TaskList>
      </div>
    </div>
  </div>
</template>

<script>
import SidebarMenu from "../components/SidebarMenu.vue";
import TaskForm from "../components/TaskForm.vue";
import TaskDetail from "../components/TaskDetail.vue";
import TaskList from "../components/TaskList.vue";

export default {
  name: "HomeView",
  components: {SidebarMenu, TaskForm, TaskDetail, TaskList},
};
</script>
