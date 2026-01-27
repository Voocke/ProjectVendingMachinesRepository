import { ref, computed } from 'vue'

// 1. Текущий язык (по умолчанию русский)
// ref делает переменную "живой" — если поменять, сайт обновится
export const currentLang = ref('ru')

// 2. Словарик
export const messages = {
  ru: {
    dashboard: 'Дашборд',
    machines: 'ТА (Аппараты)',
    calendar: 'Календарь ТО',
    schedule: 'График работ',
    reports: 'Отчеты',
    login: 'Вход',
    logout: 'Выход',
    switchLang: 'Switch to English'
  },
  en: {
    dashboard: 'Dashboard',
    machines: 'Vending Machines',
    calendar: 'Maintenance Calendar',
    schedule: 'Work Schedule',
    reports: 'Reports',
    login: 'Login',
    logout: 'Logout',
    switchLang: 'Переключить на Русский'
  }
}
