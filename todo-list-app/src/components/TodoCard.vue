<style></style>

<template>
  <div class="overflow-hidden shadow sm:rounded-md">
    <div
      class="relative bg-white flex gap-6 px-4 py-5 sm:p-6"
      v-for="(item, index) in items"
      :key="index"
    >
      <div class="flex">
        <!--svg here-->
        1 2 3 4
      </div>

      <div class="flex">{{ item.name }}</div>

      <div class="flex absolute right-6">
        <div class="">{{ item.dueDate }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "TodoCard",

  data() {
    return {
      items: [],
    };
  },

  mounted() {
    axios
      .get("/api/Todo")
      .then((response) => {
        this.items = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
</script>
