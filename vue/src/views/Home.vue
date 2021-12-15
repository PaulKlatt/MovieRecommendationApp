<template>
  <div class="home">
    <h1>home</h1>
    <!--might want to put this as some v-if 
    <p>You must be authenticated to see this</p>
    -->
    <user-homepage v-if='userrole === "user"' />
    <admin-homepage v-if='userrole != "user"' />
    
  </div>
</template>

<script>
import AdminHomepage from '../components/AdminHomepage.vue';
import UserHomepage from '../components/UserHomepage.vue';


export default {
  components: { AdminHomepage, UserHomepage },
  name: "home",
  data() {
    return{
      userrole: ""
    }
  },
  created() {
    this.userrole = this.$store.state.user.role;
  }

};
</script>

<style scoped>





</style>