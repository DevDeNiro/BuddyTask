/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  theme: {
    colors: {
      Green: "#49BE8B",
    },
    extend: {},
  },
  plugins: [require("@tailwindcss/forms")],
};
