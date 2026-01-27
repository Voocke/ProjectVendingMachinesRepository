<script setup>
import {ref, computed } from 'vue'
import { RouterLink, RouterView } from 'vue-router'
const currentLang = ref('ru')

// 2. Создаем словарь прямо здесь
const messages = {
  ru: {
    dashboard: 'Дашборд',
    machines: 'ТА (Аппараты)',
    calendar: 'Календарь ТО',
    schedule: 'График работ',
    reports: 'Отчеты',
    login: 'Вход',
    logout: 'Выход',
    switchLang: 'Switch to English' // Текст на кнопке
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

// 3. Вычисляем текущий текст
const text = computed(() => {
  return messages[currentLang.value]
})

// 4. Функция переключения
const toggleLang = () => {
  currentLang.value = currentLang.value === 'ru' ? 'en' : 'ru'
  console.log("Язык изменен на:", currentLang.value)
}
</script>

<template>
  <div class="app-container">
    <nav class="sidebar">
      <div class="logo">Vending System</div>

      <button class="lang-btn" @click="toggleLang">
        {{ text.switchLang }}
      </button>

      <RouterLink to="/dashboard">📊 {{ text.dashboard }}</RouterLink>
      <RouterLink to="/machines">🤖 {{ text.machines }}</RouterLink>
      <RouterLink to="/calendar">📅 {{ text.calendar }}</RouterLink>
      <RouterLink to="/schedule">👷 {{ text.schedule }}</RouterLink>
      <RouterLink to="/reports">📑 {{ text.reports }}</RouterLink>
      <RouterLink to="/login" class="logout">🚪 {{ text.logout }}</RouterLink>
    </nav>

    <main class="content">
      <RouterView />
    </main>
  </div>
</template>

<style>

.lang-btn {
  background: transparent;
  border: 1px solid rgba(255,255,255,0.5);
  color: white;
  padding: 8px;
  margin-bottom: 20px;
  cursor: pointer;
  border-radius: 5px;
}

/* Простой CSS, ты его знаешь */
.app-container {
  display: flex;
  height: 100vh; /* На весь экран */
  font-family: Arial, sans-serif;
}

.page h1 {
  color: #040c13; /* Темно-синий цвет, хорошо видно */
}

.sidebar {
  width: 250px;
  background-color: #2c3e50;
  color: white;
  padding: 20px;
  display: flex;
  flex-direction: column;
}

.logo {
  font-size: 20px;
  font-weight: bold;
  margin-bottom: 30px;
  text-align: center;
}

/* Ссылки меню */
a {
  color: white;
  text-decoration: none;
  padding: 10px;
  margin-bottom: 5px;
  border-radius: 5px;
  transition: 0.3s;
}

a:hover {
  background-color: #42b983;
}

/* Подсветка активной страницы (Router сам добавляет этот класс) */
.router-link-active {
  background-color: #42b983;
  font-weight: bold;
}

.logout {
  margin-top: auto; /* Прижать кнопку выхода к низу */
  background-color: #c0392b;
}

.content {
  flex-grow: 1; /* Занимает всё оставшееся место */
  padding: 20px;
  background-color: #f4f4f4;
  overflow-y: auto; /* Прокрутка, если контента много */
}
</style>