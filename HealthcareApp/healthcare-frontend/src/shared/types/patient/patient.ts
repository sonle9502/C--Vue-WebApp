export interface Patient {
  id?: number;
  name: string;
  age: number;
  gender: string;
  bloodType: string;
  address: string;
  testResults?: string;
  createdAt?: string;
}

export type PatientForm = Omit<Patient, 'id' | 'createdAt' | 'testResults'>;