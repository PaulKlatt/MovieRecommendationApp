<template>
    <div id="user-homepage">
      <!-- this might be its own component, ChooseGenres -->
      <div id="topStuff">
        <h2>choose the genres you would like to browse</h2>
        <form class="genre-form" v-on:submit.prevent='GetRandomMovie()'>
          <ul class="genres">
            <li v-for="genre in genres" v-bind:key='genre.id'>
              <input v-bind:name='genre.name' type='checkbox' v-bind:value='genre.id' v-model='selected_genre_ids' />
              <label v-bind:for='genre.name'>{{ genre.name }}</label>
            </li>
          </ul>
          <button id='findMoviesRandom' v-if="!suggestedMovie" type="submit">find a movie!</button>
        </form>
      </div>
      <div id='swipeUp' v-if="suggestedMovie" v-on:click='SaveToExcluded("Favorite")' href="#" class="arrow arrow-top">favorite</div>
      <div id='swipeRight' v-if="suggestedMovie" v-on:click='SaveToExcluded("Passed")' href="#" class="arrow arrow-right">maybe later</div>
      <div id='swipeLeft' v-if="suggestedMovie" v-on:click='SaveToExcluded("Uninterested")' href="#" class="arrow arrow-left">uninterested</div>
      <section class="container">
      <Vue2InteractDraggable
        :interact-out-of-sight-x-coordinate="1000"
        :interact-out-of-sight-y-coordinate="1000"
        :interact-max-rotation="15"
        :interact-x-threshold="200"
        :interact-y-threshold="200"
        @draggedRight='draggedRight'
        @draggedLeft='draggedLeft'
        @draggedUp='draggedUp'
        v-if='isVisible'
        class="card"
        id="card">
        <div class="card__main">
          <div id="movie-details" v-if="suggestedMovie">
            <ul>
              <li id="movieTitle"> {{ suggestedMovie.title }}</li>
              <li>Movie Id: {{ suggestedMovie.id }}
              <li v-if="suggestedMovie.posterPath"><img v-bind:src="'https://image.tmdb.org/t/p/original' + suggestedMovie.posterPath" /></li>
              <li v-if="!suggestedMovie.posterPath"><p>Sorry, no poster found for this movie.</p><img id="posterNotFound" src="https://previews.123rf.com/images/lineartestpilot/lineartestpilot1802/lineartestpilot180205606/94855861-cartoon-cat-shrugging-shoulders.jpg?fj=1" /></li>
              <li id="releaseDate">Release Date: {{ suggestedMovie.releaseDate }}</li>
              <li id="movieOverview"> {{ suggestedMovie.overview }}</li>
              <li>Genres: {{ GenreNames(suggestedMovie.genreIds) }}</li>
            </ul>
          </div>    
        </div>
      </Vue2InteractDraggable>
  </section>
  </div>
</template>

<script>
import movieService from '../services/MovieService';
import userService from '../services/UserService';
import { Vue2InteractDraggable } from 'vue2-interact'

export default {

  components: { Vue2InteractDraggable },

  data() {
    return {
      genres: [],
      selected_genre_ids: [],
      suggestedMovie: false,
      currentUser: false,
      isVisible: true
      //test: document.getElementById("card").cloneNode(true)
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
        setTimeout(() => this.isVisible = false, 200)
        setTimeout(() => {

        this.isVisible = true
      }, 200);
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
      },

         GenreNames(genreArray) {
        const allGenreList = this.$store.state.genres
        let containedGenreNames = '';
        genreArray.forEach( cId => {
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
      },

      draggedLeft() {
        this.SaveToExcluded("Uninterested")
        
      },

      draggedRight() {
        this.SaveToExcluded("Passed")

      },

      draggedUp() {
        this.SaveToExcluded("Favorite")

      },

  }
}
 

    
</script>

<style>
ul {
  list-style-type: none;
  margin: auto;
}

#movie-details li {
  width:90%;
}

img {
  display: block;
  margin: 0 auto;
  width: 80%;
  height: 70%;
}

#findMoviesRandom, #swipeUp, #swipeRight, #swipeLeft {
  background-color: #f67280; font-size: larger; 
  color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
  line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;
  box-shadow: 3px 3px #f8b195;
  padding: 0.25em 0.5em;
  user-select: none; touch-action: manipulation;
  cursor: pointer;
  width:300px;
}

#findMoviesRandom:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#swipeUp:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#swipeRight:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#swipeLeft:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

.arrow{
  position: relative;
  height:40px;
  line-height: 40px;
  margin:auto; 
  text-align:center;

}

.arrow-top:before{
  position: absolute;
  top: -10px;
  left:50%;
  margin-left: -10px;
  content:"";
  border-left: 10px solid #355c7d;
  border-right: 10px solid #355c7d;
  border-bottom: 10px solid #f67280; 
}

.arrow-right:after{
    content: "";
    position: absolute;
    right: -20px;
    top: 0;
    border-top: 20px solid#355c7d;
    border-bottom: 20px solid #355c7d;
    border-left: 20px solid #f67280; 
}

.arrow-left:before{
    content: "";
    position: absolute;
    left: -20px;
    top: 0;
    border-top: 20px solid #355c7d;
    border-bottom: 20px solid#355c7d;
    border-right: 20px solid #f67280; 
}

#movie-details {
  background-color: #f67280;
  width: 100%;
  margin: 20px auto;
  border-radius: 10%;
}

#topStuff {
  grid-area: topStuff;
}

#swipeUp {
  grid-area: swipeUp;
}

#swipeLeft {
  grid-area: swipeLeft;
}

#swipeRight {
  grid-area: swipeRight;
}

.container {
  grid-area: container;
}

#user-homepage {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:". topStuff ."
                     ". swipeUp ."
                     "swipeLeft container swipeRight";
                     
}

</style>