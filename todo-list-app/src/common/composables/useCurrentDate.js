import {ref} from 'vue';

export function useCurrentDate() {
    const now = ref(new Date());

    function formatDateWithoutPeriod(date) {
        const monthNames = [
            "Jan", "Feb", "Mar", "Apr", "May", "June",
            "July", "Aug", "Sept", "Oct", "Nov", "Dec",
        ];
        const day = date.getDate();
        const monthIndex = date.getMonth();
        return `${monthNames[monthIndex]} ${day}`;
    }

    const currentDate = ref(formatDateWithoutPeriod(now.value));

    return {currentDate};
}
