import { __awaiter, __generator } from "tslib";
import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home.vue';
import users from '@/store/Modules/Users';
Vue.use(VueRouter);
var routes = [
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
        component: function () { return import(/* webpackChunkName: "about" */ '../views/About.vue'); }
    },
    {
        path: '/Login',
        name: 'Login',
        component: function () { return import(/* webpackChunkName: "about" */ '../views/Login.vue'); }
    },
    {
        path: '/AdminPanel',
        name: 'AdminPanel',
        component: function () { return import(/* webpackChunkName: "about" */ '../views/AdminPanel.vue'); }
    },
    {
        path: '/Products/:categoryId',
        name: 'Products',
        component: function () { return import(/* webpackChunkName: "about" */ '../views/Products.vue'); }
    }
];
var router = new VueRouter({
    routes: routes
});
router.beforeEach(function (to, from, next) { return __awaiter(void 0, void 0, void 0, function () {
    return __generator(this, function (_a) {
        //if (!users.isProfileLoaded) {
        //   await users.getProfile();
        //}
        if (to.path.toLowerCase() == '/adminpanel' && !users.isAdmin) {
            next('/login');
        }
        else
            next();
        return [2 /*return*/];
    });
}); });
export default router;
//# sourceMappingURL=index.js.map