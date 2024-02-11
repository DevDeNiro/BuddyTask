import {ref} from 'vue';

export function useLoading() {
    const loading = ref(true);
    const hasError = ref(false);

    const setLoading = (state) => {
        loading.value = state;
    };

    const setError = (state) => {
        hasError.value = state;
    };

    return {loading, hasError, setLoading, setError};
}
