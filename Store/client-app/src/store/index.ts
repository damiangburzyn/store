import Vue from 'vue'
import Vuex, { Payload, Store }from 'vuex'
import VuexPersistence from 'vuex-persist'
//import users from "@/store/Modules/Users";
//import antiforgery from "@/store/Modules/Antiforgery";

const vuexLocal = new VuexPersistence({
    storage: window.localStorage
})

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        users: {

        },
        antiforgery: {
            antiforgeryToken :""
        }
      
    },
  mutations: {
  },
  actions: {
  },
  modules: {
    },
   plugins: [vuexLocal.plugin]
})
