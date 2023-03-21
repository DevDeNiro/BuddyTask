<style>
span.color {
  padding: 5px;
  border-left-width: 7px;
  border-left-style: solid;
  border-left-color: rgb(47, 139, 70);
  margin-left: 10px;
}
</style>

<template>
  <div class="w-2/12 min-h-screen bg-gray-100">
    <div class="min-h-screen shadow-xl flex flex-col bg-white overflow-hidden">
      <div class="flex items-center h-20 shadow">
        <h1 class="pl-3 font-medium text-3xl uppercase text-indigo-500">
          Main
        </h1>
      </div>
      <ul class="flex shadow flex-col py-4">
        <li class="border-l-green-500">
          <a
            href="#"
            class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
          >
            <span class="color focus:border-green-400 text-sm font-medium"
              >Dashboard</span
            >
            <div :src="trashSvg"></div>
          </a>
        </li>
      </ul>
      <div class="">
        <ul class="flex shadow flex-col py-4">
          <li class="border-l-green-500">
            <a
              href="#"
              class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
            >
              <span
                class="color focus:border-green-400 text-sm font-medium"
                :class="{'focus:border-green-400': spanFocused}"
                tabindex="0"
                @focus="spanFocused = true"
                @blur="spanFocused = false"
                >Logout</span
              >
            </a>
          </li>
          <li class="border-l-green-500">
            <a
              href="#"
              class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800"
            >
              <span class="color focus:border-green-400 text-sm font-medium"
                >Setting</span
              >
            </a>
          </li>
        </ul>
        <div>All right deserves</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",
  data() {
    return {
      inputValue: "",
      inputFocused: false,
      trashSvg: "../public/trash.svg",
    };
  },
};
</script>
