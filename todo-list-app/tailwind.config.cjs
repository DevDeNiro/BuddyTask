/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
    darkMode: 'selector',
    theme: {
        colors: {
            white: "#FFFFFF",
            purple: "#856AFF",
            purpleBack: "#EDEBFF",
            green: "#2AE2BD",
            greenBack: "#C9F9EB", // Genereate opacity 10% of green for background card
            red: "#FF7F7E",
            redBack: "#FFE9E9",
            gray: "#F5F6FA",
            grayUnselected: "#C3C9DF",
            completed: "#FF964F",
            'primary': {
                100: '#f5f7fb',
                200: '#f6f8fb',
                300: '#f7f9fc',
                400: '#f8fafc',
                600: '#f5f7fb',
            },
            // Surface pour le darkmode
            'surface': {
                100: '#121212',
                200: '#282828',
                300: '#3f3f3f',
                400: '#575757',
                500: '#717171',
                600: '#8b8b8b',
            },
        },
        extend: {},
    },
    plugins: [require("@tailwindcss/forms"), require("tailwind-scrollbar-hide")],
};
