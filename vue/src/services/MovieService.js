import axios from 'axios';

export default {

  getGenres() {
    return axios.get('/movies/genrelist')
  },

  getRandomPageNumber(genreIds){
   return axios.get(`/movies/genres/${genreIds}`)
  },

  getRandomMovie(queryEnd){
    return axios.get(`/movies/genres/${queryEnd}`)
  }
}
