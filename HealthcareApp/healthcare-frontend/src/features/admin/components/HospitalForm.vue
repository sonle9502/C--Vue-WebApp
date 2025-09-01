<template>
  <form @submit.prevent="submitForm">
    <div class="mb-3">
      <label for="name" class="form-label">Tên bệnh viện</label>
      <input id="name" v-model="hospital.name" type="text" class="form-control" required />
    </div>
    <div class="mb-3">
      <label for="address" class="form-label">Địa chỉ</label>
      <input id="address" v-model="hospital.address" type="text" class="form-control" required />
    </div>
    <div class="mb-3">
      <label for="phone" class="form-label">Số điện thoại</label>
      <input id="phone" v-model="hospital.phone" type="text" class="form-control" />
    </div>
    <button type="submit" class="btn btn-success w-100">Thêm bệnh viện</button>

    <p v-if="message" :class="['mt-3', success ? 'text-success' : 'text-danger', 'text-center']">
      {{ message }}
    </p>
  </form>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue';
import type { Hospital } from '@/shared/types/hospitalService';
import { useHospitalStore } from '@/features/admin/store/store';

const hospitalStore = useHospitalStore();

// Reactive object không cần id
const hospital = reactive<Omit<Hospital, 'id'>>({
  name: '',
  address: '',
  phone: ''
});

const message = ref('');
const success = ref(false);

const submitForm = async () => {
  try {
    await hospitalStore.createHospital(hospital); // gửi object reactive
    message.value = 'Thêm bệnh viện thành công!';
    success.value = true;

    // reset form
    hospital.name = '';
    hospital.address = '';
    hospital.phone = '';
  } catch (err: any) {
    console.error(err);
    message.value = 'Có lỗi xảy ra khi thêm bệnh viện.';
    success.value = false;
  }
};
</script>
