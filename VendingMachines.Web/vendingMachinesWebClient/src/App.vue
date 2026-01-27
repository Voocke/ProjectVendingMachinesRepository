<script setup>
import { ref, computed } from 'vue'
import { RouterLink, RouterView, useRouter } from 'vue-router'
// Импорт нашего простого store
import { authState } from './stores/auth.js'

// Создаем роутер
const router = useRouter()
const currentLang = ref('ru')

const messages = {
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

// Тексты
const text = computed(() => messages[currentLang.value])

// ПРОВЕРКА АВТОРИЗАЦИИ
// Создаем вычисляемую переменную. Если токен есть — вернет true.
const isAuth = computed(() => {
  return !!authState.token.value // Двойное отрицание превращает строку/null в true/false
})

const toggleLang = () => {
  currentLang.value = currentLang.value === 'ru' ? 'en' : 'ru'
}

const handleLogout = () => {
  authState.logout()
  router.push('/login')
}
</script>

<template>
  <div class="app-container">
    <nav class="sidebar">
      <div class="logo">Vending System</div>

      <button class="lang-btn" @click="toggleLang">
        {{ text.switchLang }}
      </button>

      <div v-show="isAuth" class="menu-items">
        <RouterLink to="/dashboard">📊 {{ text.dashboard }}</RouterLink>
        <RouterLink to="/machines">🤖 {{ text.machines }}</RouterLink>
        <RouterLink to="/calendar">📅 {{ text.calendar }}</RouterLink>
        <RouterLink to="/schedule">👷 {{ text.schedule }}</RouterLink>
        <RouterLink to="/reports">📑 {{ text.reports }}</RouterLink>
        
        <button @click="handleLogout" class="logout-btn">
          🚪 {{ text.logout }}
        </button>
      </div>
      
      <div v-show="!isAuth" class="guest-msg">
        <p>Система управления</p>
      </div>
    </nav>

    <main class="content">
      <RouterView />
    </main>
  </div>
</template>

<style>
/* Твои стили без изменений */
.lang-btn {
  background: transparent;
  border: 1px solid rgba(255,255,255,0.5);
  color: white;
  padding: 8px;
  margin-bottom: 20px;
  cursor: pointer;
  border-radius: 5px;
  width: 100%;
}
.app-container { display: flex; height: 100vh; font-family: Arial, sans-serif; }
.sidebar { width: 250px; background-color: #2c3e50; color: white; padding: 20px; display: flex; flex-direction: column; }
.logo { font-size: 20px; font-weight: bold; margin-bottom: 30px; text-align: center; }
a { display: block; color: white; text-decoration: none; padding: 10px; margin-bottom: 5px; border-radius: 5px; transition: 0.3s; }
a:hover { background-color: #42b983; }
.router-link-active { background-color: #42b983; font-weight: bold; }
.logout-btn { width: 100%; text-align: left; background-color: #c0392b; color: white; border: none; padding: 10px; margin-top: 20px; border-radius: 5px; cursor: pointer; font-size: 16px; font-family: Arial, sans-serif; }
.logout-btn:hover { background-color: #a93226; }
.content {
  flex-grow: 1;
  padding: 20px;
  background-color: #f4f4f4;
  overflow-y: auto;

  /* --- ДОБАВЬ ВОТ ЭТУ СТРОКУ --- */
  color: #2c3e50; /* Темно-синий (почти черный) цвет для всего текста */
}
.guest-msg { text-align: center; color: #bdc3c7; font-size: 14px; margin-top: 50px; }
</style>