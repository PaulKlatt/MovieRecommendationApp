<template>
  <div class="update-user">
      <h2>please enter any changes you want to make to your account</h2>
    <!-- need to be able to update database from here with account info
    <form class="genre-form">
        <label id="profile-name">profile name: </label>
        <input name="profile-name" type="text"/>
       <h3>favorite genres:</h3> add some binding to the checkboxes so your favorites are already checked?
        <ul class="genres">
            
          <li>
            <input name="action" type="checkbox"/>
            <label for="action">action</label>

          </li>
          <li>
            <input name="romcom" type="checkbox"/>
            <label for="romcom">romcom</label>

          </li>
          <li>
            <input name="horror" type="checkbox"/>
            <label for="horror">horror</label>

          </li>
          <li>
            <input name="drama" type="checkbox"/>
            <label for="drama">drama</label>

          </li>
          <li>
            <input name="comedy" type="checkbox"/>
            <label for="comedy">comedy</label>

          </li>
        </ul>
    <button id="updateAccountInfo" type="submit"
    >Update Account Info</button>

    
    </form>
    -->
    <update-password id="passwordForm"/>  

    </div>
    
  
</template>

<script>
import UpdatePassword from './UpdatePassword.vue'
export default {
  components: { UpdatePassword },

}
</script>

<style>



</style>