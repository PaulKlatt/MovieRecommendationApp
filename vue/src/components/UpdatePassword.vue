<template>
    <div class='updatePassword'>
    <h3>reset your password:</h3>
    <loading class="loading" v-if="isLoading"/>
    <button class= 'updatePassword' v-on:click.prevent="showForm = true" v-if="!showForm">click here to update password</button>
    <form class='updatePassword' v-if="showForm === true" @submit.prevent="updatePassword">
      <div class="alert alert-danger" role="alert" v-if="passwordError">
                {{ passwordErrorMsg }}
               </div>
    <table>
    <tbody>
      <tr>
        <td>
    <label for="currentPassword">current password: </label>
        </td>
        <td>
    <input
                     type="password"
                     id="currentPassword"
                     class="form-control"
                     placeholder="current password"
                     v-model="user.currentPassword"
                     required
    />
        </td>
        </tr>
        <tr>
          <td>

                    <label for="password">new password:</label>
          </td>
          <td>

                    <input
                      type="password"
                      id="password"
                      class="form-control"
                      placeholder="new password"
                      v-model="user.newPassword"
                      required
    />
          </td>
        </tr>
        <tr>
          <td>

    <label for="password">confirm new password: </label>
          </td>
          <td>
                      <input
                      type="password"
                      id="confirmPassword"
                      class="form-control"
                      placeholder="confirm new password"
                      v-model="user.confirmNewPassword"
                      required
                    />
          </td>
          </tr>
          <tr>
            <td>
            </td>
            <td>
              
                     <button id='loginButton' class="btn btn-lg btn-primary btn-block" type="submit">
                      update password
                      </button>
            </td>
            </tr>
    </tbody>
    </table>
    </form>
    </div>
</template>

<script>

import userService from '../services/UserService';
import loading from '../components/Loading';

export default {
  components: { loading },
  name: "passwordForm",
  data() {
    return{
      showForm: false,
      user: {
        currentPassword: '',
        newPassword: '',
        confirmNewPassword: ''
      },
      passwordError: false,
      passwordErrorMsg: "error",
      isLoading: false
    };
  },

methods: {
    updatePassword() {

      if (this.user.newPassword != this.user.confirmNewPassword) {
        this.passwordError = true;
        this.passwordErrorMsg = 'new password & confirm new password do not match';
      } 
      else if(this.user.newPassword.length < 8 || this.user.confirmNewPassword.length < 8){
        this.passwordError = true;
        this.passwordErrorMsg = 'password must be at least 8 characters long';
      }
      else if(this.user.newPassword == this.user.confirmNewPassword && this.user.newPassword == this.user.currentPassword){
        this.passwordError = true;
        this.passwordErrorMsg = 'new password cannot be the same as the old password';
      }

      else {
        const newUserAndPassword = {
          username: this.$store.state.user.username,
          password: this.user.newPassword,
          role: this.$store.state.user.role,
          confirmPassword: this.user.confirmNewPassword
        
        }
        this.isLoading = true;
        userService
          .updatePassword(newUserAndPassword)
          .then((response) => {
            if (response.status == 200) {
               this.isLoading = false;
              this.showForm = false 
              alert('your password has been updated')
              //query: { registration: 'success' },
            }
          })
          .catch((error) => {
            const response = error.response;
            this.passwordError = true;
            if (response.status === 400) {
              this.passwordErrorMsg = 'bad request';
            }
            if(response.status === 409){
              this.passwordErrorMsg = "username already exists";
            }
          });
      }
    }

}

}

</script>

<style scoped>

#updatePassword, #loginButton {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}

#updatePassword:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#loginButton:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

</style>