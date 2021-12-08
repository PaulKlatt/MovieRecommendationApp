<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>
      <header v-if="$store.state.token != ''">
        &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'account', params:{id: $store.state.user.userId} }" >Account</router-link>
      </header>
      <header v-if="$store.state.token != ''">
        &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'logout' }" >Logout</router-link>
      </header>
    </div>
    <router-view />
  </div>
</template>

<style scoped>


html, body {
  background-color: lightblue;
}


</style>