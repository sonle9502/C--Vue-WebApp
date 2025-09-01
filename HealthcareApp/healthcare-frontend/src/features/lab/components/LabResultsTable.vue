<template>
  <div class="lab-results-table">
    <table class="table table-striped table-bordered">
      <thead class="table-light">
        <tr>
          <th>ID</th>
          <th>Patient ID</th>
          <th>Test Name</th>
          <th>Result</th>
          <th>Created At</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in results" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.patientId }}</td>
          <td>{{ item.testName }}</td>
          <td>{{ item.result }}</td>
          <td>{{ formatDate(item.createdAt) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { defineProps } from 'vue';
import { LabResult } from '../../../shared/types/lab/lab';

const props = defineProps<{
  results: LabResult[];
}>();

const formatDate = (iso: string) => {
  return new Date(iso).toLocaleString(); // hiển thị theo local timezone
};
</script>