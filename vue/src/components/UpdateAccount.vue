<template>
  <div class="update-user">
      <h3>Please enter any changes you want to make to your account.</h3>
    <!-- need to be able to update database from here with account info --> 
    <form class="genre-form">
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
        
        <button type="submit">Update Account Info</button>
    </form>
  </div>  
</template>

<script>
export default {

}
</script>

<style>

</style>