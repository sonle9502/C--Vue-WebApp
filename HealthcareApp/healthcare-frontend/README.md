# Vue 3 + Vite

This template should help get you started developing with Vue 3 in Vite. The template uses Vue 3 `<script setup>` SFCs, check out the [script setup docs](https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup) to learn more.

Learn more about IDE Support for Vue in the [Vue Docs Scaling up Guide](https://vuejs.org/guide/scaling-up/tooling.html#ide-support).

src/
 ├─ features/                 # theo domain/feature
 │   ├─ patient/
 │   │   ├─ api/              
 │   │   ├─ store/
 │   │   ├─ views/
 │   │   └─ components/
 │   ├─ auth/
 │   │   ├─ api/
 │   │   ├─ store/
 │   │   ├─ views/
 │   │   └─ components/
 │   └─ ... (doctor, appointment,...)
 │
 ├─ shared/                   # dùng chung giữa nhiều feature
 │   ├─ api/
 │   │   └─ axiosInstance.ts  # axios base config, interceptor
 │   ├─ components/           # UI cơ bản (Button, InputField, Modal, Table, ...)
 │   ├─ styles/               # global CSS / Tailwind config / SCSS variables
 │   ├─ types/                # type/interface dùng chung (User, ApiResponse, Pagination...)
 │   ├─ utils/                # hàm helper (dateFormat, currencyFormat, validators...)
 │   └─ constants/            # hằng số (roles, routes, API endpoints)
 │
 ├─ router/                   # Vue Router config
 ├─ store/                    # store root (nếu cần global state)
 ├─ App.vue
 └─ main.ts

Nguyên tắc phân chia
a. api/
    Chứa tất cả logic gọi backend (axios/fetch)
    Mỗi resource có 1 file service riêng (patients, auth, labs, ...)
b. components/
    Component nhỏ, tái sử dụng, không gọi API trực tiếp
    Ví dụ: Button, Input, Card, Modal, Table, FormField
c. views/
    Component lớn, page-level, chứa layout và import các component nhỏ
    Ví dụ: AddPatient.vue, PatientList.vue
d. router/
    Cấu hình các route, kết nối views
e. store/
    Quản lý state (Pinia hoặc Vuex)
    Ví dụ: patientStore.ts quản lý danh sách, thêm, xóa, sửa patient
f. types/
    Chứa interface/enum/type dùng chung
    Ví dụ: Patient, BloodType, TestResult
g. utils/
    Chứa helper, formatter, validator, function tái sử dụng
h. styles/
    CSS/SASS/Tailwind global, variables, theme

Tips cho dự án lớn
    Chia nhỏ component: mỗi component chỉ 1 nhiệm vụ.   
    Sử dụng TypeScript thật nghiêm ngặt: giúp catch lỗi sớm.
    Service layer: tách API ra ngoài, page chỉ gọi service.
    State management: dùng Pinia hoặc Vuex để lưu shared state.
    Types/Enums: tránh any, đảm bảo strict type checking.
    Folder per feature (optional): với dự án cực lớn, có thể gộp views, components, store theo module/feature   :
        src/features/patient/
        ├─ api.ts
        ├─ store.ts
        ├─ components/
        └─ views/
    Vite config tách environment: VITE_API_URL, VITE_ENV, dễ deploy dev/prod.