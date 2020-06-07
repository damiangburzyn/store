<template>
    <div class="auth-page">


        <!--<div class="row d-flex justify-content-center h-100">

            <div class="col-md-6 ">

         
              
                <fieldset class="form-group">
                    <input v-model="email" v-on:keyup.enter="login" class="form-control form-control-lg" type="text" placeholder="Email">
                </fieldset>
                <fieldset class="form-group">
                    <input v-model="password" v-on:keyup.enter="login" class="form-control form-control-lg" type="password" placeholder="Hasło">
                </fieldset>
                <fieldset class="form-group">
                    <span v-if="typeof(errorMessage)!= 'undefined'&& errorMessage !== '' " class="alert text-danger" role="alert">
                        {{errorMessage}}
                    </span>
                </fieldset>
                <button @click="login()" class="btn btn-lg btn-primary pull-xs-right bg-info">
                    Zaloguj
                </button>
            
            </div>
        </div>-->


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
                            <v-text-field v-model="email" v-on:keyup.enter="login"
                                          :rules="emailRules"
                                          outlined
                                          placeholder="Email"></v-text-field>
                            <v-text-field :type="'password'"
                                          v-model="password"
                                          outlined
                                          placeholder="Hasło"
                                           :rules="passwordRules"
                                          ></v-text-field>
                            <span v-if="typeof(errorMessage)!= 'undefined'&& errorMessage !== '' " class="alert text-danger" role="alert">
                                {{errorMessage}}
                            </span>
                        </v-card-text>

                        <v-card-actions>
                            <v-layout row>
                                <v-flex justify-center>
                                    <v-btn 
                                           color="cyan lighten-4"
                                           primary
                                           @click="login()">
                                        Zaloguj
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
import users from "@/store/Modules/Users";

@Component
export default class Login extends Vue {
    valid = true;
    runValidate = false;
    email = "";
    password = "";
    errorMessage = "";
    emailRules: Array<Function>  = [
                (v: string) => !!v || 'E-mail jest wymagany',
                (v: string) => /.+@.+\..+/.test(v) || 'Wpisz poprawny adres email',
            ];
     
    passwordRules: Array<Function> = [
        (v: string) => !!v || 'Hasło jest wymagane'     
    ];

    validate() {
        const isValid = (this.$refs.form as HTMLFormElement).validate();
            return isValid as boolean;
      
    }

    async login() {
        this.runValidate = true;
       let isValid =  this.validate();
       
        if (!isValid)
        {
            return;
        }
    

      const either =   await users.login({ 
        email :this.email,
          password: this.password,
      })
       if (either.isError()) {
           this.errorMessage = either.value as string;
       }
       else {
           this.errorMessage = "";
           this.$router.push('/');
       }
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