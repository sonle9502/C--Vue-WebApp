<template>
  <form @submit.prevent="handleUpload">
    <input type="file" accept=".zip" @change="onFileChange" />
    <button type="submit" :disabled="!selectedFile || loading">
      {{ loading ? 'Đang upload...' : 'Upload ZIP' }}
    </button>
  </form>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useLabStore } from "@/features/lab/store/labStore";

const labStore = useLabStore();
const { loading } = labStore;

const selectedFile = ref<File | null>(null);

const onFileChange = (e: Event) => {
  const target = e.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    selectedFile.value = target.files[0];
  }
};

const handleUpload = async () => {
  if (!selectedFile.value) return;
  await labStore.uploadZip(selectedFile.value);
};
</script>