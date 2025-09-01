<template>
  <div class="mb-3">
    <label :for="id" class="form-label">{{ label }}</label>
    <input
      :id="id"
      :type="type"
      :value="modelValue"
      class="form-control"
      :required="required"
      @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
    />
  </div>
</template>

<script setup lang="ts">

defineProps<{
  label: string;
  type?: string;
  id: string;
  modelValue: string;
  required?: boolean;
}>();

defineEmits<(e: 'update:modelValue', value: string) => void>();
</script>
