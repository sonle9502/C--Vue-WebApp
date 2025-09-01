<template>
  <div>
    <button class="btn btn-primary mb-3" @click="goToUpload">
      Upload dữ liệu
    </button>
    <LabResultsTable :results="labStore.results" />
    <div v-if="labStore.error" class="text-danger">{{ labStore.error }}</div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import { useRouter } from "vue-router";
import { useLabStore } from "@/features/lab/store/labStore";
import LabResultsTable from "@/features/lab/components/LabResultsTable.vue";

const router = useRouter();
const labStore = useLabStore();
const goToUpload = () => {
  router.push("/upload"); // đổi sang URL upload của bạn
};
// Khi component mount → gọi fetchResults
onMounted(() => {
  labStore.fetchResults();
});
</script>
