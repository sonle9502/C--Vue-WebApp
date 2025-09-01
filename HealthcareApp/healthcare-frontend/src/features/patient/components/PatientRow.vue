<template>
  <tr>
    <td>{{ no }}</td>
    <td>{{ patient.name }}</td>
    <td>{{ patient.age }}</td>
    <td>{{ patient.address }}</td>
    <td>{{ patient.bloodType }}</td>
    <td>
      <button class="btn btn-sm btn-primary me-2" @click="$emit('edit', patient)">
        {{ t('edit') }}
      </button>
      <button class="btn btn-sm btn-danger" @click="$emit('delete', patient.id)">
        {{ t('delete') }}
      </button>
    </td>
  </tr>
</template>

<script setup lang="ts">
import { defineProps } from 'vue'
import { Patient } from '@/shared/types/patient/patient'
import { useI18n } from 'vue-i18n'

defineProps<{ 
  patient: Patient
  no: number
}>()
const { t } = useI18n()
</script>
