<template>
  <form @submit.prevent="handleRegister">
    <BaseInput id="username" label="Username" v-model="username" />
    <BaseInput id="email" label="Email" type="email" v-model="email" />
    <BaseInput id="password" label="Password" type="password" v-model="password" />
    <BaseInput id="confirmPassword" label="Confirm Password" type="password" v-model="confirmPassword" />

    <!-- Chọn hospital bằng select -->
    <div class="mb-3">
      <label for="hospital" class="form-label">Tên bệnh viện</label>
      <select
        id="hospital"
        class="form-select"
        v-model="hospitalId"
        required
      >
        <option
          v-for="h in hospitalsList"
          :key="h.id"
          :value="h.id"
        >
          {{ h.name }}
        </option>
      </select>
    </div>

    <button type="submit" class="btn btn-primary w-100">Register</button>

    <p v-if="error" class="text-danger mt-3 text-center">{{ error }}</p>
  </form>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue';
import { useRouter } from 'vue-router';
import BaseInput from './BaseInput.vue';
import type { Hospital } from '@/shared/types/hospitalService';
import { register } from '../api/authService';

// Nhận props từ cha
const props = defineProps<{ hospitals: Hospital[] }>();

const username = ref('');
const email = ref('');
const password = ref('');
const confirmPassword = ref('');
const hospitalId = ref<number | null>(null);
const error = ref('');
const router = useRouter();

// Computed để chắc chắn luôn reactive
const hospitalsList = computed(() => props.hospitals);

// Watch để set hospitalId mặc định khi dữ liệu hospitals thay đổi
watch(
  hospitalsList,
  (newHospitals) => {
    if (newHospitals.length > 0 && !hospitalId.value) {
      hospitalId.value = newHospitals[0].id;
      console.log('Default hospitalId set to:', hospitalId.value);
    }
  },
  { immediate: true }
);

// Xử lý đăng ký
const handleRegister = async () => {
  if (password.value !== confirmPassword.value) {
    error.value = 'Passwords do not match';
    return;
  }

  if (!hospitalId.value) {
    error.value = 'Vui lòng chọn bệnh viện';
    return;
  }

  try {
    await register(username.value, password.value, 'user', hospitalId.value);

    error.value = '';
    alert('Registration successful! Please login.');
    router.push('/login');
  } catch (err: any) {
    error.value = err.response?.data || 'Registration failed';
  }
};
</script>
