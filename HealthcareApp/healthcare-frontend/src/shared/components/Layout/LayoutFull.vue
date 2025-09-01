<!-- LayoutFull.vue -->
<template>
  <div class="d-flex flex-column min-vh-100">
    <!-- Header -->
    <Header />

    <!-- Sub-header -->
    <HeaderSub />

    <!-- Nội dung chính -->
    <main class="flex-grow-1 p-4">
      <router-view />
    </main>

    <!-- Footer -->
    <Footer />
  </div>
</template>

<script setup lang="ts">
import Header from './Header.vue';
import Footer from './Footer.vue';
</script>
