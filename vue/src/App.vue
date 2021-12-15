<template>
  <div id="app">
    <div id="nav">
      <div v-if="$store.state.token != ''">
        <router-link v-bind:to="{ name: 'home' }">home</router-link>
      </div>
      <div v-if="$store.state.token != ''">
        <router-link v-bind:to="{ name: 'account', params:{id: $store.state.user.userId} }" >account</router-link>
      </div>
      <div v-if="$store.state.token != ''">
        <router-link v-bind:to="{ name: 'logout' }" >logout</router-link>
      </div>
    </div>
    <router-view />
  </div>
</template>

<style scoped>


</style>