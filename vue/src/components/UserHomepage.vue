<template>
    <div id="user-homepage">
      <h2>Choose the genres you would like to browse</h2>
      <form class="genre-form">
        <ul class="genres">
          <li >

          <li>
            <input name="Action" type="checkbox"/>
            <label for="Action">Action</label>

          </li>
          <li>
            <input name="Adventure" type="checkbox"/>
            <label for="Adventure">Adventure</label>

          </li>
          <li>
            <input name="Animation" type="checkbox"/>
            <label for="Animation">Animation</label>

          </li>
          <li>
            <input name="Comedy" type="checkbox"/>
            <label for="Comedy">Comedy</label>

          </li>
          <li>
            <input name="Crime" type="checkbox"/>
            <label for="Crime">Crime</label>
          </li>
          <li>
            <input name="Documentary" type="checkbox"/>
            <label for="Documentary">Documentary</label>
          </li>
          <li>
            <input name="Drama" type="checkbox"/>
            <label for="Drama">Drama</label>
          </li>
          <li>
            <input name="Family" type="checkbox"/>
            <label for="Family">Family</label>
          </li>
          <li>
            <input name="Fantasy" type="checkbox"/>
            <label for="Fantasy">Fantasy</label>
          </li>
          <li>
            <input name="History" type="checkbox"/>
            <label for="History">History</label>
          </li>
          <li>
            <input name="Horror" type="checkbox"/>
            <label for="Horror">Horror</label>
          </li>
          <li>
            <input name="Music" type="checkbox"/>
            <label for="Music">Music</label>
          </li>
          <li>
            <input name="Mystery" type="checkbox"/>
            <label for="Myster">Mystery</label>
          </li>
          <li>
            <input name="Romance" type="checkbox"/>
            <label for="Romance">Romance</label>
          </li>
          <li>
            <input name="Science Fiction" type="checkbox"/>
            <label for="Science Fiction">Science Fiction</label>
          </li>
          <li>
            <input name="TV Movie" type="checkbox"/>
            <label for="TV Movie">TV Movie</label>
          </li>
          <li>
            <input name="Thriller" type="checkbox"/>
            <label for="Thriller">Thriller</label>
          </li>
          <li>
            <input name="War" type="checkbox"/>
            <label for="War">War</label>
          </li>
          <li>
            <input name="Western" type="checkbox"/>
            <label for="Western">Western</label>
          </li>
        </ul>
        <!--<li v-for=""> -->

        <button type="submit">Find Random Movie!</button>
      </form>
    </div>
</template>

<script>
export default {

  data() {
    return {
      genres: [],
      selected_genre_ids: [] 
      //bind selected genres to the checkboxes
    }
  },
  created() {
    // call the genres service? or maybe its movie service? to find the available genres
    // and then loop through them to give options
  }
}
</script>

<style>
ul {
  list-style-type: none;
}
</style>