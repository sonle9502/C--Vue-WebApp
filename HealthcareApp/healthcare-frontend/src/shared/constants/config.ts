// config.ts
export const API_BASE_URL = 'https://localhost:7239/api';

// Backend API
export const LOGIN_URL = `${API_BASE_URL}/Auth/Login`;
export const REGISTER_URL = `${API_BASE_URL}/Auth/Register`;
export const HOSPITALS_URL = `${API_BASE_URL}/admin/hospitals`;
export const PATIENTS_URL = `${API_BASE_URL}/patients`;

// Frontend route
export const LOGIN_PATH = '/login';
