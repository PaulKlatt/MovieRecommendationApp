<template>
  <div id="admin-homepage">
      <button>Add Movies</button>
      <button>Modify Accounts</button>
      <loading class="loading" v-if="isLoading"/>

  </div>
 
</template>

<script>
import loading from '../components/Loading.vue';
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