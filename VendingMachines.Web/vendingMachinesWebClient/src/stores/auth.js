import { ref } from 'vue'

// 1. Глобальная переменная (Состояние)
// Мы создаем её ВНЕ функции, поэтому она одна на всё приложение.
const token = ref(localStorage.getItem('token') || null)



// 2. Функция входа
const login = async (email, password) => {
    console.log("--- НАЧАЛО ВХОДА ---")
  console.log("1. Логин:", email)
  console.log("2. Пароль:", password)
  try {
    const API_URL = 'https://localhost:7050/api/auth/login' // <-- Проверь порт!
    alert("Отправляю запрос на: " + API_URL + "\nЛогин: " + username)
    const requestBody = { 
      // Попробуй оба варианта (раскомментируй нужный):
      // Вариант 1 (Чаще всего):
      email: username, 
      // Вариант 2 emailЕсли сервер ждет email):
      // email: username, 
      
      password: password 
    }

    console.log("3. Отправляю JSON:", JSON.stringify(requestBody))
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ 
        email: email, // Или email/username, как в твоем C#
        password: password 
      })
    })

    console.log("4. Ответ сервера (Статус):", response.status) // 200, 400, 401?

    // Читаем ответ как текст, чтобы увидеть, что там, даже если это ошибка
    const responseText = await response.text()
    console.log("5. Ответ сервера (Тело):", responseText)
alert("СТАТУС: " + response.status + "\nОТВЕТ: " + responseText)
    if (!response.ok) throw new Error('Ошибка входа')

    // Получаем токен (если сервер шлет JSON)
    const data = await response.json()
    const newToken = data.token 
    // ИЛИ если сервер шлет текст: const newToken = await response.text()

    if (!newToken) throw new Error('Нет токена')

    // Сохраняем
    token.value = newToken
    localStorage.setItem('token', newToken)
    
    return true
  } catch (error) {
    console.error(error)
    throw error
  }
}

// 3. Функция выхода
const logout = () => {
  token.value = null
  localStorage.removeItem('token')
}

// Экспортируем всё это, чтобы использовать в других файлах
export const authState = {
  token,
  login,
  logout
}