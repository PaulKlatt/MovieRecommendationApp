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
              <label for="username" class="sr-only">Username</label>
              </td>
              <td>
                  <input
                   type="text"
                   id="username"
                    class="form-control"
                    placeholder="Username"
                    v-model="user.username"
                    required
                    autofocus
                  />
                </td>
            </tr>
            <tr>
              <td>
                <label for="password" class="sr-only">Password</label>
                </td>
                <td> 
                    <input
                      type="password"
                      id="password"
                      class="form-control"
                      placeholder="Password"
                      v-model="user.password"
                      required
                    />
                </td>
              </tr>
              <tr>
                <td>
                </td>
                <td>
                  <input
                      type="password"
                      id="confirmPassword"
                      class="form-control"
                      placeholder="Confirm Password"
                      v-model="user.confirmPassword"
                      required
                    />
                </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                     <button class="btn btn-lg btn-primary btn-block" type="submit">
                      Create Account
                      </button>
                  </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                    <router-link :to="{ name: 'login' }">Have an account?</router-link>
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
      registrationErrorMsg: 'Username is already taken, please choose a unique name.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else
       if(this.user.password.length < 8 || this.user.confirmPassword.length < 8){
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password must be at least 8 characters long';
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
              this.registrationErrorMsg = 'Bad request';
            }
            if(response.status === 409){
              this.registrationErrorMsg = "Username Already Exists";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};

</script>

<style>

</style>>




