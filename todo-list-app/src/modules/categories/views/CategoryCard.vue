<template>
  <div class="bg-white rounded-lg shadow-md p-4">
    <div class="text-xl font-bold mb-2">{{ category.name }}</div>
    <!-- Ajoutez ici d'autres détails sur la catégorie si nécessaire -->
  </div>
</template>

<script>
export default {
  props: {
    category: {
      type: Object,
      required: true,
    },
  },
};
</script>
