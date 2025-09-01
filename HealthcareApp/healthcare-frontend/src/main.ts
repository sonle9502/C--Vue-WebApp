import { createApp } from 'vue';
import App from './App.vue';
import { createPinia } from 'pinia';
import router from './router';
import i18n from './shared/locales/i18n';

// Import SCSS thay cho CSS của bootstrap
import './shared/styles/main.scss';

const app = createApp(App);
const pinia = createPinia();

app.use(i18n);
app.use(pinia);
app.use(router);

app.mount('#app');
