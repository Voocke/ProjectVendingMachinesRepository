<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const username = ref('')
const password = ref('')
const errorMessage = ref('')
const isLoading = ref(false)
const router = useRouter()

const handleLogin = async () => {
  // 1. ПРОВЕРКА ЗАПУСКА
  alert("ШАГ 1: Функция запущена. Данные: " + username.value)

  isLoading.value = true
  errorMessage.value = ''
  
  try {
    // ВНИМАНИЕ: Убедись, что порт 5000, 5001 или 7001 (как у твоего C#)
    const API_URL = 'https://localhost:7050/api/auth/login'
    
    // 2. ОТПРАВКА
    // alert("ШАГ 2: Шлю запрос на " + API_URL) 

    const response = await fetch(API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ 
        email: username.value,  // <-- Если C# ждет Login, поменяй на login: username.value
        password: password.value 
      })
    })

    const text = await response.text()
    
    // 3. РЕЗУЛЬТАТ
    alert("ШАГ 3: Пришел ответ!\nСтатус: " + response.status + "\nТекст: " + text)

    if (!response.ok) {
      throw new Error('Сервер ответил ошибкой: ' + response.status)
    }

    // Если всё ок — сохраняем токен вручную
    let data = JSON.parse(text)
    let token = data.token || data.accessToken || data.jwt
    
    if (token) {
        localStorage.setItem('token', token)
        alert("УРА! Токен сохранен. Переходим на Дашборд.")
        // Перезагружаем страницу, чтобы App.vue увидел токен
        window.location.href = '/dashboard' 
    } else {
        alert("ОШИБКА: Сервер ответил ОК, но токена нет.")
    }

  } catch (error) {
    alert("КРИТИЧЕСКАЯ ОШИБКА: " + error)
    errorMessage.value = error.message
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <div class="login-page">
    <div class="login-card">
      <h2>Тест Входа (Прямой)</h2>
      
      <form @submit.prevent="handleLogin">
        <input v-model="username" placeholder="Логин (admin)" style="display:block; width:100%; margin:10px 0; padding:10px;">
        <input v-model="password" type="password" placeholder="Пароль" style="display:block; width:100%; margin:10px 0; padding:10px;">
        
        <button type="submit" style="background: green; color: white; padding: 15px; width: 100%; font-size: 18px;">
          ВОЙТИ (ЖМИ СИЛЬНО)
        </button>
        
        <p style="color:red; margin-top:10px;">{{ errorMessage }}</p>
      </form>
    </div>
  </div>
</template>

<style>
.login-page { display: flex; justify-content: center; height: 100vh; align-items: center; }
.login-card { width: 300px; padding: 20px; border: 1px solid #ccc; }
</style>