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
          <li>Movie Id: {{ suggestedMovie.id }}</li>
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
      currentUser: false
      //bind selected genres to the checkboxes
    }
  },
  created() {
    // call the genres service? or maybe its movie service? to find the available genres
    // and then loop through them to give options
    this.genres = this.$store.state.genres;
  },
  computed: {
    queryString() {
      if (this.selected_genre_ids.length == 0)
      {
        return '*';

      } else {
        return this.selected_genre_ids.join('|');
      }
      
    }
  },
  methods: {
    GetRandomMovie(){
      movieService.getRandomMovie(this.queryString + "/users/" + this.$store.state.user.userId).then( response => {
        this.suggestedMovie = response.data;
    }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up 2: ' + error)
    });    
    },

    SaveToExcluded(opinionType){

        const movieInfo = {
          MovieCard: {
            MovieId: this.suggestedMovie.id,
            Title: this.suggestedMovie.title,
            PosterPath: this.suggestedMovie.posterPath,
            GenreIds: this.suggestedMovie.genreIds.join('|')
          },
          MovieToExclude: {
            MovieId: this.suggestedMovie.id,
            UserId: this.$store.state.user.userId,
            Opinion: opinionType,
            RemovalTracker: 0
          }
        }
        userService.saveToExcluded(this.$store.state.user.userId, movieInfo);

        this.GetRandomMovie();
      }
    }
  }
</script>

<style>
ul {
  list-style-type: none;
}
</style>