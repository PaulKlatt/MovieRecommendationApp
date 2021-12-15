<template>
  <div class="account-details" >
    <h1>Account Details</h1>
    <loading class="loading" v-if="isLoading"/>
    <div id="user-account" v-if="$store.state.user.role == 'user'">
        
        <h3>Placeholder ProfileName</h3>
        <h3>Placeholder Favorite Genres</h3>
        <!-- avatar? anythin else colt wants? placeholders need to be bound to account details from the accounts table.
             That data probably needs to be stored in the store...? 
             Might take a prop for the user or account info, not sure-->
    </div>
    <div id="admin-account" v-if="$store.state.user.role == 'admin'">
      
    </div>
  <div> 
    <button v-on:click="DeleteActiveUser">Delete Account</button>
    </div>
        
  </div>
</template>

<script>
import userService from "../services/UserService";

import loading from '../components/Loading';
export default {
  components: { loading },
  data() {
    return{
      currentUser : {},
      isLoading: false

    } 
  },
  created(){

  },
  
  methods: {
    getActiveUser() {
      this.isLoading = true;
      userService.getUser(this.$store.state.user.userId).then(response => {
        this.currentUser = response.data;
        this.isLoading = false;

        if (response.status === 200 && response.data != null) {
          /* maybe send them somewhere? */
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
      });
      
    },
      /* Delete Account Attempt  */
      DeleteActiveUser() {
      const verification = confirm("Are you sure you want to delete your account? Press OK to proceed.")
      if(verification){
              this.isLoading = true;
      userService.deleteUser(this.$store.state.user.userId).then(response => {        if (response.status === 204) {
          /* maybe send them somewhere? */
          alert("Account deleted successfully")
          this.$store.commit("LOGOUT")
          this.$router.push({ name: "login"})
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
       });

      }
    }
  }
}

</script>

<style>

</style>