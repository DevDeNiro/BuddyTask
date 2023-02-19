<style></style>

<template>
  <div class="self-center w-10/12 pt-5">
    <p class="uppercase block text-sm py-3 font-medium text-gray-700">to do</p>
    <div class="overflow-hidden shadow sm:rounded-md">
      <div class="relative bg-white flex gap-6 px-4 py-5 sm:p-6">
        <div class="flex">
          <!--svg here-->
          1 2 3 4
        </div>

        <div class="flex" v-for="user in users" :key="user.id">
          <!--name here-->
          Ajouter le plugin 2.5
        </div>

        <!-- <div class="flex justify-between"> -->
        <div class="flex absolute right-6">
          <div class="">01/01/2001</div>
        </div>
        <!-- </div> -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      users: [],
    };
  },
  mounted() {
    axios
      .get("https://monapi.com/users")
      .then((response) => {
        this.users = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
</script>
