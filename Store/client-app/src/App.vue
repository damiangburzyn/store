<template>
    <div id="app">
        <v-app>

            <AppNavbar></AppNavbar>

            <v-banner class="main-banner">

                <v-row no-gutters>
                    <v-col cols="12"
                           sm="4">


                    </v-col>
                    <v-col cols="12"
                           sm="4">
                        <v-container bg fill-height grid-list-md text-xs-center>
                            <v-layout row wrap align-center>
                                <v-flex>
                                    Hello I am center to vertically using "align-center".
                                </v-flex>
                            </v-layout>
                        </v-container>



                    </v-col>
                    <v-col class="banner-image-container" cols="12"
                           sm="4">


                        <img class="banner-image" src="../src/assets/watch.webp" alt="">
                    </v-col>
                </v-row>
            </v-banner>


            <!--<img src="../src/assets/watch.webp" alt="" class=" heartbeat">-->
            <!--<v-img src="../src/assets/watch.webp" aspect-ratio="1.7" contain></v-img>-->


            <br />

            <v-container>

                <div class="container-fluid">

                    <router-view />
                </div>
            </v-container>


        </v-app>
    </div>
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

        async  beforeCreate() {
            await users.getProfile();
        }

    }
</script>


<style lang="scss">

    .main-banner > * {
        background-color: #f0f0f2;
        text-align: center;
    }

    .v-application--is-ltr .v-banner__wrapper {
        padding: 0px !important;
    }

    .banner-image {
        max-height: 300px;
    }

    .banner-image-container {
        line-height: 0px;
    }

    .banner-text-container {
        margin-top: 25%;
    }
</style>