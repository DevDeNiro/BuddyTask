/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  theme: {
    colors: {
      green: "#4bc18b",
      gray: "#F8FAFB",
      red: "#e63c3c",
      GreyText: "#999",
    },
    extend: {},
  },
  plugins: [require("@tailwindcss/forms")],
};
