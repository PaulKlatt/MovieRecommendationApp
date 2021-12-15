<template>
    <div id="user-homepage">
      <!-- this might be its own component, ChooseGenres -->
<<<<<<< HEAD
     
      <form class="genre-form" v-on:submit.prevent='GetRandomMovie()'>
        <label for="genres">Choose the genres you would like to browse</label><br/>
        <select name="genres" v-model='selected_genre_objects' multiple>
          <option v-for="genre in genres" v-bind:key='genre.id' v-bind:value='genre.id'>{{genre.name}}</option>
        </select><br/>
        <button type="submit">Find Random Movie!</button>
      </form>

      <button v-on:click='SaveToExcluded("Favorite")'>Swipe Up(Favorite)</button>
      <button v-on:click='SaveToExcluded("Passed")'>Swipe Right(Pass)</button>
      <button v-on:click='SaveToExcluded("Uninterested")'>Swipe Left(Completely Uninterested)</button>
=======
      <h2>choose the genres you would like to browse</h2>
      <form class="genre-form" v-on:submit.prevent='GetRandomMovie()'>
        <ul class="genres">
          <li v-for="genre in genres" v-bind:key='genre.id'>
            <input v-bind:name='genre.name' type='checkbox' v-bind:value='genre.id' v-model='selected_genre_ids' />
            <label v-bind:for='genre.name'>{{ genre.name }}</label>
          </li>
        </ul>
        <button id='findMoviesRandom' type="submit">find random movie!</button>
      </form>
      <button id='swipeUp' v-on:click='SaveToExcluded("Favorite")'>swipe up (add to favorites)</button>
      <button id='swipeRight' v-on:click='SaveToExcluded("Passed")'>swipe right (pass)</button>
      <button id='swipeLeft' v-on:click='SaveToExcluded("Uninterested")'>swipe left (completely uninterested)</button>
>>>>>>> 108f61c4bd55581272e831cb31ba4079dbd83bb3
      <div id="movie-details" v-if="suggestedMovie">
        <ul>
          <li v-if="suggestedMovie.posterPath" id="movieTitle"> {{ suggestedMovie.title }}</li>
          <li><img v-bind:src="'https://image.tmdb.org/t/p/original' + suggestedMovie.posterPath" /></li>
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

export default {
  data() {
    return {
      genres: [],
      selected_genre_objects: [],
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
<<<<<<< HEAD
      let genreIds = []
      this.selected_genre_objects.forEach(element => {
        genreIds.push(element) 
      })
      return genreIds.join('|');
    },
    genreNames() {
      let genreNames = []
      this.genres.forEach(element => {
        genreNames.push(element.name)
      })
      return genreNames
=======
      if (this.selected_genre_ids.length == 0)
      {
        return '*';

      } else {
        return this.selected_genre_ids.join('|');
      }
      
>>>>>>> 108f61c4bd55581272e831cb31ba4079dbd83bb3
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
<<<<<<< HEAD
=======
ul {
  list-style-type: none;
}

#findMoviesRandom, #swipeUp, #swipeRight, #swipeLeft {
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


>>>>>>> 108f61c4bd55581272e831cb31ba4079dbd83bb3

</style>