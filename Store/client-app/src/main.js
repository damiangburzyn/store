import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import axios from 'axios';
import Vuetify from "vuetify";
import "vuetify/dist/vuetify.min.css";
import '@mdi/font/css/materialdesignicons.css';
Vue.use(Vuetify);
Vue.config.productionTip = false;
Vue.prototype.$axios = axios;
new Vue({
    router,
    store,
    render: h => h(App),
    vuetify: new Vuetify()
}).$mount('#app');
//# sourceMappingURL=main.js.map