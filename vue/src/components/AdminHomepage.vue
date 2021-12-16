<template>
  <div id="admin-homepage">
      <button id="banButton" v-on:click="showForm = true">ban movie</button>
      <form v-if="showForm" v-on:submit.prevent="banMovie(movieIdToBan)">
        <div>
        <label for="movieToBan">movie id: </label>
        <input
          type="text"
          id="movieToBan"
          placeholder="enter a movie id to ban"
          v-model="movieIdToBan"
          required
        />
        <button id="submitToBan" type="submit">submit movie to ban</button>
      </div>
      </form>
      
      <loading class="loading" v-if="isLoading"/>
  </div>
</template>

<script>
import movieService from '../services/MovieService';
import loading from '../components/Loading';

export default {
  components: { loading },
  data() {
    return {
      showForm: false,
      movieIdToBan: "",
      isLoading: true
    }
  },
  created(){
    this.isLoading = false;
  },
  methods: {
    banMovie(movieId){
      movieService.banMovie(movieId, this.$store.state.user.userId).then(response => {
        if (response.status == 201)
        {
          alert("Movie banned successfully.")
          this.showForm= false,
          this.movieIdToBan= ""
        } else {
          //ERROR HANDLING
          alert("Error banning movie.")
        }
      })
    }
  }
}
</script>

<style>

#banButton {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
      margin-right: 20px;
}

#submitToBan{
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 11px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}
#submitToBan:active{
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}
#banButton:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#admin-homepage{
  display: flex;
   align-items: center;
  justify-content: center;
  margin-top: 10%;
}


</style>