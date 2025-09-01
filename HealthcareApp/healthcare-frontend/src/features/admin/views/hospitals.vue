<template>
  <div>
    <h2>Danh sách bệnh viện</h2>
    <ul v-if="store.hospitals.length">
      <HospitalItem
        v-for="hospital in store.hospitals"
        :key="hospital.id"
        :hospital="hospital"
      />
    </ul>
    <p v-else>Không có bệnh viện nào.</p>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useHospitalStore } from '@/features/admin/store/store';
import HospitalItem from '@/features/admin/components/HospitalItem.vue';

const store = useHospitalStore();

onMounted(() => {
  store.fetchHospitals();
  console.log(store.hospitals);
});
</script>
