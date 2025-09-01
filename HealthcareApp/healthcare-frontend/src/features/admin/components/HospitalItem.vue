<template>
  <ul>
    <li>
      {{ hospital.id }} - {{ hospital.name }} - {{ hospital.address || 'Chưa có' }}
    </li>
  </ul>
</template>

<script setup lang="ts">
  import type { Hospital } from '@/shared/types/hospitalService';
  defineProps<{ hospital: Hospital }>();
</script>
