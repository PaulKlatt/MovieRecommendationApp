<template>
  <div class="account-details" >
    <h1>Account Details</h1>
    <div id="user-account">
      <h3>Username: {{ currentUser.username }}</h3>
      <h3>{{ currentUser.role === 'user' ? 'Favorite' : 'Banned' }} Movies: </h3>
       <div id="movie-details" v-if="moviesToView">
        <table>
          <tr>
            <th>Title</th>
            <th>Movie Id</th>
            <th>Movie Poster</th>
            <th>Genres</th>
          </tr>
          <tr v-for="movie in moviesToView" v-bind:key='movie.movieid'>
            <td>{{ movie.title }}</td>
            <td> {{ movie.movieId }}</td>
            <td><img class="poster" v-bind:src="'https://image.tmdb.org/t/p/original' + movie.posterPath" /></td>
            <td>{{ GenreNames(movie.genreIds) }}</td>
          </tr>
        </table>
       </div>
    </div>
  </div>
</template>

<script>
//import userService from "../services/UserService";
import movieService from "../services/MovieService";
export default {

  data() {
    return{
      currentUser: this.$store.state.user,
      moviesToView: false
    } 
  },

  methods: {
    GenreNames(genreString) {
      const containedGenreIds = genreString.split('|');
      const allGenreList = this.$store.state.genres
      let containedGenreNames = '';
      containedGenreIds.forEach( cId => {
        allGenreList.forEach( genreItem => {
          if (cId == genreItem.id){
            if (containedGenreNames != ''){
              containedGenreNames = containedGenreNames + ", " + genreItem.name;
            }else {
              containedGenreNames = genreItem.name;
            }
            
          }
        })
      })
      return containedGenreNames;
    }
  },
  
  created(){  
      movieService.findMoviesByUserId(this.$store.state.user.userId).then(response => {
        if (response.data != {}){
           this.moviesToView = response.data;
        }
      });
  } 
}

</script>

<style>

.poster {
  width:15%;
  height: 20%;
  margin: auto;
}

th, td {
  border: 1px solid black;
  text-align: center;
  vertical-align: middle;
}
</style>