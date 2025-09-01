<template>
  <div class="login-container">
    <div class="login-card">
      <h3 class="card-title">Login</h3>

      <form @submit.prevent="handleLogin">
        <div class="mb-3">
          <label for="username" class="form-label">Username</label>
          <input
            id="username"
            v-model="username"
            type="text"
            class="form-control"
            required
          />
        </div>

        <div class="mb-3">
          <label for="password" class="form-label">Password</label>
          <input
            id="password"
            v-model="password"
            type="password"
            class="form-control"
            required
          />
        </div>

        <button type="submit" class="btn btn-primary">Login</button>

        <p v-if="error" class="text-danger">{{ error }}</p>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';
import { API_BASE_URL } from '@/shared/constants/config';
import axios from 'axios';
import { useRouter } from 'vue-router';

export default {
  setup() {
    const username = ref('');
    const password = ref('');
    const error = ref('');
    const router = useRouter();

    const handleLogin = async () => {
      try {
        const res = await axios.post(`${API_BASE_URL}/Auth/Login`, {
          username: username.value,
          password: password.value
        });
        const token = res.data.token;
        window.localStorage.setItem('token', token);
        error.value = '';
        router.push('/');
      } catch (err: any) {
        error.value = err.response?.data || 'Login failed';
      }
    };

    return { username, password, error, handleLogin };
  }
};
</script>
