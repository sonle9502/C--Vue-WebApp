import axiosInstance from '@/shared/api/axiosInstance';
import type { Hospital } from '@/shared/types/hospitalService';
import { LOGIN_URL, REGISTER_URL, HOSPITALS_URL } from '@/shared/constants/config';

export const login = async (username: string, password: string) => {
  const res = await axiosInstance.post(LOGIN_URL, { username, password });
  return res.data; 
};

// Register
export const register = async (
  username: string,
  password: string,
  role: string,
  hospitalId?: number
) => {
  const res = await axiosInstance.post(REGISTER_URL, {
    username,
    password,
    role,
    hospitalId,
  });
  return res.data;
};

export const logout = () => {
  window.localStorage.removeItem('token');
  return Promise.resolve();
};

// Lấy danh sách hospital (dùng cho form đăng ký)
export const getHospitals = async () => {
  const res = await axiosInstance.get<Hospital[]>(HOSPITALS_URL);
  return res.data;
};