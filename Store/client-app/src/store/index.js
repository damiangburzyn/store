import Vue from 'vue';
import Vuex from 'vuex';
import VuexPersistence from 'vuex-persist';
var vuexLocal = new VuexPersistence({
    storage: window.localStorage
});
Vue.use(Vuex);
export default new Vuex.Store({
    state: {},
    mutations: {},
    actions: {},
    modules: {},
    plugins: [vuexLocal.plugin]
});
//# sourceMappingURL=index.js.map