/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  theme: {
    colors: {
      green: "#49BE8B",
      gray: "#F8FAFB",
    },
    extend: {},
  },
  plugins: [require("@tailwindcss/forms")],
};
