import { createI18n } from 'vue-i18n'
import en from '../locales/en.json'
import vi from '../locales/vi.json'
import ja from '../locales/ja.json'

const i18n = createI18n({
  legacy: false,
  locale: 'ja',       
  fallbackLocale: 'en',
  messages: { en, vi , ja },
})


export default i18n
