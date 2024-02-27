import {ref} from "vue";
import {useStore} from "vuex";

export function useToggleTaskForm() {
    const selectedCategoryIndex = ref(null);
    const selectedCategory = ref(null);

    const store = useStore();
    const categories = store.getters.categories;

    const toggleTaskForm = (index) => {
        if (selectedCategoryIndex.value === index) {
            store.dispatch("hidePopup").then(r => console.log(r));
            selectedCategoryIndex.value = null;
            selectedCategory.value = null;
        } else {
            selectedCategoryIndex.value = index;
            selectedCategory.value = categories.value[index];
            store.dispatch("showPopup", {
                message: `Add new task for ${categories.value[index].name}`,
                type: "info",
            }).then(r => console.log(r));
        }
    };

    return {
        selectedCategoryIndex,
        selectedCategory,
        toggleTaskForm,
    };
}