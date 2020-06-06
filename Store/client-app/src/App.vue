<template>
    <v-app>
        <AppNavbar></AppNavbar>
        <br />
        <div id="app">

            <div class="container-fluid">

                <router-view />
            </div>
        </div>
    </v-app>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import AppNavbar from '@/components/AppNavbar.vue';
    import users from "@/store/Modules/Users";
//import HelloWorld from './components/HelloWorld.vue';

@Component({
  components: {
   AppNavbar,
  },
})
export default class App extends Vue {

 async  created() {

 await  users.getProfile();
    };

}
</script>


