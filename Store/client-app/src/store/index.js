import Vue from 'vue';
import Vuex from 'vuex';
import VuexPersistence from 'vuex-persist';
//import users from "@/store/Modules/Users";
//import antiforgery from "@/store/Modules/Antiforgery";
var vuexLocal = new VuexPersistence({
    storage: window.localStorage
});
Vue.use(Vuex);
export default new Vuex.Store({
    state: {
        users: {},
        antiforgery: {
            antiforgeryToken: ""
        }
    },
    mutations: {},
    actions: {},
    modules: {},
    plugins: [vuexLocal.plugin]
});
//# sourceMappingURL=index.js.map