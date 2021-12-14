<template>
    <div class='updatePassword'>
    <h3>Reset your password:</h3>
    <button class= 'updatePassword' v-on:click.prevent="showForm = true" v-if="!showForm">Click Here To Update Password</button>
    <form class='updatePassword' v-if="showForm === true" @submit.prevent="updatePassword">
      <div class="alert alert-danger" role="alert" v-if="passwordError">
                {{ passwordErrorMsg }}
               </div>
    <table>
    <tbody>
      <tr>
        <td>
    <label for="currentPassword">Current Password</label>
        </td>
        <td>
    <input
                     type="password"
                     id="currentPassword"
                     class="form-control"
                     placeholder="Current Password"
                     v-model="user.currentPassword"
                     required
    />
        </td>
        </tr>
        <tr>
          <td>

                    <label for="password">New Password</label>
          </td>
          <td>

                    <input
                      type="password"
                      id="password"
                      class="form-control"
                      placeholder="New Password"
                      v-model="user.newPassword"
                      required
    />
          </td>
        </tr>
        <tr>
          <td>

    <label for="password">Confirm New Password</label>
          </td>
          <td>
                      <input
                      type="password"
                      id="confirmPassword"
                      class="form-control"
                      placeholder="Confirm New Password"
                      v-model="user.confirmNewPassword"
                      required
                    />
          </td>
          </tr>
          <tr>
            <td>
            </td>
            <td>
              
                     <button class="btn btn-lg btn-primary btn-block" type="submit">
                      Update Password
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

export default {
  name: "passwordForm",
  data() {
    return{
      showForm: false,
      user: {
        currentPassword: '',
        newPassword: '',
        confirmNewPassword: '',
        
      },
      passwordError: false,
      passwordErrorMsg: "ugh"
    };
  },
methods: {
    updatePassword() {
      if (this.user.newPassword != this.user.confirmNewPassword) {
        this.passwordError = true;
        this.passwordErrorMsg = 'New Password & Confirm New Password do not match.';
      } 
      else if(this.user.newPassword.length < 8 || this.user.confirmNewPassword.length < 8){
        this.passwordError = true;
        this.passwordErrorMsg = 'Password must be at least 8 characters long';
      }
      else if(this.user.newPassword == this.user.confirmNewPassword && this.user.newPassword == this.user.currentPassword){
        this.passwordError = true;
        this.passwordErrorMsg = 'New password cannot be the same as the old password.';
      }

      else {
        const newUserAndPassword = {
          username: this.$store.state.user.username,
          password: this.user.newPassword,
          role: this.$store.state.user.role,
          confirmPassword: this.user.confirmNewPassword
        }
        userService
          .updatePassword(newUserAndPassword)
          .then((response) => {
            if (response.status == 200) {
              this.showForm = false 
              alert('Your password has been updated')
              //query: { registration: 'success' },
            }
          })
          .catch((error) => {
            const response = error.response;
            this.passwordError = true;
            if (response.status === 400) {
              this.passwordErrorMsg = 'Bad request';
            }
            if(response.status === 409){
              this.passwordErrorMsg = "Username Already Exists";
            }
          });
      }
    }

}
}

</script>