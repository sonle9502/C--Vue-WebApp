// api/labResultsApi.ts
import axiosInstance from '@/shared/api/axiosInstance';

export const getlabdata = async () => {
  const response = await axiosInstance.get('/Lab/results'); // dùng GET
  console.log('Fetched lab results:', response.data);
  return response.data;
};

export const uploadZipFile = async (file: File): Promise<number> => {
  const formData = new FormData();
  formData.append("file", file);

  const response = await axiosInstance.post('/Lab/upload-zip', formData, {
    headers: { "Content-Type": "multipart/form-data" }
  });

  return response.status;
};