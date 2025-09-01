export interface LabResult {
  id: number;
  patientId: number;
  result: string;
  testName: string;
  createdAt: string;
  // ... nếu còn các field khác
}