<template>
  <div>
    <h1>update account</h1>
    <img class = "bestcat" src="https://canary.contestimg.wish.com/api/webimage/5e1468224b7eec03c97f8915-large.jpg?cache_buster=085e00180f70ca62aa0f8a1c7d8e6bdb" />
    <div class="update-user">
    <update-password id="passwordForm"/>  
    <button id="deleteButton" v-on:click="DeleteActiveUser">delete account</button>
    </div>
  </div>
</template>

<script>
import UpdatePassword from './UpdatePassword.vue'
import userService from "../services/UserService";
export default {
  components: { UpdatePassword },
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
    },
  }

}
</script>

<style>

#updateAccountInfo, #deleteButton {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}

.update-user{
  display: flex;
  align-items: center;
  justify-content: center;
  margin-top: 5%;
}
#updateAccountInfo:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#deleteButton:active{
box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

.bestcat {
  width: 400px;
  height: 400px;
  border: 10px solid #6c5b7b
}
</style>