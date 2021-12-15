<template>
    <div id="user-homepage">
      <!-- this might be its own component, ChooseGenres -->
      <button id="selectGenres" v-if="!showForm" v-on:click="ToggleForm">Select Genres</button>
      <loading class="loading" v-if="isLoading"/>
      <form class="genre-form" v-if="!isLoading" v-on:submit.prevent='GetRandomMovie()'>
        <div v-show="showForm">
        <label for="genres">Choose the genres you would like to browse</label><br/>
        <select name="genres" v-model='selected_genre_objects' multiple>
          <option v-for="genre in genres" v-bind:key='genre.id' v-bind:value='genre.id'>{{genre.name}}</option>
        </select><br/>
        </div>
        <button id="findMoviesRandom" type="submit">Find Random Movie!</button>
      </form>

      <button id="swipeUp" v-on:click='SaveToExcluded("Favorite")'>Swipe Up(Favorite)</button>
      <button id="swipeRight" v-on:click='SaveToExcluded("Passed")'>Swipe Right(Pass)</button>
      <button id="swipeLeft" v-on:click='SaveToExcluded("Uninterested")'>Swipe Left(Completely Uninterested)</button>
      <div id="movie-details" v-if="suggestedMovie">
        <ul>
          <li v-if="suggestedMovie.posterPath" id="movieTitle"> {{ suggestedMovie.title }}</li>
          <li ><img id="homepagePoster" v-bind:src="'https://image.tmdb.org/t/p/original' + suggestedMovie.posterPath" /></li>
           <li v-if="!suggestedMovie.posterPath"><p>Sorry, no poster found for this movie.</p><img id="posterNotFound" src="https://previews.123rf.com/images/lineartestpilot/lineartestpilot1802/lineartestpilot180205606/94855861-cartoon-cat-shrugging-shoulders.jpg?fj=1" /></li>
          <li id="releaseDate">Release Date: {{ suggestedMovie.releaseDate }}</li>
          <li id="movieOverview"> {{ suggestedMovie.overview }}</li>
          <li>Genres: {{ GenreNames(suggestedMovie.genreIds) }}</li>
        </ul>
      </div>
    </div>
</template>

<script>
import movieService from '../services/MovieService';
import userService from '../services/UserService';
import loading from '../components/Loading';

export default {
  components: { loading },
  data() {
    return {
      genres: [],
      selected_genre_objects: [],
      suggestedMovie: false,
      currentUser: false,
      sameGenres: false,
      isLoading: true,
      showForm: false
      //bind selected genres to the checkboxes
    }
  },
  created() {
    // call the genres service? or maybe its movie service? to find the available genres
    // and then loop through them to give options
    this.genres = this.$store.state.genres;
    this.isLoading = false;
  },
  computed: {
    queryString() {
      if (this.selected_genre_objects.length == 0)
      {
        return '*';

      } else {
        return this.selected_genre_objects.join('|');
      }
      
    },
    /*
    queryString() {
      let genreIds = []
      this.selected_genre_objects.forEach(element => {
        genreIds.push(element) 
      })
      if (genreIds == []){
        return "*";
      } else {
      return genreIds.join('|');
      }
    },*/
    genreNames() {
      let genreNames = []
      this.genres.forEach(element => {
        genreNames.push(element.name)
      })
      return genreNames
    },
  },
  methods: {
    GetRandomMovie(){
      this.isLoading= true;
      movieService.getRandomMovie(this.queryString + "/users/" + this.$store.state.user.userId).then( response => {
        this.suggestedMovie = response.data;
        this.isLoading= false; 
    }).catch ( error => {
      //ERROR HANDLING
      console.log('Something messed up 2: ' + error)
    });
    this.showForm = false;    
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
      swipeHandler(direction){ 
        if(direction == 'right'){
          this.SaveToExcluded("Passed");
        } else if (direction == 'top'){
          this.SaveToExcluded("Favorite");
        } else if (direction == 'left'){
          this.SaveToExcluded("Uninterested");
        }
        console.log(direction);
      
      },
      ToggleForm() {
        this.showForm = !this.showForm;
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
      }
    }  
  }

 

    
</script>

<style>
ul {
  list-style-type: none;
}

.loading {
  display: flex;
  margin: auto;
  width: 100px;
  height: 100px;
  text-align: center;
  justify-content: center;
  align-content: center;
  


  
}
#movieTitle{
  color: #f67280;
  
}
#homepagePoster{
  width: 30%;
  height: 50%;
}
#releaseDate{
  color: #f67280;
}
#movieOverview{
  color: #f8b195;
}

#findMoviesRandom, #swipeUp, #swipeRight, #swipeLeft, #selectGenres {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
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

#selectGenres:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}


</style>