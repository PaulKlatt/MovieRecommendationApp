<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
                {{ registrationErrorMsg }}
               </div>
      <table>
        <tbody>
          <tr>
            <td>
              <label for="username" class="sr-only">username: </label>
              </td>
              <td>
                  <input
                   type="text"
                   id="username"
                    class="form-control"
                    placeholder="username"
                    v-model="user.username"
                    required
                    autofocus
                  />
                </td>
            </tr>
            <tr>
              <td>
                <label for="password" class="sr-only">password: </label>
                </td>
                <td> 
                    <input
                      type="password"
                      id="password"
                      class="form-control"
                      placeholder="password"
                      v-model="user.password"
                      required
                    />
                </td>
              </tr>
              <tr>
                <td>
                <label for="confirmPassword" class="sr-only">confirm password: </label>
                </td>
                <td>
                  <input
                      type="password"
                      id="confirmPassword"
                      class="form-control"
                      placeholder="confirm password"
                      v-model="user.confirmPassword"
                      required
                    />
                </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                     <button id='registerAccountButton' class="btn btn-lg btn-primary btn-block" type="submit">
                      create account
                      </button>
                  </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                    <router-link :to="{ name: 'login' }">have an account?</router-link>
                  </td>

                </tr>
      </tbody>
      </table>
      
      
      
      
     
    </form>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'username is already taken, please choose a unique name',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'password & confirm password do not match';
      } else
       if(this.user.password.length < 8 || this.user.confirmPassword.length < 8){
        this.registrationErrors = true;
        this.registrationErrorMsg = 'password must be at least 8 characters long';
      }
    
      
      else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'bad request';
            }
            if(response.status === 409){
              this.registrationErrorMsg = "username already exists";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'there were problems registering this user';
    },
  },
};

</script>

<style>

#registerAccountButton {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}

#registerAccountButton:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}


</style>




