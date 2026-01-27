<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div class="app-container">
    <nav class="sidebar">
      <div class="logo">Vending System</div>
      
      <RouterLink to="/dashboard">📊 Дашборд</RouterLink>
      <RouterLink to="/machines">🤖 ТА (Аппараты)</RouterLink>
      <RouterLink to="/calendar">📅 Календарь ТО</RouterLink>
      <RouterLink to="/schedule">👷 График работ</RouterLink>
      <RouterLink to="/reports">📑 Отчеты</RouterLink>
      <RouterLink to="/login" class="logout">🚪 Выход</RouterLink>
    </nav>

    <main class="content">
      <RouterView />
    </main>
  </div>
</template>

<style scoped>
/* Простой CSS, ты его знаешь */
.app-container {
  display: flex;
  height: 100vh; /* На весь экран */
  font-family: Arial, sans-serif;
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