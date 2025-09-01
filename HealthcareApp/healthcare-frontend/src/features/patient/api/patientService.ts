import axiosInstance from '@/shared/api/axiosInstance';
import { Patient } from '@/shared/types/patient/patient';
import { PATIENTS_URL } from '@/shared/constants/config';

export const getAllPatients = async (): Promise<Patient[]> => {
  const res = await axiosInstance.get<Patient[]>(PATIENTS_URL); 
  return res.data;
};

export const addPatient = async (patient: Patient): Promise<Patient> => {
  const res = await axiosInstance.post<Patient>(PATIENTS_URL, patient);
  return res.data;
};

export const updatePatient = async (patient: Patient): Promise<Patient> => {
  const res = await axiosInstance.put<Patient>(`${PATIENTS_URL}/${patient.id}`, patient);
  return res.data;
};

export const deletePatient = async (id: number): Promise<void> => {
  await axiosInstance.delete(`${PATIENTS_URL}/${id}`);
};

