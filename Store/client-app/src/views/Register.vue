<template>
    <div class="auth-page">

        <v-dialog scrollable v-model="showRegisterDialog"
                  persistent
                  width="500">


        </v-dialog>

        <v-row no-gutters>
            <v-col cols="12"
                   sm="4">
            </v-col>
            <v-col cols="12"
                   sm="4">
                <v-form ref="form"
                        v-model="valid"
                        lazy-validation>
                    <v-card>
                        <v-card-text>

                            <v-text-field :type="'text'"
                                          v-model="registerModel.name"
                                          outlined
                                          @keyup.enter="registerUser()"
                                          placeholder="Imię"
                                          :rules="nameRules"></v-text-field>

                            <v-text-field :type="'text'"
                                          v-model="registerModel.lastname"
                                          outlined
                                          @keyup.enter="registerUser()"
                                          placeholder="Nazwisko"
                                          :rules="lastnameRules"></v-text-field>

                            <v-text-field v-model="registerModel.email" v-on:keyup.enter="login"
                                          :rules="emailRules"
                                          outlined
                                          @keyup.enter="registerUser()"
                                          placeholder="Email"></v-text-field>

                            <v-text-field :type="'password'"
                                          v-model="registerModel.password"
                                          outlined
                                          @keyup.enter="registerUser()"
                                          placeholder="Hasło"
                                          :rules="passwordRules"></v-text-field>

                            <v-text-field :type="'password'"
                                          v-model="registerModel.repeatPassword"
                                          outlined
                                          @keyup.enter="registerUser()"
                                          placeholder="Powtórz hasło"
                                          :rules="repeatPasswordRules"></v-text-field>

                            <v-text-field :type="'text'"
                                          v-model="registerModel.phone"
                                          outlined
                                          @keyup.enter="registerModel.registerUser()"
                                          placeholder="Numer telefonu"
                                          :rules="phoneRules"></v-text-field>

                            <v-checkbox v-model="registerModel.acceptTermsOfUse"
                                        :label="`Warunki korzystania z serwisu`"
                                        :rules="termsRules"></v-checkbox>

                            <span v-if="typeof(errorMessage)!= 'undefined'&& errorMessage !== '' " class="alert text-danger" role="alert">
                                {{errorMessage}}
                            </span>
                        </v-card-text>

                        <v-card-actions>
                            <v-layout row>
                                <v-flex justify-center>
                                    <v-btn color="cyan lighten-4"
                                           primary
                                           @click="registerUser()">
                                        Zarejestruj
                                    </v-btn>
                                </v-flex>
                            </v-layout>
                        </v-card-actions>
                    </v-card>
                </v-form>
            </v-col>
            <v-col cols="12"
                   sm="4">
            </v-col>
        </v-row>
    </div>

</template>
<script lang="ts">
    import { Vue, Component } from "vue-property-decorator";

    //import UserLogin from "@/store/models";
    import { registerUser as registerFunc } from "@/store/api";
    import antiforgery from "@/store/Modules/Antiforgery";
    import { RegisterModel } from "@/store/modelsData"


    @Component
    export default class Register extends Vue {
        registerSuccess = false;
        showRegisterDialog = false;
        valid = true;
        runValidate = false;
        registerModel = new RegisterModel();
        errorMessage = "";
        emailRules: Array<Function> = [
            (v: string) => !!v || 'E-mail jest wymagany',
            (v: string) => /.+@.+\..+/.test(v) || 'Wpisz poprawny adres email',
        ];

        passwordRules: Array<Function> = [
            (v: string) => !!v || 'Hasło jest wymagane',
            (v: string) => /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/.test(v) || 'Hasło musi się składać z conajmniej 8 znaków. <br /> Wielkich i małych liter cyfr i znaku specjalnego',


        ];
        repeatPasswordRules: Array<Function> = [
            (v: string) => !!v || 'Hasło jest wymagane',
            (v: string) => this.registerModel.password === v || 'Hasło i powtórz hasło muszą się zgadzać',
        ];

        termsRules: Array<Function> = [
            (v: string) => !!v || 'Pole wymagane'
        ];

        phoneRules: Array<Function> = [
            (v: string) => !!v || 'Pole wymagane'
        ];
        nameRules: Array<Function> = [
            (v: string) => !!v || 'Pole wymagane'
        ];
        lastnameRules: Array<Function> = [
            (v: string) => !!v || 'Pole wymagane'
        ];
        validate() {
            const isValid = (this.$refs.form as HTMLFormElement).validate();
            return isValid as boolean;

        }

        async registerUser() {

            this.runValidate = true;
            const isValid = this.validate();
            if (!isValid) {
                return;
            }

            await registerFunc(this.registerModel).then(() => {
                this.registerSuccess = true;
                this.showRegisterDialog = true;
            }).catch((ex) => {
                this.registerSuccess = false;
                this.showRegisterDialog = false;
            });
        }
    }
</script>

<style lang="scss">
    .auth-page {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        /*margin-top: 60px;*/
    }
</style>