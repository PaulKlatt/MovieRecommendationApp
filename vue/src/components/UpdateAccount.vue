<template>
  <div class="update-user">
    <!-- need to be able to update database from here with account info --> 
    <form class="genre-form">
      <h2>Update Account</h2>
        <label for="profile-name">Profile Name: </label>
        <input name="profile-name" type="text"/>
        <h3>Favorite Genres:</h3> <!-- add some binding to the checkboxes so your favorites are already checked? -->
        <ul class="genres">
            
          <li>
            <input name="action" type="checkbox"/>
            <label for="action">Action</label>

          </li>
          <li>
            <input name="romcom" type="checkbox"/>
            <label for="romcom">RomCom</label>

          </li>
          <li>
            <input name="horror" type="checkbox"/>
            <label for="horror">Horror</label>

          </li>
          <li>
            <input name="drama" type="checkbox"/>
            <label for="drama">Drama</label>

          </li>
          <li>
            <input name="comedy" type="checkbox"/>
            <label for="comedy">Comedy</label>

          </li>
        </ul>
    <button type="submit" style="background-color: #f67280;
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;
      user-select: none; touch-action: manipulation;
      padding: 0.25em 0.5em;"
    >Update Account Info</button>

    
    </form>
    
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