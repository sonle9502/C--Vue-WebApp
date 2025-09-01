import axiosInstance from '@/shared/api/axiosInstance';
import type { Hospital } from '@/shared/types/hospitalService';
import { HOSPITALS_URL } from '@/shared/constants/config';

export const addHospital = async (hospital: Omit<Hospital, 'id'>) => {
  return await axiosInstance.post(HOSPITALS_URL, hospital);
};

export const getHospitals = async () => {
  return await axiosInstance.get(HOSPITALS_URL);
};
