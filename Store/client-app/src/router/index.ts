import Vue from 'vue'
import VueRouter, { RouteConfig, NavigationGuard } from 'vue-router'
import Home from '../views/Home.vue'
import users from '@/store/Modules/Users';

Vue.use(VueRouter)

  const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },

  {
      path: '/Login',
      name: 'Login',
      component: () => import(/* webpackChunkName: "about" */ '../views/Login.vue')

  },

  {
    path: '/AdminPanel',
    name: 'AdminPanel',
    component: () => import(/* webpackChunkName: "about" */ '../views/AdminPanel.vue')
  }

]

const router = new VueRouter({
    routes
});



 router.beforeEach( async (to, from, next) =>  {

    //if (!users.isProfileLoaded) {
     //   await users.getProfile();
    //}


    if (to.path.toLowerCase() == '/adminpanel' && !users.isAdmin) {
        next('/login')
    }
    else next();
})

export default router
