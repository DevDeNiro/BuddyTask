import CategoryList from "./views/CategoryList.vue";
import CategoryCard from "./views/CategoryCard.vue";

const checkDataFetched = async (to, from, next) => {
  if (store.state.categories.categories.length > 0) {
    next();
  } else {
    await store.dispatch("showPopup", {
      message: "Les données n'ont pas été récupérées. Veuillez réessayer.",
      type: "error",
    });

    next(false); // Pass 'false' to cancel the navigation
  }
};

const categoriesRoutes = [
  {
    path: "/",
    name: "CategoryList",
    component: CategoryList,
    beforeEnter: checkDataFetched,
  },
  {
    path: "/",
    name: "CategoryCard",
    component: CategoryCard,
  },
];

export default categoriesRoutes;
