import {defineConfig} from "vite";
import vue from "@vitejs/plugin-vue";

export default defineConfig({
  plugins: [
    vue({
      template: {
        compilerOptions: {
          isCustomElement: (tag) => ["md-linedivider"].includes(tag),
        },
      },
    }),
  ],
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:7174",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
