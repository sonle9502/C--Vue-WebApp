<template>
  <div class="add-patient">
    <h2>{{ t('addPatient') }}</h2>
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
      <button type="submit">Thêm Patient</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from 'vue';
import { usePatientStore } from '@/features/patient/store/patientStore';
import type { PatientForm } from '@/shared/types/patient/patient';
import { useI18n } from 'vue-i18n'

export default defineComponent({
  setup() {
    const patientStore = usePatientStore();
    const { t } = useI18n()  // t là hàm dịch

    const form = reactive<PatientForm>({
      name: '',
      age: 0,
      address: '',
      gender: 'Male',
      bloodType: '',
    });

    const submitForm = async () => {
      await patientStore.addPatient(form);
      // reset form
      form.name = '';
      form.age = 0;
      form.address = '';
      form.gender = 'Male';
      alert('Đã thêm Patient!');
    };

    return { form, submitForm, t };
  },
});
</script>