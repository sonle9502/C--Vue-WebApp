import { createRouter, createWebHistory } from 'vue-router';
import LayoutFull from "../shared/components/Layout/LayoutFull.vue";
import NoLayout from "../shared/components/Layout/NoLayout.vue";
import LayoutNoFooter from "../shared/components/Layout/LayoutNoFooter.vue";

import PatientList from "../features/patient/views/PatientList.vue";
import AddPatient from "../features/patient/views/AddPatient.vue";
import EditPatient from "../features/patient/views/EditPatient.vue";
import Login from "../features/auth/views/Login.vue";
import Register from "../features/auth/views/Register.vue";
import Admin from "../features/admin/views/AddHospital.vue";
import Hospitals from "../features/admin/views/hospitals.vue";
import LabResults  from "../features/lab/views/LabResultsPage.vue";
import UploadPage  from "../features/lab/views/UploadPage.vue";

const routes = [
  // Routes không có header
  {
    path: '/',
    component: LayoutFull,
    children: [
      { path: '', component: PatientList },
    ]
  },
  {
    path: '/login',
    component: NoLayout,
    children: [
      { path: '', component: Login },
    ]
  },
  {
    path: '/register',
    component: NoLayout,
    children: [
      { path: '', component: Register },
    ]
  },

  // Routes có header
  {
    path: '/add',
    component: LayoutFull,
    children: [
      { path: '', component: AddPatient },
    ]
  },
  {
    path: '/edit/:id',
    component: LayoutNoFooter,
    children: [
      { path: '', component: EditPatient },
    ]
  },
  {
    path: '/admin',
    component: LayoutFull,
    children: [
      { path: '', component: Admin },
      { path: 'hospitals', component: Hospitals },
    ]
  },
  {
    path: '/lab-results',
    component: LayoutNoFooter,
    children: [
      { path: '', component: LabResults  },
    ]
  },
  {
    path: '/upload',
    component: LayoutNoFooter,
    children: [
      { path: '', component: UploadPage  },
    ]
  },
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
