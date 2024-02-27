<template>
  <button
      class="inline-flex items-center justify-center w-10 h-10 mr-2 text-indigo-100 transition-colors duration-150 bg-indigo-700 rounded-full focus:shadow-outline hover:bg-indigo-800"
  >
    <svg class="w-4 h-4 fill-current" viewBox="0 0 20 20">
      <path
          clip-rule="evenodd"
          d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z"
          fill-rule="evenodd"
      ></path>
    </svg>
  </button>
</template>

<script>
export default {
  name: "CustomButton",
};
</script>
