<template>
    <!--<nav class="navbar navbar-expand-lg navbar-light bg-light flex-column flex-md-row bd-navbar">
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarTogglerDemo01">
            <a class="navbar-brand" href="#">Hidden brand</a>
            <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
                <li class="nav-item active">
                    <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Articles</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link disabled" href="#">Disabled</a>
                </li>

                <li v-if="username" class="nav-item">
                    <router-link class="nav-link" to="/settings">
                        <i class="ion-gear-a"></i>&nbsp;Settings
                    </router-link>
                </li>
                <li v-if="username" class="nav-item">
                    <router-link class="nav-link" :to="'/@' + username">
                        {{ username }}
                    </router-link>
                </li>
                <li v-if="!username" class="nav-item">
                    <router-link class="nav-link" to="/register"> Rejestracja </router-link>
                </li>
                <li v-if="!username" class="nav-item">
                    <router-link class="nav-link" to="/login"> Logowanie </router-link>
                </li>

                <li v-if="isAdmin" class="nav-item">
                    <router-link class="nav-link" to="/adminpanel"> PanelAdministratora </router-link>
                </li>
            </ul>

        </div>
    </nav>-->


    <div>
        <b-navbar toggleable="sm" type="light" variant="light">
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

                    <b-nav-item  v-if="username" right> Wyloguj</b-nav-item>
                    <b-nav-item right>{{ username }} </b-nav-item>
                    
                </b-navbar-nav>

            </b-collapse>
        </b-navbar>
    </div>






</template>

<script>
import { Vue, Component } from 'vue-property-decorator';
import users from '@/store/Modules/Users';

@Component
export default class AppNavbar extends Vue {
    get username() {
        return users.username;
    };
    get isAdmin() {
        return users.isAdmin;
    }
}
</script>
