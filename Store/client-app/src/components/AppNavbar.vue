<template>



    <div>


      



        <v-app-bar color="grey lighten-1"
                   dense
                   light>
            <!--<v-app-bar-nav-icon></v-app-bar-nav-icon>-->

            <v-btn-toggle 
                          tile
                          color="black"
                          group>
                <v-btn to="/">
                    Home
                </v-btn>

                <v-btn to="/about" value="right">
                    About
                </v-btn>

            </v-btn-toggle>
            <!--<v-divider vertical></v-divider>-->
            <v-spacer></v-spacer>
            <v-btn-toggle 
                          tile
                          color="black"
                          group>


                <v-btn v-if="!username"  to="/login">
                    Logowanie
                </v-btn>
                <v-btn v-if="!username" to="/register">
                    Rejestracja
                </v-btn>
                <v-btn v-if="isAdmin" to="/adminpanel">
                    Panel Administratora
                </v-btn>
                
                <v-btn v-if="username"  v-on:click="logout">
                  Wyloguj: {{username}}
                </v-btn>
            </v-btn-toggle>




            <!--<v-tabs show-arrows>
        <v-tabs-slider color="teal lighten-3"></v-tabs-slider>

        <v-tab>
            <router-link class="nav-link" to="/"><span class=""> Home</span> </router-link>
        </v-tab>
        <v-tab>
            <router-link class="nav-link" to="/about"> About </router-link>
        </v-tab>
        <v-tab>
            <router-link v-if="!username" class="nav-link" to="/login"> Logowanie </router-link>
        </v-tab>
        <v-tab>
            <router-link v-if="!username" class="nav-link" to="/register"> Rejestracja </router-link>
        </v-tab>
        <v-tab>
            <router-link v-if="isAdmin" class="nav-link" to="/adminpanel"> Panel Administratora </router-link>
        </v-tab>
    </v-tabs>-->







    

           

            <v-btn icon>
                <v-icon>mdi-heart</v-icon>
            </v-btn>

            <v-btn icon>
                <v-icon>mdi-magnify</v-icon>
            </v-btn>

            <v-menu left
                    bottom>
                <template v-slot:activator="{ on }">
                    <v-btn icon v-on="on">
                        <v-icon>mdi-dots-vertical</v-icon>
                    </v-btn>
                </template>

                <v-list>
                    <v-list-item v-for="n in 5"
                                 :key="n"
                                 @click="() => {}">
                        <v-list-item-title>Option {{ n }}</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </v-app-bar>
    </div>






</template>

<script>
import { Vue, Component } from 'vue-property-decorator';
import users from '@/store/Modules/Users';

@Component
export default class AppNavbar extends Vue {
    get username() {
        return users.username;
    }
    get isAdmin() {
        return users.isAdmin;
    }
    logout() {
        users.logOut();
        this.$router.push('/');
    }
}
</script>

<style lang="scss">

    .v-tab > a.nav-link {
        color: grey;
        text-decoration: none;
    }

    .v-btn {
        text-transform: none !important;    
    }

    .v-btn-toggle--group > .v-btn.v-btn {
        margin: 0px !important;
    }
</style>