<template>
    <div id="user-homepage">
      <h2>Choose the genres you would like to browse</h2>
      <form id="genre-form">
        <ul id="genres">
          <li>
            <input id="action" name="action" type="checkbox"/>
            <label for="action">Action</label>

          </li>
          <li>
            <input id="romcom" name="romcom" type="checkbox"/>
            <label for="romcom">RomCom</label>

          </li>
          <li>
            <input id="horror" name="horror" type="checkbox"/>
            <label for="horror">Horror</label>

          </li>
          <li>
            <input id="drama" name="drama" type="checkbox"/>
            <label for="drama">Drama</label>

          </li>
          <li>
            <input id="comedy" name="comedy" type="checkbox"/>
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