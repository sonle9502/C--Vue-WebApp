import { defineStore } from 'pinia';
import { ref } from 'vue';
import type { Hospital } from '@/shared/types/hospitalService';
import * as hospitalApi from '../api/api';

export const useHospitalStore = defineStore('hospital', () => {
  const hospitals = ref<Hospital[]>([]);
  const loading = ref(false);
  const error = ref('');

  const fetchHospitals = async () => {
    loading.value = true;
    try {
      const res = await hospitalApi.getHospitals();
      hospitals.value = res.data;
    } catch (err: any) {
      console.error('Lỗi fetchHospitals:', err);
      error.value = 'Không thể tải danh sách bệnh viện.';
    } finally {
      loading.value = false;
    }
  };

  const createHospital = async (hospital: Omit<Hospital, 'id'>) => {
    try {
      await hospitalApi.addHospital(hospital); // backend tự sinh id
      await fetchHospitals(); // refresh danh sách sau khi thêm
      return true;
    } catch (err: any) {
      console.error('Lỗi createHospital:', err.response || err);
      throw new Error('Thêm bệnh viện thất bại');
    }
  };

  return { hospitals, loading, error, fetchHospitals, createHospital };
});
