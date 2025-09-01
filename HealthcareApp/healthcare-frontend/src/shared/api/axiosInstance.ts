import axios from 'axios';
import { API_BASE_URL, LOGIN_PATH } from "../constants/config";

const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

// Interceptor để tự động thêm token vào header Authorization
axiosInstance.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem('token');
    console.log('Token in interceptor:', token); // kiểm tra token
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    if (error instanceof Error) {
      return Promise.reject(error);
    }
    return Promise.reject(new Error(error?.message || "Unknown error"));
  }
);

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    const originalRequest = error.config;

    // Nếu 401 và request **không phải register/login**
    if (
      (error.response?.status === 401 || error.response?.status === 403) &&
      originalRequest &&
      !originalRequest.url?.includes("/Auth/Login")
    ) {
      localStorage.removeItem("token");
      window.location.href = LOGIN_PATH;
    }
    return Promise.reject(new Error(error?.message || "Unknown error"));
  }
);

export default axiosInstance;
