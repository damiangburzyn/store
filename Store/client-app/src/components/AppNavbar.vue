<template>



    <div>
        <!--<b-navbar toggleable="sm" type="light" variant="light">
            <b-navbar-toggle target="nav-text-collapse"></b-navbar-toggle>


            <b-collapse id="nav-text-collapse" is-nav>
                <b-navbar-nav>
                    <b-nav-item><router-link class="nav-link" to="/"> Home </router-link></b-nav-item>

                    <b-nav-item><router-link class="nav-link" to="/about"> About </router-link></b-nav-item>

                    <b-nav-item v-if="!username"><router-link class="nav-link" to="/login"> Logowanie </router-link></b-nav-item>
                    <b-nav-item v-if="!username"><router-link class="nav-link" to="/register"> Rejestracja </router-link></b-nav-item>
                    <b-nav-item v-if="isAdmin"> <router-link class="nav-link" to="/adminpanel"> Panel Administratora </router-link></b-nav-item>
                    <b-nav-item></b-nav-item>
                    <b-nav-item></b-nav-item>
                </b-navbar-nav>


                <b-navbar-nav class="ml-auto">

                    <b-nav-item v-if="username" right> Wyloguj</b-nav-item>
                    <b-nav-item right>{{ username }} </b-nav-item>

                </b-navbar-nav>

            </b-collapse>
        </b-navbar>-->
   
        <v-app-bar color="blue lighten-5"
                   dense
                   light>
            <v-app-bar-nav-icon></v-app-bar-nav-icon>

            <router-link class="nav-link" to="/"><span class=""> Home</span> </router-link>

            <router-link class="nav-link" to="/about"> About </router-link>
            <router-link  v-if="!username" class="nav-link" to="/login"> Logowanie </router-link>
            <router-link  v-if="!username" class="nav-link" to="/register"> Rejestracja </router-link>
            <router-link  v-if="isAdmin" class="nav-link" to="/adminpanel"> Panel Administratora </router-link>



            <v-spacer></v-spacer>

            <span v-if="username" right> Wyloguj</span>
            <span>{{ username }} </span>

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
}
</script>

<style lang="scss">
    .v-toolbar__content > a.nav-link {
        color: grey ;
    }
</style>