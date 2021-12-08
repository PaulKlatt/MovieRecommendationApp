<template>
  <div id="app">
    <div id="nav">
      <div v-if="$store.state.token != ''">
        <router-link v-bind:to="{ name: 'home' }">Home</router-link>
      </div>
      <div v-if="$store.state.token != ''">
        <router-link v-bind:to="{ name: 'account', params:{id: $store.state.user.userId} }" >Account</router-link>
      </div>
      <div v-if="$store.state.token != ''">
        <router-link v-bind:to="{ name: 'logout' }" >Logout</router-link>
      </div>
    </div>
    <router-view />
  </div>
</template>

<style scoped>


html, body {
  background-color: lightblue;
}


</style>