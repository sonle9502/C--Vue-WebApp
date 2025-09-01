<template>
  <div class="register-container">
    <div class="register-card">
      <h2 class="card-title">Register</h2>
      <RegisterForm :hospitals="hospitalStore.hospitals" />
      <p class="mt-3 text-center">
        Already have an account?
        <router-link to="/login" class="text-decoration-none">Login</router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useHospitalStore } from '@/features/admin/store/store';
import RegisterForm from '@/features/auth/components/RegisterForm.vue';

const hospitalStore = useHospitalStore();

onMounted(async () => {
  await hospitalStore.fetchHospitals();
});
</script>
