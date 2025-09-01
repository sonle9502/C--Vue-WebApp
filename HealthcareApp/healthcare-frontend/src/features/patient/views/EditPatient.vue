<template>
  <div class="edit-patient">
    <h2>{{ t('editPatient') }}</h2>
    <form @submit.prevent="submitForm">
      <div class="form-row">
        <label for="name">Tên:</label>
        <input id="name" v-model="form.name" required />
      </div>
      <div class="form-row">
        <label for="age">Tuổi:</label>
        <input id="age" type="number" v-model.number="form.age" required />
      </div>
      <div class="form-row">
        <label for="address">Địa chỉ:</label>
        <input id="address" v-model="form.address" />
      </div>
      <div class="form-row">
        <label for="gender">Giới tính:</label>
        <select id="gender" v-model="form.gender">
          <option value="Male">Nam</option>
          <option value="Female">Nữ</option>
        </select>
      </div>
      <div class="form-row">
        <label for="bloodType">Blood Type:</label>
        <input id="bloodType" v-model="form.bloodType" />
      </div>
      <button type="submit">Cập nhật</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { usePatientStore } from '@/features/patient/store/patientStore';
import type { Patient } from '@/shared/types/patient/patient';
import { useI18n } from 'vue-i18n'

export default defineComponent({
  setup() {
    const route = useRoute();
    const router = useRouter();
    const store = usePatientStore();
    const id = Number(route.params.id);
    const { t } = useI18n();

    const form = reactive<Patient>({
      id,
      name: '',
      age: 0,
      address: '',
      gender: 'Male',
      bloodType: ''
    });

    onMounted(() => {
      const patient = store.patients.find(p => p.id === id);
      if (patient) {
        Object.assign(form, patient);
      }
    });

    const submitForm = async () => {
      await store.updatePatient(form);
      alert('Đã cập nhật bệnh nhân!');
      router.push('/');
    };

    return { form, submitForm, t };
  },
});
</script>