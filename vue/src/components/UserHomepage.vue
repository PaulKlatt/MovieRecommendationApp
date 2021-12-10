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

export default {

  data() {
    return {
      genres: [],
      selected_genre_ids: [],
      suggestedMovie: false,
      sameGenres: false
      //bind selected genres to the checkboxes
    }
  },
  created() {
    // call the genres service? or maybe its movie service? to find the available genres
    // and then loop through them to give options
    movieService.getGenres().then( response => {
      console.log(response.data)
      this.genres = response.data.genres
    }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up' + error)
    }); 
  },
  computed: {
    queryString() {
      return this.selected_genre_ids.join('|');
    }
  },
  methods: {
    GetRandomMovie(){
      let randomPageNumber;
        await movieService.getMovies(this.queryString).then( response => {
          const totalPages = response.data.totalPages;
          const min = Math.ceil(1);
          const max = Math.floor(totalPages);
          randomPageNumber = Math.floor(Math.random() * (max - min + 1) + min);
          console.log(randomPageNumber);
        }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up 1' + error)
        }); 

      movieService.getRandomMoviePage(this.queryString + "/page/" + randomPageNumber).then( response => {
        const movieArray = response.data.results;
        const min = Math.ceil(0);
        const max = Math.floor(movieArray.length - 1);
        const randomMovieIndex = Math.floor(Math.random() * (max - min + 1) + min);
        this.suggestedMovie = movieArray[randomMovieIndex];
        console.log(movieArray)
    }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up 2' + error)
    }); 
    
      
    }
  }
}
</script>

<style>
ul {
  list-style-type: none;
}
</style>