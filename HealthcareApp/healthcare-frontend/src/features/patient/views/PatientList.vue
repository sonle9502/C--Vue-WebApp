<template>
  <div>
    <div class="d-flex align-items-center mb-3 gap-2" style="flex-wrap: nowrap;">
      <!-- Search box -->
      <input
        v-model="searchQuery"
        type="text"
        :placeholder="t('searchPatient')"
        class="form-control"
        style="width: 300px;"
      />

      <!-- Add patient button -->
      <router-link to="/add" class="ms-auto">
        <button class="btn btn-primary">{{ t('addPatient') }}</button>
      </router-link>
    </div>

    <!-- Title -->
    <h2 class="btn-listpatients mb-4">{{ t('listpatients') }}</h2>

    <!-- Empty state -->
    <div v-if="filteredPatients.length === 0">
      Không tìm thấy bệnh nhân nào
    </div>

    <!-- Patient table -->
    <table v-else class="table table-striped table-bordered">
      <thead class="table-light">
        <tr>
          <th>ID</th>
          <th>{{ t('name') }}</th>
          <th>{{ t('age') }}</th>
          <th>{{ t('address') }}</th>
          <th>{{ t('BloodType') }}</th>
          <th>{{ t('actions') }}</th>
        </tr>
      </thead>
      <tbody>
        <PatientRow
          v-for="(p, index) in filteredPatients"
          :key="p.id"
          :patient="p"
          :no="index + 1"
          @edit="handleEdit"
          @delete="handleDelete"
        />
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, computed, ref } from 'vue'
import { useRouter } from 'vue-router'
import { usePatientStore } from '@/features/patient/store/patientStore'
import PatientRow from '@/features/patient/components/PatientRow.vue'
import { Patient } from '@/shared/types/patient/patient'
import { useI18n } from 'vue-i18n'

export default defineComponent({
  components: { PatientRow },
  setup() {
    const store = usePatientStore()
    const router = useRouter()
    const { t } = useI18n()

    const searchQuery = ref('')

    onMounted(() => {
      store.fetchPatients()
    })

    const patients = computed(() => store.patients)

    const filteredPatients = computed(() => {
      const q = searchQuery.value.toLowerCase()
      return patients.value.filter(
        (p: Patient) =>
          p.name.toLowerCase().includes(q) ||
          p.address.toLowerCase().includes(q)
      )
    })

    const handleEdit = (patient: Patient) => {
      router.push(`/edit/${patient.id}`)
    }

    const handleDelete = (id?: number) => {
      if (id !== undefined) {
        store.deletePatient(id)
      }
    }

    return { patients, filteredPatients, searchQuery, handleEdit, handleDelete, t }
  },
})
</script>
