<template>
  <div>
      <account-details v-if="!showForm"/>
      <update-account v-if="showForm"/>
      <button v-if="!showForm" v-on:click="updateAccount()">Update Account Details</button>
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

</style>