import {onMounted, onUnmounted, ref} from 'vue';

export function useCurrentTime() {
    const currentTime = ref(new Intl.DateTimeFormat("fr", {
        hour: "2-digit",
        minute: "2-digit",
    }).format(new Date()));

    let intervalId;

    function updateCurrentTime() {
        currentTime.value = new Intl.DateTimeFormat("fr", {
            hour: "2-digit",
            minute: "2-digit",
        }).format(new Date());
    }

    onMounted(() => {
        intervalId = setInterval(updateCurrentTime, 60000);
    });

    onUnmounted(() => {
        clearInterval(intervalId);
    });

    return {currentTime};
}
