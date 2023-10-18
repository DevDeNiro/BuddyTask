<template>
  <div class="flex">
    <Popup>
      <div v-if="popup.taskDetail">
        <h3>{{ popup.taskDetail.name }}</h3>
        <p>{{ popup.taskDetail.description }}</p>
      </div>
    </Popup>
  </div>
</template>

<script>
import {ref, computed} from "vue";
import {useStore} from "vuex";
import Popup from "../common/Popup.vue";

export default {
  name: "TaskDetail",
  components: {
    Popup,
  },

  setup() {
    const store = useStore();
    const popup = computed(() => store.getters.popup);

    const showTaskDetail = (task) => {
      store.dispatch("showPopup", {
        message: "Task Detail",
        type: "info", // info, success, warning, error
        taskDetail: task,
      });
    };

    return {
      showTaskDetail,
      popup,
    };
  },
};
</script>

<style scoped></style>
