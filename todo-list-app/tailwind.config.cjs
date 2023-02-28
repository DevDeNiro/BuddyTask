/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  theme: {
    colors: {
      light: "#67e8f9",
      DEFAULT: "#06b6d4",
      dark: "#0e7490",
    },
    extend: {},
  },
  plugins: [require("@tailwindcss/forms")],
};
