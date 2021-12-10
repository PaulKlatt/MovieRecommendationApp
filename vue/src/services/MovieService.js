import axios from 'axios';

export default {

  getGenres() {
    return axios.get('/movies/genres')
  },

  getMovies(genreIds){
      return axios.get(`/movies/genres/${genreIds}`)
  }
}
