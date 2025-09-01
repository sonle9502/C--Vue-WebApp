<template>
  <div class="container mt-5 d-flex justify-content-center align-items-center" style="min-height:80vh">
    <div class="col-md-6">
      <div class="card shadow">
        <div class="card-body">
          <h3 class="card-title text-center mb-4">Thêm bệnh viện mới</h3>
          <HospitalForm />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import HospitalForm from '@/features/admin/components/HospitalForm.vue';
</script>
