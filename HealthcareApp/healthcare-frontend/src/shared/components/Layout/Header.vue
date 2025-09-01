<template>
  <header class="d-flex justify-content-between align-items-center p-3 bg-primary text-white">
    <!-- Logo trái -->
    <div class="logo" @click="goHome">
      <img :src="homeIcon" alt="Home" class="icon" />
      HealthCareApp
    </div>

    <!-- Controls phải -->
    <div class="d-flex align-items-center ms-auto">
      <!-- Select hiển thị -->
      <select v-model="selectedView" class="form-select form-select-sm w-auto me-2">
        <option value="default">Thông tin bệnh nhân</option>
        <option value="lab">Kết quả xét nghiệm</option>
      </select>

      <!-- Select ngôn ngữ -->
      <select v-model="selectedLanguage" class="form-select form-select-sm w-auto me-2">
        <option value="ja">日本語</option>
        <option value="en">English</option>
        <option value="vi">Tiếng Việt</option>
      </select>

      <!-- Login / Logout -->
      <button v-if="isLoggedIn" @click="logout" class="btn-custom">Logout</button>
      <button v-else @click="login" class="btn-custom">Login</button>
    </div>
  </header>
</template>


<script setup lang="ts">
import { ref, computed, watch, onMounted  } from "vue";
import { useRouter } from "vue-router";
import { useI18n } from 'vue-i18n';
import homeIcon from '@/assets/home.png';

const router = useRouter();
const hover = ref(false); // khai báo hover
const { locale } = useI18n();
const selectedLanguage = ref('ja');
const selectedView = ref('default');

onMounted(() => {
  selectedView.value = router.currentRoute.value.path === '/lab-results' ? 'lab' : 'default';
});

const isLoggedIn = computed(() => !!localStorage.getItem("token"));

const goHome = () => {
  router.push("/");
};
const login = () => {
  router.push("/login");
};
const logout = () => {
  localStorage.removeItem("token");
  router.push("/login");
};
// Watch hoặc phương thức để xử lý khi đổi ngôn ngữ
watch(selectedLanguage, (newLang) => {
  locale.value = newLang;
});

watch(selectedView, (newView) => {
  console.log("selectedView changed:", newView);
  if (newView === 'lab') {
    router.push('/lab-results');
  } else if (newView === 'default') {
    router.push('/'); // quay về trang chính
  }
});


</script>