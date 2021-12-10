import axios from 'axios';

export default {

  getGenres() {
    return axios.get('/movies/genrelist')
  },

  getMovies(genreIds){
      return axios.get(`/movies/genres/${genreIds}`)
  },

  getRandomMoviePage(queryEnd){
    return axios.get(`/movies/genres/${queryEnd}`)
  }
}
