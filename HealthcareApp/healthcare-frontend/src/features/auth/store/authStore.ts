import { defineStore } from 'pinia';
import { ref } from 'vue';
import * as authApi from '@/features/auth/api/authService';

interface User {
  username: string;
  role: string;
}

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string | null>(window.localStorage.getItem('token'));
  const currentUser = ref<User | null>(null);

  const login = async (username: string, password: string) => {
    const { token: jwt } = await authApi.login(username, password);
    token.value = jwt;
    window.localStorage.setItem('token', jwt);
  };

  const register = async (username: string, password: string, role: string) => {
    await authApi.register(username, password, role);
  };

  const logout = () => {
    token.value = null;
    currentUser.value = null;
    window.localStorage.removeItem('token');
  };

  return { token, currentUser, login, register, logout };
});
