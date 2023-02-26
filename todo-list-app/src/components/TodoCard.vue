<style>
.blockElements {
  align-items: center;
}

.svgToAction {
  align-items: center;
  width: 35px;
}
</style>

<template>
  <div class="overflow-hidden shadow sm:rounded-md">
    <div
      class="relative bg-white flex gap-6 px-4 py-5 sm:p-6 items-center"
      v-for="(item, index) in items"
      :key="index"
    >
      <div class="flex">
        <!--svg here-->
        <img class="svgToAction" v-bind:src="completSvg" alt="complet" />
        <img class="svgToAction" v-bind:src="uncompletSvg" alt="uncomplet" />
        <img class="svgToAction" v-bind:src="penSvg" alt="update" />
        <img class="svgToAction" v-bind:src="deleteSvg" alt="delete" />
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
      completSvg: "",
      uncompletSvg: "",
      penSvg: "",
      deleteSvg: "",
    };
  },

  mounted() {
    // Récupérer les URLs des SVG stockés dans le localStorage
    const svgFiles = [
      "../public/complet.svg",
      "../public/uncomplet.svg",
      "../public/pen.svg",
      "../public/delete.svg",
    ];

    svgFiles.forEach((filePath) => {
      const url = localStorage.getItem(filePath);
      if (url) {
        this[`${filePath.split("/").pop().split(".")[0]}Svg`] = url;
      }
    });

    axios
      .get("/api/Todo")
      .then((response) => {
        this.items = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
  methods: {
    handleClick1() {
      // Do something when the first SVG is clicked
    },
    handleClick2() {
      // Do something when the second SVG is clicked
    },
    handleClick3() {
      // Do something when the third SVG is clicked
    },
    handleClick3() {
      // Do something when the third SVG is clicked
    },
  },
};
</script>
