import {computed, ref} from 'vue';
import {useStore} from 'vuex';

export function useCategories() {
    const store = useStore();
    const editingCategory = ref(null);
    const editingCategoryName = ref("");

    const categories = computed(() => store.getters.categories);
    const editingCategoryId = computed(() => editingCategory.value);

    const startEditingCategory = (id) => {
        const category = categories.value.find(c => c.id === id);
        editingCategoryName.value = category.name;
        editingCategory.value = id;
    };

    const updateCategory = (category) => {
        const updatedCategory = {...category, name: editingCategoryName.value};
        store.dispatch("updateCategory", updatedCategory).then(r => console.log(r));
        editingCategory.value = null;
    };

    const deleteCategory = (id) => {
        store.dispatch("deleteCategory", id).then(r => console.log(r));
    };

    return {
        categories,
        editingCategoryId,
        editingCategoryName,
        updateCategory,
        deleteCategory,
        startEditingCategory,
    };
}
