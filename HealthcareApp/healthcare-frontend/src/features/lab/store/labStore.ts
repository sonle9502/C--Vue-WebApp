import { defineStore } from "pinia";
import { ref } from "vue";
import { getlabdata, uploadZipFile } from "../api/labApi";
import { LabResult } from '@/shared/types/lab/lab';

export const useLabStore = defineStore("lab", () => {
  const results = ref<LabResult[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);
  const uploadstatus = ref<number | null>(null);
  
    const fetchResults = async () => {
      loading.value = true;
      error.value = null;
      try {
        const newResults = await getlabdata();
        results.value = newResults;
      } catch (err: any) {
        error.value = err.message || "Lỗi khi tải kết quả xét nghiệm";
      } finally {
        loading.value = false;
      }
    };

  const uploadZip = async (file: File) => {
    loading.value = true;
    error.value = null;
    uploadstatus.value = null;
    
    try {
      const status = await uploadZipFile(file);
      uploadstatus.value = status;

    } catch (err: any) {
      error.value = err.message || "Lỗi khi upload file ZIP";
    } finally {
      loading.value = false;
    }
  };

  return { results, uploadstatus, loading, error, fetchResults, uploadZip };
});
