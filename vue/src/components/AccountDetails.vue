<template>
  <div class="account-details" >
    <h1>Account Details</h1>
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
    <button>Delete Account</button>
    </div>
        
  </div>
</template>

<script>
import userService from "../services/UserService";
export default {

  data() {
    return{
      currentUser : {}
    } 
  },
  created(){

  },
  
  methods: {
    getActiveUser() {
      userService.getUser(this.$store.state.user.userId).then(response => {
        this.currentUser = response.data;

        if (response.status === 200 && response.data.length > 0) {
          /* maybe send them somewhere? */
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
      });
    },
      /* Delete Account Attempt  */
      DeleteActiveUser() {
      userService.deleteUser(this.$store.state.user.userId).then(response => {
        this.currentUser = response.data;
        
        if (response.status === 200 && response.data.length > 0) {
          /* maybe send them somewhere? */
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
      });
    }
  }
}

</script>

<style>

</style>