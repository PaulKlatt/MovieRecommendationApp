<template>
  <div class="account-details" >
    <h1>{{ currentUser.username }}'s account</h1>
    <div id="user-account">
      <!-- <h3>Username: {{ currentUser.username }}</h3> -->
      <h3>{{ currentUser.role === 'user' ? '' : 'Banned Movies' }}  </h3>
       <div id="movie-details" v-if="moviesToView">
         <!--
        <table>
          <tr>
            <th>Title</th>
            <th>Movie Id</th>
            <th v-if="currentUser.role === 'user' ">Movie Poster</th>
            <th>Genres</th>
          </tr>
          <tr v-for="movie in moviesToView" v-bind:key='movie.movieid'>
            <td>{{ movie.title }}</td>
            <td> {{ movie.movieId }}</td>
            <td v-if="currentUser.role === 'user' ">
              <img class="poster" v-if="movie.posterPath" v-bind:src="'https://image.tmdb.org/t/p/original' + movie.posterPath" /></td>
                <div v-if="!movie.posterPath">
                  <p>Sorry, no poster found for this movie.</p>
                  <img id="posterNotFound" src="https://previews.123rf.com/images/lineartestpilot/lineartestpilot1802/lineartestpilot180205606/94855861-cartoon-cat-shrugging-shoulders.jpg?fj=1" />
                </div>
            <td>{{ GenreNames(movie.genreIds) }}</td>
          </tr>
        </table>
        -->
        <ul id="accountMovieCard" v-for="movie in moviesToView" v-bind:key='movie.movieid'>
          <li id="accountMoviePoster" v-if="currentUser.role === 'user' "> <img class="poster" v-if="movie.posterPath" v-bind:src="'https://image.tmdb.org/t/p/original' + movie.posterPath" /></li>
          <li id="accountMovieTitle">{{ movie.title }}</li>
          <li id="accountMovieId">ID: {{ movie.movieId }}</li>
          <li id="accountMovieGenre"> {{ GenreNames(movie.genreIds) }}</li>
          </ul>
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
  width: 75%;
  height: 90%;
  margin: auto;
  display: block;
}


#accountMovieTitle{
  color: #f67280;
  font-size: 25px;
  text-align: center;
  
  
}
#accountMovieGenre{
  color: #f8b195;
  text-align: center;
}
#accountMovieId{
  color: #f8b195;
  text-align: center;
}
#movie-details{
  display: grid;
  grid-template-columns: 1fr, 1fr, 1fr;
  gap: 50px;
  
  justify-items: center;
  margin-right: 10px;
  grid-template-areas:
  "movie movie movie"
}

ul{
  grid-area: "movie"
}

@media screen and (max-width: 768px){
  #movie-details{
    grid-template-areas:
    "movie";
    grid-template-columns: 1fr;
  }
  
}

</style>