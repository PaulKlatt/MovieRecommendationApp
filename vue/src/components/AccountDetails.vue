<template>
  <div class="account-details" >
    <h1>{{ currentUser.username }}'s account</h1>
    <loading class="loading" v-if="isLoading"/>
    <div id="user-account">
      <h3>{{ currentUser.role === 'user' ? '' : 'Banned Movies' }}  </h3>
       <div id="movie-details" v-if="moviesToView">
        
        <ul id="accountMovieCard" v-for="movie in moviesToView" v-bind:key='movie.movieid'>
          <li id="accountMoviePoster" v-if="currentUser.role === 'user' "> <img class="poster" v-if="movie.posterPath" v-bind:src="'https://image.tmdb.org/t/p/original' + movie.posterPath" /></li>
          <li id="accountMovieTitle">{{ movie.title }}</li>
          <li id="accountMovieId">ID: {{ movie.movieId }}</li>
          <li id="accountMovieGenre"> {{ GenreNames(movie.genreIds) }}</li>
          </ul>
       </div>
    </div>
    <div> 
    <button v-on:click="DeleteActiveUser">Delete Account</button>
    </div>
  </div>
</template>

<script>
import userService from "../services/UserService";
import movieService from "../services/MovieService";
import loading from '../components/Loading';
export default {
components: { loading },
  data() {
    return{
      currentUser: this.$store.state.user,
      moviesToView: false,
      isLoading: false
    } 
  },

  methods: {
    getActiveUser() {
      this.isLoading = true;
      userService.getUser(this.$store.state.user.userId).then(response => {
        this.currentUser = response.data;
        this.isLoading = false;

        if (response.status === 200 && response.data != null) {
          /* maybe send them somewhere? */
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
      });
      
    },
      /* Delete Account Attempt  */
      DeleteActiveUser() {
      const verification = confirm("Are you sure you want to delete your account? Press OK to proceed.")
      if(verification){
              this.isLoading = true;
      userService.deleteUser(this.$store.state.user.userId).then(response => {        if (response.status === 204) {
          /* maybe send them somewhere? */
          alert("Account deleted successfully")
          this.$store.commit("LOGOUT")
          this.$router.push({ name: "login"})
        } else {
          alert("Account not found, please attempt to sign in again.")
          /*this.$router.push(`/${name: login}`); */
        }
       });

      }
    },
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

button {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}

button:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

</style>