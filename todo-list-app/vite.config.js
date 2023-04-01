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
    host: "0.0.0.0",
    port: parseInt(process.env.PORT) || 3000, // Utilisez le port par défaut si la variable d'environnement PORT n'est pas définie
    proxy: {
      "/api": {
        target: "http://localhost:7174", // Utilisez http au lieu de https
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
