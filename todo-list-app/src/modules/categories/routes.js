import CategoryList from "./views/CategoryList.vue";
import CategoryCard from "./views/CategoryCard.vue";

export default [
  {
    path: "/categories",
    name: "categories",
    component: CategoryList,
    children: [
      {
        path: ":id",
        name: "category",
        component: CategoryCard,
      },
    ],
  },
];
