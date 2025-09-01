<template>
  <div class="d-flex flex-column min-vh-100">
    <Header />
    <main class="flex-grow-1 p-4">
      <router-view />
    </main>
  </div>
</template>

<script setup lang="ts">
  import Header from './Header.vue';
</script>
