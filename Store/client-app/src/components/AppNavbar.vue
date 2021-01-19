<template>
    <div>
        <v-app-bar color="grey lighten-1"
                   dense
                   light>
            <!--<v-app-bar-nav-icon></v-app-bar-nav-icon>-->

            <v-btn-toggle tile
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
            <v-btn-toggle tile
                          color="black"
                          group>


                <v-btn v-if="!username" to="/login">
                    Logowanie
                </v-btn>
                <v-btn v-if="!username" to="/register">
                    Rejestracja
                </v-btn>
                <v-btn v-if="isAdmin" to="/adminpanel">
                    Panel Administratora
                </v-btn>

                <v-btn v-if="username" v-on:click="logout">
                    Wyloguj: {{username}}
                </v-btn>
            </v-btn-toggle>

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
                        {{cartCount}} <v-icon>mdi-cart</v-icon>
                    </v-btn>
                </template>

                <v-list>
                    <v-list-item v-if="cartCount>0" @click="goToSummary()">
                        <v-list-item-title>Podsumowanie</v-list-item-title>
                    </v-list-item>
                    <v-list-item v-else>
                        <v-list-item-title>Twój koszyk jest pusty</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </v-app-bar>
    </div>






</template>

<script>
import { Vue, Component } from 'vue-property-decorator';
import users from '@/store/Modules/Users';
import cart from '@/store/Modules/Cart';


@Component
export default class AppNavbar extends Vue {

    get cartCount() {
        return cart.cartCount || 0 ;
    }

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
    goToSummary() {
        this.$router.push('/Summary')
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