import { defineStore } from 'pinia';
import { ref } from 'vue';
import { Patient } from '@/shared/types/patient/patient';
import * as patientService from '@/features/patient/api/patientService';

export const usePatientStore = defineStore('patient', () => {
  const patients = ref<Patient[]>([]);

  const fetchPatients = async () => {
  const allPatients = await patientService.getAllPatients();
  
  // copy ra 1 mảng mới rồi sort
  const sortedPatients = [...allPatients].sort((a, b) => {
      const dateA = new Date(a.createdAt ?? 0).getTime();
      const dateB = new Date(b.createdAt ?? 0).getTime();
      return dateB - dateA;
    });

    patients.value = sortedPatients;
  };

  const addPatient = async (patient: Patient) => {
    const newPatient = await patientService.addPatient(patient);
    patients.value.push(newPatient);
  };

  const updatePatient = async (patient: Patient) => {
    const updatedPatient = await patientService.updatePatient(patient);
    const index = patients.value.findIndex(p => p.id === updatedPatient.id);
    if (index !== -1) {
      patients.value[index] = updatedPatient;
    }
  };
  const deletePatient = async (id: number) => {
    await patientService.deletePatient(id);
    patients.value = patients.value.filter(p => p.id !== id);
  };

  return { patients, fetchPatients, addPatient, updatePatient , deletePatient};
});
