import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import axios from 'axios';
import Vuetify from "vuetify";
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import "vuetify/dist/vuetify.min.css";
import '@mdi/font/css/materialdesignicons.css';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
// Install BootstrapVue
Vue.use(BootstrapVue);
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin);
Vue.use(Vuetify);
Vue.config.productionTip = false;
Vue.prototype.$axios = axios;
new Vue({
    router: router,
    store: store,
    render: function (h) { return h(App); },
    vuetify: new Vuetify()
}).$mount('#app');
//# sourceMappingURL=main.js.map