import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'), // thêm alias @ trỏ tới src
    },
  },
  css: {
    preprocessorOptions: {
      scss: {
        quietDeps: true, // ẩn tất cả warning từ node_modules (Bootstrap)
      },
    },
  },
})
