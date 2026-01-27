import { createRouter, createWebHistory } from 'vue-router'

import LoginView from '../views/LoginView.vue'
import DashboardView from '../views/DashboardView.vue'
import MachinesView from '../views/MachinesView.vue'
import CalendarView from '../views/CalendarView.vue'
import ScheduleView from '../views/ScheduleView.vue'
import ReportsView from '../views/ReportsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/login'
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: DashboardView,
      meta: { requiresAuth: true } 
    },
    {
      path: '/machines',
      name: 'machines',
      component: MachinesView,
      meta: { requiresAuth: true } 
    },
    {
      path: '/calendar',
      name: 'calendar',
      component: CalendarView,
      meta: { requiresAuth: true }
    },
    {
      path: '/schedule',
      name: 'schedule',
      component: ScheduleView,
      meta: { requiresAuth: true }
    },
    {
      path: '/reports',
      name: 'reports',
      component: ReportsView,
      meta: { requiresAuth: true }
    }
  ],
})

router.beforeEach((to, from, next) => {
  // Проверяем, есть ли токен в кармане
  const isAuthenticated = localStorage.getItem('token')

  // Если страница требует авторизации (meta.requiresAuth), а токена нет
  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login') // Пшел вон на логин
  } else {
    next() // Проходи
  }
})

export default router
