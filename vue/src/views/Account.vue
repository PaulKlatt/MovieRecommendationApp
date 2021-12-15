<template>
  <div>
      <account-details v-if="!showForm"/>
      <update-account v-if="showForm"/>
      <button id="updateAccountInfo" v-if="!showForm" v-on:click="updateAccount()">Update Account Details</button>
  </div>
</template>

<script>
import AccountDetails from '../components/AccountDetails.vue'
import UpdateAccount from '../components/UpdateAccount.vue'
export default {
  components: { AccountDetails, UpdateAccount },
name: "account",
data() {
    return {
        showForm: false
    }
},
methods: {
    updateAccount() {
        this.showForm = true;
    }
}
}
</script>

<style>
#updateAccountInfo {
  background-color: #F67280; font-size: larger; 
      color: #355C7D; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #F8B195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}
#updateAccountInfo:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}
</style>