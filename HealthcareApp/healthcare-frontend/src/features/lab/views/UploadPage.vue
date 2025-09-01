<template>
  <div>
    <LabUploadForm />
  </div>
</template>

<script setup lang="ts">
import { watch } from "vue";
import { useRouter } from "vue-router";
import { useLabStore } from "@/features/lab/store/labStore";
import LabUploadForm from "@/features/lab/components/LabUploadForm.vue";

const labStore = useLabStore();
const router = useRouter();

// Watch uploadstatus → khi = 200 thì show dialog
watch(
  () => labStore.uploadstatus,
  (newStatus) => {
    if (newStatus === 200) {
      const confirmed = window.confirm("Upload thành công! Nhấn OK để xem kết quả.");
      if (confirmed) {
        router.push("/lab-results");
      }
    }
  }
);
</script>
