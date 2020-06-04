<template>
  <div class="auth-page">
 

    <div class="row d-flex justify-content-center h-100">

        <div class="col-md-6 ">

            <!--<ul class="error-messages">
      <li>That email is already taken</li>
    </ul>-->
            <!--<form>-->
            <fieldset class="form-group">
                <input v-model="email" v-on:keyup.enter="login"  class="form-control form-control-lg" type="text" placeholder="Email">
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
                <!--</form>-->
        </div>
    </div>
  </div>

</template>
<script lang="ts">
import {Vue, Component} from "vue-property-decorator";
//import UserLogin from "@/store/models";
import users from "@/store/Modules/Users";

@Component
export default class Login extends Vue {
    email = "";
    password = "";
    errorMessage = ""

   async login(){
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