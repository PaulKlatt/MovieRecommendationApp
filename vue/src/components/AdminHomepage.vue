<template>
  <div id="admin-homepage">
      <button v-on:click="showForm = true">Ban Movie</button>
      <form v-if="showForm" v-on:submit.prevent="banMovie(movieIdToBan)">
        <div>
        <label for="movieToBan">Movie Id:</label>
        <input
          type="text"
          id="movieToBan"
          placeholder="Enter a movie Id to ban"
          v-model="movieIdToBan"
          required
        />
        <button type="submit">Submit Movie To Ban</button>
      </div>
      </form>
      <button>Modify Accounts</button>
  </div>
</template>

<script>
import movieService from '../services/MovieService';

export default {
  data() {
    return {
      showForm: false,
      movieIdToBan: ""
    }
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

</style>