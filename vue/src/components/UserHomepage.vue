<template>
    <div id="user-homepage">
      <!-- this might be its own component, ChooseGenres -->
      <h2>Choose the genres you would like to browse</h2>
      <form class="genre-form" v-on:submit.prevent='GetRandomMovie()'>
        <ul class="genres">
          <li v-for="genre in genres" v-bind:key='genre.id'>
            <input v-bind:name='genre.name' type='checkbox' v-bind:value='genre.id' v-model='selected_genre_ids' />
            <label v-bind:for='genre.name'>{{ genre.name }}</label>
          </li>
        </ul>
        <button type="submit">Find Random Movie!</button>
      </form>
      <button v-on:click='SaveToExcluded("Favorite")'>Swipe Up(Favorite)</button>
      <button v-on:click='SaveToExcluded("Passed")'>Swipe Right(Pass)</button>
      <button v-on:click='SaveToExcluded("Uninterested")'>Swipe Left(Completely Uninterested)</button>
      <div id="movie-details" v-if="suggestedMovie">
        <ul>
          <li>Title: {{ suggestedMovie.title }}</li>
          <li><img v-bind:src="'https://image.tmdb.org/t/p/original' + suggestedMovie.posterPath" /></li>
          <li>Overview: {{ suggestedMovie.overview }}</li>
          <li>Release Date: {{ suggestedMovie.releaseDate }}</li>
        </ul>
      </div>
    </div>
</template>

<script>
import movieService from '../services/MovieService';
import userService from '../services/UserService';

export default {

  data() {
    return {
      genres: [],
      selected_genre_ids: [],
      suggestedMovie: false,
      sameGenres: false,
      currentUser: false
      //bind selected genres to the checkboxes
    }
  },
  created() {
    // call the genres service? or maybe its movie service? to find the available genres
    // and then loop through them to give options
    movieService.getGenres().then( response => {
      this.genres = response.data.genres
    }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up' + error)
    });

    userService.getUser(this.$store.state.user.userId).then(response => {

        if (response.status === 200 && response.data != null) {
          this.currentUser = response.data;
          /* maybe send them somewhere? */
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
    });
  },
  computed: {
    queryString() {
      return this.selected_genre_ids.join('|');
    }
  },
  methods: {
    GetRandomMovie(){
      // do while loop, while its in exluded movies list
      /*let randomPageNumber;
        await movieService.getRandomPageNumber(this.queryString).then( response => {
         randomPageNumber = response.data;
        }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up 1: ' + error)
        }); 
*/
      movieService.getRandomMovie(this.queryString + "/users/" + this.currentUser.userId).then( response => {
        this.suggestedMovie = response.data;
    }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up 2: ' + error)
    });    
    },

    SaveToExcluded(opinion){
      const movieToExclude = {
        MovieId: this.suggestedMovie.id,
        UserId: this.currentUser.userId,
        Opinion: opinion
      }
      userService.saveToExcluded(this.currentUser.userId, movieToExclude);

      this.GetRandomMovie();
    }
  }
}
</script>

<style>
ul {
  list-style-type: none;
}
img{
  max-width: 100px;
  height: 200px;
}


</style>