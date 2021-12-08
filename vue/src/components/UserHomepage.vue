<template>
    <div id="user-homepage">
      <h2>Choose the genres you would like to browse</h2>
      <form class="genre-form">
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
        <button type="submit">Find Random Movie!</button>
      </form>
    </div>
</template>

<script>
export default {

}
</script>

<style>
ul {
  list-style-type: none;
}
</style>