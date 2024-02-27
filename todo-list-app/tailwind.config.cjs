/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
    theme: {
        colors: {
            white: "#FFFFFF",
            purple: "#856AFF",
            green: "#2AE2BD",
            greenBack: "#C9F9EB",
            red: "#FF7F7E",
            redBack: "#FFE9E9",
            gray: "#F5F6FA",
            grayUnselected: "#C3C9DF",
            completed: "#FF964F",
        },
        extend: {},
    },
    plugins: [require("@tailwindcss/forms"), require("tailwind-scrollbar-hide")],
};
