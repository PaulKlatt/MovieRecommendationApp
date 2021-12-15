<template>
  <div>
      <h1>Account Details</h1>
      <loading class="loading" v-if="isLoading"/>
      
  </div>
</template>

<script>


export default {
  components: { loading },
  data() {
    return{
      isLoading: true
    }
 }

}

</script>

<style>

</style>