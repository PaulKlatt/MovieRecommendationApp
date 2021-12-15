<template>
  <div id="app">
    <div id="nav">
      <div id="HomeLink" v-if="$store.state.token != ''">
        <router-link class="link" v-bind:to="{ name: 'home' }">Home | </router-link>
      </div>
      <div id="AccountLink" v-if="$store.state.token != ''">
        <router-link class="link" v-bind:to="{ name: 'account', params:{id: $store.state.user.userId} }" >  Account | </router-link>
      </div>
      <div id="LogoutLink" v-if="$store.state.token != ''">
        <router-link class="link" v-bind:to="{ name: 'logout' }" >  Logout</router-link>
      </div>
    </div>
    <router-view />
  </div>
</template>

<style scoped>
#nav{
  display: flex;
  justify-content: right;
  
}
.link{
  color: #f8b195;
  text-decoration: none;
}

</style>