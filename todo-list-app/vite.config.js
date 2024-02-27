import {defineConfig} from "vite";
import vue from "@vitejs/plugin-vue";
import path from "path";

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
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "src/"),
        },
    },

    server: {
        // host: "0.0.0.0",
        // port: parseInt(process.env.PORT) || 5173,

        proxy: {
            "/api": {
                target: "https://localhost:7174",
                changeOrigin: true,
                secure: false,
            },
        },
    },
});
